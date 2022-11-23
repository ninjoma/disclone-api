using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<StudentsController> _logger;
    public StudentsController(DataContext context, ILogger<StudentsController> logger){
        _context = context;
         _logger = logger;
    }



    [HttpGet(Name = "GetStudents")]
    public Student Get()
    {
            return _context.Students.Find(1);
    }



}
