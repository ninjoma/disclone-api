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
        public virtual UserDTO? User { get; set; }
        public virtual ChannelDTO? Channel { get; set; }
    }
}
