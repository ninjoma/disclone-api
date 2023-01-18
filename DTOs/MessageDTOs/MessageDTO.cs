namespace disclone_api.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string? Content { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
