namespace disclone_api.DTO
{
    public class MessageDTO : BaseDTO
    {
        /// <summary>
        /// Identificador del usuario que al que pertenece el mensaje.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Identificador del canal donde se envió el mensaje.
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// Contenido del mensaje.
        /// </summary>
        /// <example>Sample text</example>
        public string? Content { get; set; }
        /// <summary>
        /// Fecha en la que se creó el mensaje.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }

    public class MessageDetailDTO : MessageDTO
    {
        /// <summary>
        /// Entidad usuario que creó el mensaje.
        /// </summary>
        public virtual UserDTO? User { get; set; }
        /// <summary>
        /// Entidad canal por la que se envió el mensaje.
        /// </summary>
        public virtual ChannelDTO? Channel { get; set; }
    }
}
