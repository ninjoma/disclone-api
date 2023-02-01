namespace disclone_api.DTO
{
    public class MessageDTO : BaseDTO
    {
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string? Content { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class MessageDetailDTO : MessageDTO
    {
        public virtual UserDTO? User { get; set; }
        public virtual ChannelDTO? Channel { get; set; }
    }
}
