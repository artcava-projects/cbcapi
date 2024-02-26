﻿using CenturyBelongingCalculatorAPI.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace CenturyBelongingCalculatorAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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
    [Route("GetJoin")]
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
    #endregion

    #region Command
    [HttpPost]
    [Route("Calc")]
    public async Task<ActionResult> AddCalc(AddCalcCommand command)
    {
        try
        {
            var result = await _sender.Send(command);

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
