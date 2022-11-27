using Microsoft.AspNetCore.Mvc;
using disclone_api.Entities;

namespace disclone_api.Controllers;
[ApiController]
[Route("[controller]")]
public class InvitationsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<StudentsController> _logger;
    public InvitationsController(DataContext context, ILogger<StudentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public Invitation? GetById(int id)
    {  
        return _context.Invitation.Find(id);
    }

    [HttpPost("create/")]
    public void Create(int serverId, int Receiver, string url)
    {
        Invitation invitation = new Invitation() {
            ServerId = serverId,
            Receiver = Receiver,
            Url = url,
            CreationDate = DateTime.Now.ToUniversalTime(),
            IsActive = true,
        };

        _context.Invitation.Add(invitation);
        _context.SaveChanges();
    }

}
