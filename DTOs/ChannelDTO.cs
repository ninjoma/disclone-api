using disclone_api.Enums;

namespace disclone_api.DTO
{
    public class ChannelDTO : BaseDTO
    {
        /// <summary>
        /// Identificador del servidor que contiene el canal.
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Nombre del canal.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tipo de canal.
        /// </summary>
        public ChannelTypeEnum Type { get; set; }
        /// <summary>
        /// Fecha de creación del canal.
        /// </summary>
        public DateTime CreationDate { get; set; }

    }
    
    public class ChannelDetailDTO : ChannelDTO 
    {
        /// <summary>
        /// Entidad de servidor que contiene el canal.
        /// </summary>
        public virtual ServerDTO? Server { get; set; }
        /// <summary>
        /// Lista de mensajes que han sido enviados por el canal.
        /// </summary>
        public virtual List<MessageDTO>? Messages { get; set; }
    }
}