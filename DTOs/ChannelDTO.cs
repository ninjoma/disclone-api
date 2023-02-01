using disclone_api.Enums;

namespace disclone_api.DTO
{
    public class ChannelDTO
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; }
        public ChannelTypeEnum Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

    }
    
    public class ChannelDetailDTO : ChannelDTO 
    {
        public virtual ServerDTO? Server { get; set; }
        public virtual List<MessageDTO>? Messages { get; set; }
    }
}