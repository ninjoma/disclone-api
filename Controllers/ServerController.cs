using disclone_api.DTOs.ServerDTOs;
using disclone_api.DTOs.UserDTOs;
using disclone_api.DTOs.MemberDTOs;
using disclone_api.DTOs.ChannelDTOs;
using disclone_api.Entities;
using disclone_api.Services.ServerServices;
using disclone_api.Services.AuthServices;
using disclone_api.Services.MemberServices;
using disclone_api.Services.ChannelServices;
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


        [HttpPost("/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> createServer(ServerDTO newServer)
        {
            if(newServer != null && newServer.Name != null && newServer.Name != ""){
                var loggedUser = await _AuthSv.GetUserByClaim(User);
                newServer.OwnerId = loggedUser.Id;
                newServer.IsActive = true;
                var createdServer = await _ServerSv.AddEditAsync(newServer);

                var memberDTO = new MemberDTO();
                memberDTO.UserId = loggedUser.Id;
                memberDTO.ServerId = createdServer.Id;
                memberDTO.IsActive = true;
                await _MemberSv.AddEditAsync(memberDTO);

                var channelDTO = new ChannelDTO();
                channelDTO.ServerId = createdServer.Id;
                channelDTO.Name = "Default Channel";
                channelDTO.IsActive = true;
                await _ChannelSv.AddEditAsync(channelDTO);

                return Ok(createdServer.Id);
            }
            return BadRequest();
        }

        #region Get
        [HttpGet("{id}")]
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
        [HttpPost("{id}")]
        public async Task<ActionResult> AddById(ServerDTO newServer)
        {
            var result = await this._ServerSv.AddById(newServer);
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
        [HttpDelete("{id}/")]
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
