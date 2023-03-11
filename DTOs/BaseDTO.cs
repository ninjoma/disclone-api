namespace disclone_api.DTO
{
    public class BaseDTO : GenericDTO
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public int Id {get; set;}
        /// <summary>
        /// Si la entidad está activa (es un dato activo en la BBDD).
        /// </summary>
        public bool IsActive { get; set; }
    }

    public class GenericDTO
    {

    }
}