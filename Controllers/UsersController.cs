using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;
using disclone_api.Services.UserServices;
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
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _UserSv;
    private readonly ITokenBuilder _tokenBuilder;
    public UsersController(DataContext context, ILogger<UsersController> logger, IUserService UserSv, ITokenBuilder tokenBuilder)
    {
        _context = context;
        _logger = logger;
        _UserSv = UserSv;
        _tokenBuilder = tokenBuilder;
    }
    #endregion

    #region Auth
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(UserDTO user)
    {
        var dbUser = _context.User.SingleOrDefault(u => u.Username == user.UserName);

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

        var username = User
            .Claims
            .SingleOrDefault();


        if (username == null)
        {
            return Unauthorized();
        }

        var userExists = _context.User.Any(u => u.Username == username.Value);

        if (!userExists)
        {
            return Unauthorized();
        }

        return NoContent();
    }
    #endregion

    #region Get
    [HttpGet("GetById/{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _UserSv.GetById(id);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
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
            return BadRequest();
        }
    }
    #endregion

    #region Set
    [HttpPost("AddEditAsync")]
    public async Task<ActionResult> AddEditAsync(UserDTO newUser)
    {
        var result = await this._UserSv.AddEditAsync(newUser);
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

    #region Delete
    [HttpDelete("ToggleInactiveById/{id}")]
    public async Task<ActionResult> ToggleInactiveById(int id)
    {
        var result = await this._UserSv.ToggleInactiveById(id);
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
