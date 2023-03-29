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
    public class ServersController: ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger _logger;
        private readonly IServerService _ServerSv;
        private readonly IAuthService _AuthSv;

        private readonly IMemberService _MemberSv;

        private readonly IChannelService _ChannelSv;


        public ServersController(DataContext context, ILogger<ServersController> logger, IServerService ServerSv, IAuthService AuthSv, IMemberService MemberSv, IChannelService ChannelSv)
        {
            _context = context;
            _logger = logger;
            _ServerSv = ServerSv;
            _AuthSv = AuthSv;
            _MemberSv = MemberSv;
            _ChannelSv = ChannelSv;
        }
        #endregion

        /// <summary>
        /// Crea un servidor nuevo en base al usuario.
        /// </summary>
        /// <response code="200">Devuelve una lista llena de todos los servidores en los que está el usuario.</response>
        /// <response code="400">El usuario no existe o no se ha logueado correctamente. (Su token es inválido)</response>
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
                memberDTO.Nickname = loggedUser.Username;
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
        /// <summary>
        /// Recupera el servidor en base a su ID.
        /// </summary>
        /// <response code="200">Devuelve las propiedades del servidor pedido.</response>
        /// <response code="404">El servidor demandado por la petición no existe.</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _ServerSv.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Recupera una lista de servidores en base al nombre.
        /// </summary>
        /// <response code="200">Devuelve una lista de servidores.</response>
        /// <response code="404">No existe ningún servidor que contenga ese nombre.</response>
        [HttpGet("{name}")]
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
                return NotFound();
            }
        }

        /// <summary>
        /// Recupera todos los canales de un servidor.
        /// </summary>
        /// <response code="200">Devuelve una lista con todos los canales de un servidor.</response>
        /// <response code="404">No existe el servidor suministrado por la petición.</response>
        [HttpGet("{serverId}/channels")]
        public async Task<IActionResult> ListByServer(int serverId)
        {
            var result = await _ChannelSv.ListByServer(serverId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Lista a todos los miembros de un servidor.
        /// </summary>
        /// <response code="200">Devuelve una lista de miembros que pertenecen al servidor.</response>
        /// <response code="404">No existe el servidor descrito por la petición.</response>
        [HttpGet("{serverId}/members")]
        public async Task<ActionResult> ListByserverId(int serverId)
        {
            var result = await _MemberSv.ListByServerId(serverId);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        #endregion

        #region Set
        /// <summary>
        /// Crea un servidor en base a los datos de la petición.
        /// </summary>
        /// <response code="200">El servidor ha sido creado satisfactoriamente.</response>
        /// <response code="400">El servidor pedido en la petición no existe.</response>
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
        
        /// <summary>
        /// Edita los datos de un servidor.
        /// </summary>
        /// <response code="200">El servidor ha sido editado satisfactoriamente.</response>
        /// <response code="400">El servidor pedido en la petición no existe.</response>
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
        /// <summary>
        /// Elimina los datos de un servidor.
        /// </summary>
        /// <response code="200">El servidor ha sido eliminado satisfactoriamente.</response>
        /// <response code="400">El servidor pedido en la petición no existe.</response>
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
