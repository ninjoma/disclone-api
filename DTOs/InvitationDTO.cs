namespace disclone_api.DTO
{
    public class InvitationDTO : BaseDTO
    {
        /// <summary>
        /// Identificador del servidor en el que se envió la invitación.
        /// </summary>
        public int ServerId { get; set; }
        /// <summary>
        /// Identificador del usuario que recibe la petición.
        /// </summary>
        public int Receiver { get; set; }
        /// <summary>
        /// Url que cuando un usuario pulsa, entra automáticamente al servidor asociado con la invitación.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// Fecha de creación de la invitación.
        /// </summary>
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Fecha de expiración de la invitación.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }

    public class InvitationDetailDTO : InvitationDTO
    {
        /// <summary>
        /// Entidad de usuario que ha creado la invitación.
        /// </summary>
        public virtual UserDTO? User { get; set; }
        /// <summary>
        /// Entidad de servidor que representa la invitación.
        /// </summary>
        public virtual ServerDTO? Server { get; set; }
    }
}
