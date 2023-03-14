using disclone_api.Entities;
using disclone_api.Services;
using disclone_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using disclone_api.utils;


namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    #region Constructor
    private readonly DataContext _context;
    private readonly IUserService _UserSv;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly IAuthService _AuthSv;
    private readonly ILoggerService _loggerSv;
    private readonly IMemberService _MemberSv;
    public UsersController(DataContext context, ILoggerService loggerSv, IUserService UserSv, ITokenBuilder tokenBuilder, IAuthService AuthSv, IMemberService MemberSv)
    {
        _context = context;
        _loggerSv = loggerSv;
        _UserSv = UserSv;
        _AuthSv = AuthSv;
        _tokenBuilder = tokenBuilder;
        _MemberSv = MemberSv;
    }
    #endregion

    /// <summary>
    /// Recupera los datos del usuario actualmente logueado.
    /// </summary>
    /// <response code="200">Devuelve las propiedades del usuario que está actualmente logueado.</response>
    /// <response code="401">No se dispone de la suficiente información en la petición para poder 
    /// determinar el usuario del cliente que ha realizado la petición.</response>
    [HttpGet("me")]
    public async Task<ActionResult> GetUserInfo()
    {
        var loggedUser = await _AuthSv.GetUserByClaim(User);
        if(loggedUser != null){
            return Ok(loggedUser);
        } else {
            return Unauthorized();
        }
    }

    #region Get
    /// <summary>
    /// Recupera los datos de un usuario en base a su Id.
    /// </summary>
    /// <response code="200">Devuelve las propiedades del usuario.</response>
    /// <response code="400">El usuario pedido en la petición no existe.</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        if(id != null)
        {
            var userbyid = await _UserSv.GetById(id);
            if (userbyid != null)
            {
                return Ok(userbyid);
            }
        }
        return NotFound("Not found");
    }

    /// <summary>
    /// Recupera un usuario en base a su 'nickname' o apodo online.
    /// </summary>
    /// <response code="200">Devuelve el usuario que coincide con el parámetro.</response>
    /// <response code="400">El usuario pedido en la petición no existe.</response>
    [HttpGet("{username}")]
    public async Task<ActionResult> GetByName(string username)
    {
        if(username != null)
        {
            var userbyname = await _UserSv.ListByName(username);
            if(userbyname != null)
            {
                return Ok(userbyname);
            }
        }
        return NotFound("Not found");
    }

    /// <summary>
    /// Recupera un usuario en base a su correo electrónico.
    /// </summary>
    /// <response code="200">Devuelve el usuario que coincide con el parámetro.</response>
    /// <response code="400">El usuario pedido en la petición no existe.</response>
    [HttpGet("{email}")]
    public async Task<ActionResult> GetByEmail(string email)
    {
        if(email != null)
        {
            var userbymail = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.IsActive == true);
            if(userbymail != null){
                return Ok(userbymail);
            }
        }
        return NotFound("Not found");
    }

    /// <summary>
    /// Recupera todos los miembros del usuario pedido.
    /// </summary>
    /// <response code="200">Devuelve una lista con todos las entidades miembro (propiedad de servidor) del usuario</response>
    /// <response code="400">El usuario pedido en la petición no existe.</response>
    [HttpGet("{id}/members")]
    public async Task<ActionResult> ListMemberByUserId(int id)
    {
        var result = await _MemberSv.ListByUserId(id);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Devuelve los miembros de un servidor en base a un usuario perteneciente al mismo.
    /// </summary>
    /// <response code="200">Devuelve una lista con miembros que pertenecen al servidor en el que está al usuario</response>
    /// <response code="400">El usuario o el servidor pedidos en la petición no existen.</response>
    [HttpGet("{userId}/servers/{serverId}/members")]
    public async Task<ActionResult> GetByServerIdAndByUserId(int userId, int serverId)
    {
        var result = await _MemberSv.GetByServerIdAndByUserId(userId, serverId);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest();
        }
    }
    #endregion

    #region Set
    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <response code="200">El usuario ha sido creado satisfactoriamente.</response>
    /// <response code="400">Los datos suministrados están mal formados.</response>
    [HttpPost("{id}")]
    public async Task<ActionResult> Add(UserDTO newUser)
    {
        var result = await this._UserSv.Add(newUser);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Error when updating or creating the user");
        }
    }

    /// <summary>
    /// Actualiza los datos de un usuario.
    /// </summary>
    /// <response code="200">Los datos del usuario han sido actualizados satisfactoriamente.</response>
    /// <response code="400">Los datos suministrados para actualizar al usuario están mal formados.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult> EditById(UserDTO newUser)
    {
        var result = await this._UserSv.EditById(newUser);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Error when updating or creating the user");
        }
    }
    #endregion

    #region Delete
    /// <summary>
    /// Elimina un usuario.
    /// </summary>
    /// <response code="200">El usuario ha sido eliminado satisfactoriamente.</response>
    /// <response code="400">Los datos suministrados para eliminar al usuario están mal formados..</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(int id)
    {
        var result = await this._UserSv.DeleteById(id);
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
