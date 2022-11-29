using disclone_api.Entities;
using disclone_api.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _UserSv;
    public UsersController(DataContext context, ILogger<UsersController> logger, IUserService UserSv)
    {
        _context = context;
        _logger = logger;
        _UserSv = UserSv;
    }

    [HttpGet("GetById/{id}")]
    public ActionResult GetById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("AddEditAsync")]
    public async Task<ActionResult> AddEditAsync(User newUser)
    {
        var result = await this._UserSv.AddEditAsync(newUser);
        if (result != null)
        {
            return Ok(result);
        } else
        {
            return BadRequest();
        }
    }

}
