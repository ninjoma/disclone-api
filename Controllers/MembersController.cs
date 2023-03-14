using disclone_api.DTO;
using disclone_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;



namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberService _MemberSv;
        private readonly IAuthService _AuthSv;
        
        public MembersController(DataContext context, ILogger<MembersController> logger, IMemberService MemberSv, IAuthService AuthSv)
        {
            _context = context;
            _logger = logger;
            _MemberSv = MemberSv;
            _AuthSv = AuthSv;
        }
        #endregion

        /// <summary>
        /// Devuelve las entidades que demuestran la pertenencia a los servidores (miembros)
        /// </summary>
        /// <response code="200">Devuelve la lista de miembros del usuario.</response>
        /// <response code="400">Error en la petición.</response>
        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> fetchServers()
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            if(loggedUser == null){
                return Unauthorized();
            }
            var ServerList = await _MemberSv.ListByUserId(loggedUser.Id);
            if(ServerList != null){
                return Ok(ServerList);
            }
            return BadRequest();
        }

        /// <summary>
        /// Une al usuario a un servidor
        /// </summary>
        /// <response code="200">El usuario se ha unido al servidor satisfactoriamente.</response>
        /// <response code="400">Error en la petición.</response>
        [HttpGet("me/servers/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> joinServer(int id)
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            
            if(await _MemberSv.ListByServerId(id) == null){
                return BadRequest();
            }

            var memberDTO = new MemberDTO{
                UserId = loggedUser.Id,
                ServerId = id,
                Nickname = loggedUser.Username,
                IsActive = true
            };

            await _MemberSv.Add(memberDTO);
            return Ok();
        }

        #region Get
        /// <summary>
        /// Recupera una entidad miembro (Demuestra propiedad de usuario en servidor)
        /// </summary>
        /// <response code="200">Devuelve información sobre el miembro por el ID suministrado.</response>
        /// <response code="400">No se ha podido encontrar la entidad miembro por su ID.</response>
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
        /// <summary>
        /// Editar una entidad miembro.
        /// </summary>
        /// <response code="200">La entidad miembro se ha editado satisfactoriamente.</response>
        /// <response code="400">No se ha podido encontrar la entidad miembro por su ID.</response>
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
        /// <summary>
        /// Eliminar una entidad miembro.
        /// </summary>
        /// <response code="200">La entidad miembro se ha eliminado satisfactoriamente.</response>
        /// <response code="400">No se ha podido encontrar la entidad miembro por su ID.</response>
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
