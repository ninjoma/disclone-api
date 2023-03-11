namespace disclone_api.DTO
{
    public class MemberDTO : BaseDTO
    {
        /// <summary>
        /// Identificador del usuario que es miembro.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Identificador del servidor al que pertenece el miembro.
        /// </summary>
        public int ServerId { get; set; }
        /// <summary>
        /// Apodo del miembro en el servidor.
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// Fecha en la que el usuario se unió al servidor.
        /// </summary>
        public DateTime JoinDate { get; set; }
    }

    public class MemberDetailDTO : MemberDTO
    {
        /// <summary>
        /// Entidad usuario que representa al miembro.
        /// </summary>
        public virtual UserDTO? User { get; set; }
        /// <summary>
        /// Entidad servidor al que pertenece el servidor.
        /// </summary>
        public virtual ServerDTO? Server { get; set; }
    }
}
