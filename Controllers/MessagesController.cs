using disclone_api.DTO;
using disclone_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _MessageSv;
        private readonly IAuthService _AuthSv;
        private readonly IChannelService _ChannelSv;
        private readonly IServerService _ServerSv;

        public MessagesController(DataContext context, ILogger<MessagesController> logger, IMessageService MessageSv, IAuthService AuthSv, IChannelService ChannelSv, IServerService ServerSv)
        {
            _context = context;
            _logger = logger;
            _MessageSv = MessageSv;
            _AuthSv = AuthSv;
            _ChannelSv = ChannelSv;
            _ServerSv = ServerSv;
        }
        #endregion

        /// <summary>
        /// Devuelve el canal al que pertenece el mensaje.
        /// </summary>
        /// <response code="200">Devuelve el canal del mensaje y sus propiedades.</response>
        /// <response code="400">No se ha podido recuperar el canal por el que se envió el mensaje.</response>
        [HttpGet("{id}/channels")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> getMessagesFromChannel(int id)
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            var channel = await _ChannelSv.GetById(id);
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
        /// <summary>
        /// Envia un mensaje por un canal de texto de un servidor.
        /// </summary>
        /// <response code="200">El mensaje ha sido enviado satisfactoriamente.</response>
        /// <response code="400">No se ha podido enviar el mensaje en el canal descrito en la petición.</response>
        [HttpPost("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> sendMessage(MessageDTO message)
        {
            var loggedUser = await _AuthSv.GetUserByClaim(User);
            message.UserId = loggedUser.Id;
            message.IsActive = true;

            var channel = await _ChannelSv.GetById(message.ChannelId);
            if(channel == null){
                return BadRequest();
            }
            
            var result = await _MessageSv.Add(message);
            if(result != null)
            {
                return Ok(result);
            } else {
                return BadRequest();
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Devuelve una entidad mensaje en base a su ID.
        /// </summary>
        /// <response code="200">El mensaje ha sido recuperado satisfactoriamente.</response>
        /// <response code="400">El mensaje que se ha intentado recuperar no existe.</response>
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

        /// <summary>
        /// Filtra un mensaje en base a su contenido un campo
        /// </summary>
        /// <response code="200">El mensaje ha sido recuperado satisfactoriamente.</response>
        /// <response code="400">El mensaje que se ha intentado filtrar no existe.</response>
        [HttpGet("")]
        public async Task<IActionResult> FilterByContent([FromQuery] string Content, [FromQuery] string orderby = "CreationDate")
        {
            var result = await _MessageSv.FilterByContent(Content, orderby);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("/channel/messages/{id}")]
        
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

        /// <summary>
        /// Recupera todos los mensajes de un usuario
        /// </summary>
        /// <response code="200">Devuelve una lista con todos los mensajes de un usuario.</response>
        /// <response code="400">El usuario suministrado en la petición no existe.</response>
        [HttpGet("users/{id}/messages")]
        public async Task<IActionResult> ListByUserId(int id)
        {
            var result = await _MessageSv.ListByUserId(id);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Elimina un mensaje
        /// </summary>
        /// <response code="200">La entidad mensaje ha sido eliminada satisfactoriamente.</response>
        /// <response code="400">No existe un mensaje con esa ID.</response>
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
