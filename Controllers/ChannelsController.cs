﻿using disclone_api.DTO;
using disclone_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace disclone_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelsController : ControllerBase
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly ILogger<ChannelsController> _logger;
        private readonly IChannelService _ChannelSv;
        private readonly IServerService _ServerSv;
        private readonly IMessageService _MessageSv;
        private readonly IAuthService _AuthSv;

        public ChannelsController(DataContext context, ILogger<ChannelsController> logger, IChannelService ChannelSv, IServerService ServerSv, IMessageService MessageSv, IAuthService AuthSv)
        {
            _context = context;
            _logger = logger;
            _ChannelSv = ChannelSv;
            _ServerSv = ServerSv;
            _MessageSv = MessageSv;
            _AuthSv = AuthSv;
        }
        #endregion

        /// <summary>
        /// Añade un canal a un servidor
        /// </summary>
        /// <response code="200">Los datos del canal con el ID especificado</response>
        /// <response code="400">No se puede crear un canal con los datos especificados</response>
        [HttpPost("/servers/{id}/channels")]
        public async Task<IActionResult> Add(int id, ChannelDTO channel)
        {
            channel.ServerId = id;
            var result = await _ChannelSv.Add(channel);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        #region Get
        /// <summary>
        /// Recupera un canal de chat o texto
        /// </summary>
        /// <response code="200">Los datos del canal con el ID especificado</response>
        /// <response code="404">El canal no existe</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ChannelSv.GetById(id);
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
        /// Recupera los mensajes y sus datos a partir de la id de un canal
        /// </summary>
        /// <response code="200">Una lista de mensajes de un canal.</response>
        /// <response code="400">El servidor o el canal no existe.</response>
        /// <response code="404">No existe un mensaje con esa ID.</response>
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> getMessagesFromChannel(int id, [FromQuery] string Content, [FromQuery] string orderby = "CreationDate")
        {
            return Ok(await _MessageSv.FilterByChannelContent(id, Content, orderby));
        }
        #endregion

        #region Set
        /// <summary>
        /// Edita un canal por su id
        /// </summary>
        /// <response code="200">El canal ha sido editado satifactoriamente.</response>
        /// <response code="400">El canal no existe.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditById(ChannelDTO channel)
        {
            var result = await _ChannelSv.EditById(channel);
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
        /// Eliminar un canal por su id
        /// </summary>
        /// <response code="200">El canal ha sido eliminado satfactoriamente.</response>
        /// <response code="400">El canal no existe.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _ChannelSv.DeleteById(id);
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
