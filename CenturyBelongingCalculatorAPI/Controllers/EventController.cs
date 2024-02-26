using CenturyBelongingCalculatorAPI.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace CenturyBelongingCalculatorAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class EventController : ControllerBase
{
    #region Ctor
    private readonly ILogger<EventController> _logger;
    private readonly ISender _sender;

    public EventController(ILogger<EventController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    #endregion

    [HttpGet]
    [Route("List")]
    [AuthorizeForScopes(Scopes = ["Event.Read"])]
    public async Task<ActionResult<IEnumerable<EventResult>>> Get()
    {
        var query = new GetEventsQuery();

        var result = await _sender.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet]
    [Route("Get")]
    [AuthorizeForScopes(Scopes = ["Event.Read"])]
    public async Task<ActionResult<EventResult>> GetEventByIdAsync(int eventId)
    {
        try
        {
            var query = new GetEventQuery
            {
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
    }
}
