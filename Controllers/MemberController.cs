using disclone_api.DTO;
using disclone_api.Services;
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


        [HttpGet("me")]
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

        [HttpGet("me/server/{id}")]
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
            await _MemberSv.Add(memberDTO);
            return Ok();
        }

        #region Get
        [HttpGet("{id}")]
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
        #endregion

        #region Set
        [HttpPost("{id}")]
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
        [HttpDelete("{id}")]
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
