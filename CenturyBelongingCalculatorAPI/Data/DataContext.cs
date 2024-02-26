using CenturyBelongingCalculatorAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CenturyBelongingCalculatorAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Event> Events { get; set; }
}
