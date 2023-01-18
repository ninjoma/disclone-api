using disclone_api.DTO;
using disclone_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageService _MessageSv;
        private readonly IAuthService _AuthSv;
        private readonly IChannelService _ChannelSv;
        private readonly IServerService _ServerSv;

        public MessageController(DataContext context, ILogger<MessageController> logger, IMessageService MessageSv, IAuthService AuthSv, IChannelService ChannelSv, IServerService ServerSv)
        {
            _context = context;
            _logger = logger;
            _MessageSv = MessageSv;
            _AuthSv = AuthSv;
            _ChannelSv = ChannelSv;
            _ServerSv = ServerSv;
        }
        #endregion

        [HttpGet("getMessagesFromChannel/{id}")]
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

        #region Set
        [HttpPost("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> sendMessage(MessageDTO message)
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            message.UserId = loggedUser.Id;
            message.IsActive = true;
            var result = await _MessageSv.AddEditAsync(message);
            if(result != null)
            {
                return Ok(result);
            } else {
                return BadRequest();
            }
        }
        #endregion

        #region Get
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _MessageSv.GetById(id);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("ListByChannelId/{id}")]
        public async Task<IActionResult> ListByChannelId(int id)
        {
            var result = await _MessageSv.ListByChannelId(id);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("ListByUserId/{id}")]
        public async Task<IActionResult> ListByUserId(int id)
        {
            var result = await _MessageSv.ListByUserId(id);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _MessageSv.DeleteById(id);
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
