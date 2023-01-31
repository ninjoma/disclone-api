using disclone_api.DTO;
using disclone_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ChannelController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<ChannelController> _logger;
        private readonly ChannelService _ChannelSv;
        private readonly ServerService _ServerSv;
        private readonly MessageService _MessageSv;

        public ChannelController(DataContext context, ILogger<ChannelController> logger, IChannelService ChannelSv, IServerService ServerSv, IMessageService MessageSv)
        {
            _context = context;
            _logger = logger;
            _ChannelSv = ChannelSv;
            _ServerSv = ServerSv;
            _MessageSv = MessageSv;
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
                return NotFound();
            }
        }

        [HttpGet("{id}/message")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> getMessagesFromChannel(int id)
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            var channel = await _ChannelSv.GetByIdAsync(id);
            if(channel == null){
                return BadRequest();
            }
            var server = await _ServerSv.GetById(channel.ServerId);
            if(server == null){
                return BadRequest();
            }
            if(server.Members.Any(current => current.UserId == loggedUser.Id)){
                return Ok(await _MessageSv.ListByChannelId(id));
            }
            return BadRequest();
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
