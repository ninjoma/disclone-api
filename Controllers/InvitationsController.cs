using Microsoft.AspNetCore.Mvc;
using disclone_api.Entities;
using disclone_api.Services;
using disclone_api.DTO;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class InvitationsController : ControllerBase
{
    #region Constructor
    private readonly DataContext _context;
    private readonly ILogger<InvitationsController> _logger;
    private readonly IInvitationService _InvitationSv;
    public InvitationsController(DataContext context, ILogger<InvitationsController> logger, IInvitationService InvitationSv)
    {
        _context = context;
        _logger = logger;
        _InvitationSv = InvitationSv;
    }
    #endregion


    /// <summary>
    /// Recupera los detalles de una invitación.
    /// </summary>
    /// <response code="200">La invitación existe.</response>
    /// <response code="404">La invitación pedida no existe.</response>
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
            return NotFound();
        }
    }

    /*[HttpGet("/user/{userId}/server/{serverId}")]
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
    }*/


    /// <summary>
    /// Recupera el servidor asociado con esa invitación
    /// </summary>
    /// <response code="200">Devuelve servidor asociado con la invitación.</response>
    /// <response code="404">La invitación pedida no existe.</response>
    [HttpGet("{id}/servers")]
    public async Task<ActionResult> ListByServerId(int id)
    {
        var result = await _InvitationSv.ListByServerId(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Recupera los usuarios relacionados con esa invitación
    /// </summary>
    /// <response code="200">Devuelve el usuario asociado con la invitación.</response>
    /// <response code="404">La invitación pedida no existe.</response>
    [HttpGet("{id}/users")]
    public async Task<ActionResult> ListByUserId(int id)
    {
        var result = await _InvitationSv.ListByUserId(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return NotFound();
        }
    }
    #endregion

    /// <summary>
    /// Crea una invitación a partir de los datos suministrados.
    /// </summary>
    /// <response code="200">La invitación ha sido creada satisfactoriamente.</response>
    /// <response code="400">La invitación no se ha podido crear por un error en la declaración.</response>
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

    /// <summary>
    /// Edita una invitación.
    /// </summary>
    /// <response code="200">La invitación ha sido editada satisfactoriamente.</response>
    /// <response code="400">La invitación no se ha podido crear por un error en los datos de la petición.</response>
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

    /// <summary>
    /// Elimina la invitación.
    /// </summary>
    /// <response code="200">La invitación ha sido eliminada satifactoriamente.</response>
    /// <response code="400">Hay errores en la petición para eliminar la invitación.</response>
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
