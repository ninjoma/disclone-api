using disclone_api.DTO;
using disclone_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IAuthService _AuthSv;

        private readonly IMemberService _MemberSv;

        private readonly IChannelService _ChannelSv;


        public ServerController(DataContext context, ILogger<ServerController> logger, IServerService ServerSv, IAuthService AuthSv, IMemberService MemberSv, IChannelService ChannelSv)
        {
            _context = context;
            _logger = logger;
            _ServerSv = ServerSv;
            _AuthSv = AuthSv;
            _MemberSv = MemberSv;
            _ChannelSv = ChannelSv;
        }
        #endregion


        [HttpPost("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> createServer(ServerDTO newServer)
        {
            if(newServer != null && newServer.Name != null && newServer.Name != ""){
                
                // Assign User Ownership
                var loggedUser = await _AuthSv.GetUserByClaim(User);
                newServer.OwnerId = loggedUser.Id;
                newServer.IsActive = true;

                // Create Server
                var createdServer = await _ServerSv.Add(newServer);

                // Joins User to Server
                var memberDTO = new MemberDTO();
                memberDTO.UserId = loggedUser.Id;
                memberDTO.ServerId = createdServer.Id;
                memberDTO.IsActive = true;
                await _MemberSv.Add(memberDTO);

                // Creates Default Channel
                var channelDTO = new ChannelDTO();
                channelDTO.ServerId = createdServer.Id;
                channelDTO.Name = "Default Channel";
                channelDTO.IsActive = true;
                await _ChannelSv.Add(channelDTO);

                // Returns Channel Id
                return Ok(createdServer.Id);
            }
            return BadRequest();
        }

        #region Get
        [HttpGet("/server/{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            Console.WriteLine("GetById");
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

        [HttpGet("")]
        public async Task<ActionResult> ListByName(string name)
        {
            Console.WriteLine(name);
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

        [HttpGet("{serverId}/channel")]
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

        [HttpGet("{serverId}/member")]
        public async Task<ActionResult> ListByserverId(int serverId)
        {
            var result = await _MemberSv.ListByServerId(serverId);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Set
        [HttpPost("")]
        public async Task<ActionResult> Add(ServerDTO newServer)
        {
            var result = await this._ServerSv.Add(newServer);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditById(ServerDTO newServer)
        {
            var result = await this._ServerSv.EditById(newServer);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var result = await this._ServerSv.DeleteById(id);
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
