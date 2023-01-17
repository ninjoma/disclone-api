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
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ChannelSv.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/{id}/server")]
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
        [HttpPut("/{id}")]
        public async Task<IActionResult> EditById(ChannelDTO channel)
        {
            var result = await _ChannelSv.EditById(channel);
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
        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _ChannelSv.DeleteById(id);
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
