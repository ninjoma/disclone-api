namespace disclone_api.DTO
{
    public class UserDTO
    {

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password {get; set; }
        public  string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
