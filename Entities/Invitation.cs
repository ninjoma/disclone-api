namespace disclone_api.Entities
{
    public class Invitation
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public int Receiver { get; set; }
        public string? Url { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
