namespace disclone_api.DTO
{
    public class UserDTO : BaseDTO
    {
        /// <summary>
        /// Nombre de Usuario
        /// </summary>
        /// <example>Ninjam</example>
        public string? Username { get; set; }

        /// <summary>
        /// Email del Usuario
        /// </summary>
        /// <example>example_email@example.com</example>
        public string? Email { get; set; }

        /// <summary>
        /// Contraseña del Usuario.
        /// </summary>
        /// <example>l33t_h4ck3r</example>
        public string? Password {get; set; }
        
        /// <summary>
        /// Imagen que usa el usuario en la plataforma. Codificada en base64 para guardarse en base de datos relacional.
        /// </summary>
        /// <example>aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g/dj1kUXc0dzlXZ1hjUQ==</example>
        public  string? Image { get; set; }

        /// <summary>
        /// Fecha de creación del usuario. Autogenerado por el servidor.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }

    public class UserDetailDTO : UserDTO
    {
        /// <summary>
        /// Lista de las entidades miembro que pertenecen al usuario.
        /// </summary>
        public virtual List<MemberDTO>? Members { get; set; }

        /// <summary>
        /// Lista de las entidades invitación que pertenecen al usuario.
        /// </summary>
        public virtual List<InvitationDTO>? Invitations { get; set; }
        /// <summary>
        /// Lista de las entidades mensaje que el usuario ha enviado.
        /// </summary>
        public virtual List<MessageDTO>? Messages { get; set; }
        /// <summary>
        /// Lista de servidores en los que el usuario se encuentra actualmente.
        /// </summary>
        public virtual List<ServerDTO>? Servers { get; set; }
    }
}
