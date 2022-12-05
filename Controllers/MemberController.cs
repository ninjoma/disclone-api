using disclone_api.DTOs.MemberDTOs;
using disclone_api.Services.MemberServices;
using disclone_api.Services.UserServices;
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
        private readonly IUserService _UserSv;
        public MemberController(DataContext context, ILogger<MemberController> logger, IMemberService MemberSv, IUserService UserSv)
        {
            _context = context;
            _logger = logger;
            _MemberSv = MemberSv;
            _UserSv = UserSv;
        }
        #endregion


        [HttpGet("fetchServers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async void fetchServers()
        {
            


        }



        #region Get
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
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
        [HttpPost("AddEditAsync")]
        public async Task<ActionResult> AddEditAsync(MemberDTO member)
        {
            var result = await _MemberSv.AddEditAsync(member);
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
        public async Task<ActionResult> ToggleInactiveById(int id)
        {
            var result = await _MemberSv.ToggleInactiveById(id);
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
