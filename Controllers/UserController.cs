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
public class UserController : ControllerBase
{
    #region Constructor
    private readonly DataContext _context;
    private readonly IUserService _UserSv;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly IAuthService _AuthSv;
    private readonly ILoggerService _loggerSv;
    public UserController(DataContext context, ILoggerService loggerSv, IUserService UserSv, ITokenBuilder tokenBuilder, IAuthService AuthSv)
    {
        _context = context;
        _loggerSv = loggerSv;
        _UserSv = UserSv;
        _AuthSv = AuthSv;
        _tokenBuilder = tokenBuilder;
    }
    #endregion

    [HttpGet("getUserInfo")]
    public async Task<ActionResult> GetUserInfo()
    {
        var loggedUser = await _AuthSv.GetUserByClaim(User);
        return Ok(loggedUser);
    }

    #region Auth
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(UserDTO user)
    {
        var dbUser = _context.User.FirstOrDefault(u => u.Username == user.UserName);

        if (dbUser == null)
        {
            return NotFound("User not found.");
        }

        var isValid = dbUser.Password == DCrypt.Encrypt(user.Password);
        if (!isValid)
        {
            return BadRequest("Could not authenticate user.");
        }
    
        var token = _tokenBuilder.BuildToken(dbUser.Id);

        return Ok(token);
    }

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

    #region Get
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _UserSv.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return NotFound("Not found");
        }
    }

    [HttpGet("UserNameExist/{username}")]
    public async Task<ActionResult> UserNameExist(string username)
    {
        if (await _context.User.FirstOrDefaultAsync(x => x.Username.Equals(username) && x.IsActive == true) != null)
        {
            return Ok(true);
        } else
        {
            return Ok(false);
        }
    }

    [HttpGet("EmailExist/{email}")]
    public async Task<ActionResult> EmailExist(string email)
    {
        if (await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.IsActive == true) != null)
        {
            return Ok(true);
        } else
        {
            return Ok(false);
        }
    }

    [HttpGet("ListByName")]
    
    public async Task<ActionResult> ListByName(string name)
    {
        var result = await _UserSv.ListByName(name);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return NotFound("Not found");
        }
    }
    #endregion

    #region Set
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

    [HttpPost("Register")]
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

    #endregion

    #region Delete
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
