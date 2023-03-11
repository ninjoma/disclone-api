using disclone_api.Enums;
using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;
        public RolesController(DataContext context, ILogger<RolesController> logger){
            _context = context;
            _logger = logger;
        }

        private readonly ILogger<RolesController> _logger;

        [HttpGet("{id}")]
        public Role? Get(int id)
        {
            return _context.Role.Find(id);
        }

        [HttpPost("")]
        public void Create(string name, string color, RolePermissionsEnum permissions)
        {

            Role newRole = new Role() {
                Name = name,
                Color = color,
                Permits = permissions,
                CreationDate = DateTime.Now.ToUniversalTime(),
                IsActive = true
            };
            _context.Role.Add(newRole);
            _context.SaveChanges();
        }
    }

}