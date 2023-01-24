﻿using disclone_api.Entities;
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
    private readonly IMemberService _MemberSv;
    public UserController(DataContext context, ILoggerService loggerSv, IUserService UserSv, ITokenBuilder tokenBuilder, IAuthService AuthSv, IMemberService MemberSv)
    {
        _context = context;
        _loggerSv = loggerSv;
        _UserSv = UserSv;
        _AuthSv = AuthSv;
        _tokenBuilder = tokenBuilder;
        _MemberSv = MemberSv;
    }
    #endregion

    [HttpGet("me")]
    public async Task<ActionResult> GetUserInfo()
    {
        var loggedUser = await _AuthSv.GetUserByClaim(User);
        return Ok(loggedUser);
    }

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

    [HttpGet("{id}/member")]
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
