﻿using disclone_api.DTOs.MemberDTOs;
using disclone_api.Services.MemberServices;
using disclone_api.Services.AuthServices;
using disclone_api.DTOs.MemberDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _MemberSv;
        private readonly IAuthService _AuthSv;
        
        public MemberController(DataContext context, ILogger<MemberController> logger, IMemberService MemberSv, IAuthService AuthSv)
        {
            _context = context;
            _logger = logger;
            _MemberSv = MemberSv;
            _AuthSv = AuthSv;
        }
        #endregion


        [HttpGet("fetchServers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> fetchServers()
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            var ServerList = await _MemberSv.ListByUserId(loggedUser.Id);
            if(ServerList != null){
                return Ok(ServerList);
            }
            return BadRequest();
        }

        [HttpGet("joinServer/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> joinServer(int id)
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            if((await _MemberSv.ListByServerId(id)).Count < 1){
                return BadRequest();
            }
            var memberDTO = new MemberDTO();
            memberDTO.UserId = loggedUser.Id;
            memberDTO.ServerId = id;
            memberDTO.IsActive = true;
            await _MemberSv.AddEditAsync(memberDTO);
            return Ok();
        }

        #region Get
        [HttpGet("/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _MemberSv.GetById(id);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByServerIdAndByUserId/{userId}/{serverId}")]
        public async Task<ActionResult> GetByServerIdAndByUserId(int userId, int serverId)
        {
            var result = await _MemberSv.GetByServerIdAndByUserId(userId, serverId);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("ListByserverId/{id}")]
        public async Task<ActionResult> ListByserverId(int id)
        {
            var result = await _MemberSv.ListByServerId(id);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }


        [HttpGet("ListByUserId/{id}")]
        public async Task<ActionResult> ListByUserId(int id)
        {
            var result = await _MemberSv.ListByUserId(id);
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
        [HttpPost("/{id}")]
        public async Task<ActionResult> EditById(MemberDTO member)
        {
            var result = await _MemberSv.EditById(member);
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
        public async Task<ActionResult> DeleteById(int id)
        {
            var result = await _MemberSv.DeleteById(id);
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
