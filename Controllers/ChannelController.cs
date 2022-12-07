using disclone_api.DTOs.ChannelDTOs;
using disclone_api.Services.ChannelServices;
using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<ChannelController> _logger;
        private readonly IChannelService _ChannelSv;
        public ChannelController(DataContext context, ILogger<ChannelController> logger, IChannelService UserSv)
        {
            _context = context;
            _logger = logger;
            _ChannelSv = UserSv;
        }
        #endregion

        #region Get
        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ChannelSv.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("ListByServer/{serverId}")]
        public async Task<IActionResult> ListByServer(int serverId)
        {
            var result = await _ChannelSv.ListByServer(serverId);
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
        public async Task<IActionResult> AddEditAsync(ChannelDTO channel)
        {
            var result = await _ChannelSv.AddEditAsync(channel);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("ToggleInactiveById/{id}")]
        public async Task<IActionResult> ToggleInactiveById(int id)
        {
            var result = await _ChannelSv.ToggleInactiveById(id);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
