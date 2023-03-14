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
public class AuthController : ControllerBase
{
    #region Constructor
    private readonly DataContext _context;
    private readonly IUserService _UserSv;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly IAuthService _AuthSv;
    private readonly ILoggerService _loggerSv;
    public AuthController(DataContext context, ILoggerService loggerSv, IUserService UserSv, ITokenBuilder tokenBuilder, IAuthService AuthSv)
    {
        _context = context;
        _loggerSv = loggerSv;
        _UserSv = UserSv;
        _AuthSv = AuthSv;
        _tokenBuilder = tokenBuilder;
    }
    #endregion

    #region Auth

    /// <summary>
    /// Crea un usuario
    /// </summary>
    /// <returns>Devuelve 200 si sale bien, 400 si la petición está mal formada.</returns>
    /// <response code="200">Usuario Creado</response>
    /// <response code="400">Consulta mal formada</response>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> Register(UserDTO newUser)
    {
        var result = await this._UserSv.Register(newUser);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest("Error when creating the user");
        }
    }

    /// <summary>
    /// Autentifica a un usuario
    /// </summary>
    /// <param name="newUser">El usuario representado por el modelo de datos UserDTO.</param>
    /// <response code="200">Usuario Logueado. Devuelve JWT como llave para autentificar las siguientes peticiones del usuario.</response>
    /// <response code="404">El usuario que representa esas credenciales no existe</response>
    /// <response code="401">Credenciales Incorrectos.</response>
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(UserDTO user)
    {
        var dbUser = _context.User.FirstOrDefault(u => u.Username == user.Username);

        if (dbUser == null)
        {
            return NotFound("User not found.");
        }

        var isValid = dbUser.Password == DCrypt.Encrypt(user.Password);
        if (!isValid)
        {
            return Unauthorized("Could not authenticate user.");
        }
    
        var token = _tokenBuilder.BuildToken(dbUser.Id);

        return Ok(token);
    }

    /// <summary>
    /// Verifica la validez de un token de usuario (JWT)
    /// </summary>
    /// <param name="newUser">El usuario representado por el modelo de datos UserDTO.</param>
    /// <response code="204">El token es válido. Respuesta vacía.</response>
    /// <response code="401">El token JWT ha caducado.</response>
    [HttpGet("verify")]
    public IActionResult VerifyToken()
    {

        var userid = User
            .Claims
            .SingleOrDefault();


        if (userid == null)
        {
            return Unauthorized();
        }

        var userExists = _context.User.Any(u => u.Id == Int32.Parse(userid.Value));

        if (!userExists)
        {
            return Unauthorized();
        }

        return NoContent();
    }
    #endregion


}
