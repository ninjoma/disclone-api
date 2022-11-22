using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private DataContext _context;
    public StudentsController(DataContext context, ILogger<StudentsController> logger){
        _context = context;
         _logger = logger;
    }

    private readonly ILogger<StudentsController> _logger;


    [HttpGet(Name = "GetStudents")]
    public Student Get()
    {

            return _context.Students.Find(1);
    }



}
