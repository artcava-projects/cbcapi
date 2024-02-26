using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;
using CenturyBelongingCalculatorAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CenturyBelongingCalculatorAPI.ServiceManager;

//SeriLog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File($"log-{DateTimeOffset.Now:yyyy-MM}.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    #region Add services to the container.
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                builder.Configuration.Bind("AzureAd", options);
                options.TokenValidationParameters.NameClaimType = "name";
            }, 
            options => { builder.Configuration.Bind("AzureAd", options); });
            
    builder.Services.AddAuthorization(config =>
    {
        config.AddPolicy("AuthZPolicy", policyBuilder =>
            policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
    });

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IServiceManager, ServiceManager>();
    #endregion

    #region Repositories

    #region InMemory
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseInMemoryDatabase("CBCDataBase")
               .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    });
    #endregion

    #endregion


    var app = builder.Build();

    #region Seeding
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        new Seed().SeedData(dataContext);
    }
    #endregion

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}
        app.UseSwagger();
        app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();


    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}