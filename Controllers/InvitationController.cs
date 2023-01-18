using Microsoft.AspNetCore.Mvc;
using disclone_api.Entities;
using disclone_api.Services;
using disclone_api.DTO;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class InvitationController : ControllerBase
{
    #region Constructor
    private readonly DataContext _context;
    private readonly ILogger<InvitationController> _logger;
    private readonly IInvitationService _InvitationSv;
    public InvitationController(DataContext context, ILogger<InvitationController> logger, IInvitationService InvitationSv)
    {
        _context = context;
        _logger = logger;
        _InvitationSv = InvitationSv;
    }
    #endregion

    #region Get
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _InvitationSv.GetById(id);
        if (result != null)
        {
            return Ok(result);
            
        } else
        {
            return BadRequest();
        }
    }

    [HttpGet("/user/{userId}/server/{serverId}")]
    public async Task<ActionResult> GetByServerIdAndByUserId(int userId, int serverId)
    {
        var result = await _InvitationSv.GetByServerIdAndByUserId(userId, serverId);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}/server")]
    public async Task<ActionResult> ListByServerId(int id)
    {
        var result = await _InvitationSv.ListByServerId(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}/user")]
    public async Task<ActionResult> ListByUserId(int id)
    {
        var result = await _InvitationSv.ListByUserId(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }
    #endregion

    #region Set
    [HttpPost("")]
    public async Task<ActionResult> Add(InvitationDTO invitation)
    {
        var result = await _InvitationSv.Add(invitation);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest();
        }
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> EditById(InvitationDTO invitation)
    {
        var result = await _InvitationSv.EditById(invitation);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest();
        }
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(int id)
    {
        var result = await _InvitationSv.DeleteById(id);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest();
        }
    }
    #endregion
}
