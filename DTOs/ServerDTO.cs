namespace disclone_api.DTO
{
    public class ServerDTO : BaseDTO
    {
        /// <summary>
        /// Identificador del usuario que creó el servidor.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Nombre del servidor.
        /// </summary>
        /// <example>Sample Server Name</example>
        public string Name { get; set; }

        /// <summary>
        /// Fecha de creación del servidor.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }

    public class ServerDetailDTO : ServerDTO
    {
        /// <summary>
        /// Lista de miembros que pertenecen al servidor.
        /// </summary>
        public virtual List<MemberDTO>? Members { get; set; }

        /// <summary>
        /// Lista de invitaciones creadas para unirse al servidor.
        /// </summary>
        public virtual List<InvitationDTO>? Invitations { get; set; }
    }
}
