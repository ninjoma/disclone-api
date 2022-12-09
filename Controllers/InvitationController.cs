using Microsoft.AspNetCore.Mvc;
using disclone_api.Entities;
using disclone_api.Services.InvitationServices;
using disclone_api.DTOs.InvitationDTOs;

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
    [HttpGet("GetById/{id}")]
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

    [HttpGet("GetByServerIdAndByUserId/{userId}/{serverId}")]
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

    [HttpGet("ListByserverId/{id}")]
    public async Task<ActionResult> ListByserverId(int id)
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

    [HttpGet("ListByUserId/{id}")]
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
    [HttpPost("AddEditAsync")]
    public async Task<ActionResult> AddEditAsync(InvitationDTO invitation)
    {
        var result = await _InvitationSv.AddEditAsync(invitation);
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
    [HttpDelete("ToggleInactiveById/{id}")]
    public async Task<ActionResult> ToggleInactiveById(int id)
    {
        var result = await _InvitationSv.ToggleInactiveById(id);
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
