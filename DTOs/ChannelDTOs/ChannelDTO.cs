using disclone_api.DTOs.MessageDTOs;
using disclone_api.DTOs.ServerDTOs;
using disclone_api.Enums;

namespace disclone_api.DTOs.ChannelDTOs
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
}
