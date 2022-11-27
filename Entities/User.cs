namespace disclone_api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; } 
    }
}
