using disclone_api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    #region Propiedades Privadas y Constructor
    private readonly DataContext _context;
    private readonly ILogger<StudentsController> _logger;
    public UsersController(DataContext context, ILogger<StudentsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    #endregion

    #region Get
    [HttpGet("GetById/{id}")]
    public ActionResult GetById(int id)
    {
        throw new NotImplementedException();
    }
    #endregion

}
