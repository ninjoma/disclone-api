using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class InvitationsController : ControllerBase
{
    #region Propiedades Privadas y Constructor
    private readonly DataContext _context;
    private readonly ILogger<StudentsController> _logger;
    public InvitationsController(DataContext context, ILogger<StudentsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    #endregion

    #region Get
    [HttpGet("GetById/{id}")]
    public ActionResult GetById()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Set
    [HttpPost("AddEditAsync")]
    public ActionResult AddEditAsync()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Delete
    [HttpDelete("Delete")]
    public ActionResult Delete()
    {
        throw new NotImplementedException();
    }
    #endregion
}
