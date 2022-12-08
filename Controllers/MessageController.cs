using disclone_api.DTOs.MessageDTOs;
using disclone_api.Services.AuthServices;
using disclone_api.Services.MemberServices;
using disclone_api.Services.MessageServices;
using Microsoft.AspNetCore.Mvc;

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

        public MessageController(DataContext context, ILogger<MessageController> logger, IMessageService MessageSv, IAuthService AuthSv)
        {
            _context = context;
            _logger = logger;
            _MessageSv = MessageSv;
            _AuthSv = AuthSv;
        }
        #endregion

        #region Set
        [HttpPost("AddEditAsync")]
        public async Task<IActionResult>AddEditAsync(MessageDTO message)
        {
            var result = await _MessageSv.AddEditAsync(message);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Get
        [HttpGet("GetById/{id}")]
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
        [HttpDelete("ToggleInactiveById/{id}")]
        public async Task<IActionResult> ToggleInactiveById(int id)
        {
            var result = await _MessageSv.ToggleInactiveById(id);
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
