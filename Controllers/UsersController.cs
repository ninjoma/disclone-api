using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<UsersController> _logger;
    public UsersController(DataContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("GetById/{id}")]
    public ActionResult GetById(int id)
    {
        throw new NotImplementedException();
    }

}
