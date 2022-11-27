using Microsoft.AspNetCore.Mvc;


namespace disclone_api.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _context;
        public RoleController(DataContext context, ILogger<RoleController> logger){
            _context = context;
            _logger = logger;
        }

        private readonly ILogger<RoleController> _logger;

        [HttpGet("{id}")]
        public Role? Get(int id)
        {
            return _context.Role.Find(id);
        }

        [HttpPost("create/")]
        public void Create(string name, string color, Permissions permissions)
        {

            Role newRole = new Role() {
                Name = name,
                Color = color,
                Permits = permissions,
                CreationDate = DateTime.Now.ToUniversalTime(),
                isActive = true
            };
            _context.Role.Add(newRole);
            _context.SaveChanges();
        }
    }

}