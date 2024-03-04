using Azure.Core;
using CenturyBelongingCalculatorAPI.Features;
using CenturyBelongingCalculatorAPI.Features.Calcs.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CenturyBelongingCalculatorAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class CalcController : ControllerBase
{
    #region Ctor
    private readonly ILogger<CalcController> _logger;
    private readonly ISender _sender;

    public CalcController(ILogger<CalcController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    #endregion

    #region Query
    [HttpGet]
    [Route("GetJoinDate")]
    [AuthorizeForScopes(Scopes = ["Calc.Read"])]
    public async Task<ActionResult<DateTimeOffset>> GetJoinDateAsync(DateTimeOffset startDate, int eventId)
    {
        try
        {
            var query = new GetJoinDateQuery
            {
                StartDate = startDate,
                EventId = eventId
            };

            var result = await _sender.Send(query);

            return Ok(result);
        }
        catch (NoEventExistsException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
        catch (NotAllowedCalcException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
    }

    [HttpGet]
    [Route("GetDaysToJoin")]
    [AuthorizeForScopes(Scopes = ["Calc.Read"])]
    public async Task<ActionResult<int>> GetDaysToJoinDateAsync(DateTimeOffset startDate, int eventId)
    {
        try
        {
            var query = new GetDaysToJoinDateQuery
            {
                StartDate = startDate,
                EventId = eventId
            };

            var result = await _sender.Send(query);

            return Ok(result);
        }
        catch (NoEventExistsException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
        catch (NotAllowedCalcException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
        catch (JoinDateElapsedException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
    }

    [HttpGet]
    [Route("GetCalcList")]
    [AuthorizeForScopes(Scopes = ["Calc.Read"])]
    public async Task<ActionResult<CalcResult[]>> GetCalcListAsync()
    {
        try
        {
            var result = await InternalTasks.GetCalcsAsync(User.GetObjectId());
            //var result = await _sender.Send(new GetCalcListQuery());

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
    }

    #endregion

    #region Command
    [HttpPost]
    [Route("AddCalc")]
    [AuthorizeForScopes(Scopes = ["Calc.Write"])]
    public async Task<ActionResult> AddCalc(AddCalcCommand command)
    {
        try
        {
            var result = await _sender.Send(command);

            bool good = await InternalTasks.AddCalcAsync(User.GetObjectId(), result);

            return CreatedAtRoute(new { countId = result.Id }, result);
        }
        catch (EndDateGreaterThenStartDateException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
        catch (NoEventExistsException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
        catch (NotAllowedCalcException ex)
        {
            return Conflict(new
            {
                ex.Message
            });
        }
    }
    #endregion
}
