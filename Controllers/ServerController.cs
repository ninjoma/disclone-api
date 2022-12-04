using disclone_api.DTOs.ServerDTOs;
using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;
using disclone_api.Services.ServerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController: ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger _logger;
        private readonly IServerService _ServerSv;
        public ServerController(DataContext context, ILogger<ServerController> logger, IServerService ServerSv)
        {
            _context = context;
            _logger = logger;
            _ServerSv = ServerSv;
        }
        #endregion

        #region Get
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _ServerSv.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("ListByName")]
        public async Task<ActionResult> ListByName(string name)
        {
            var result = await _ServerSv.ListByName(name);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Set
        [HttpPost("AddEditAsync")]
        public async Task<ActionResult> AddEditAsync(ServerDTO newServer)
        {
            var result = await this._ServerSv.AddEditAsync(newServer);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("ToggleInactiveById/{id}")]
        public async Task<ActionResult> ToggleInactiveById(int id)
        {
            var result = await this._ServerSv.ToggleInactiveById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
