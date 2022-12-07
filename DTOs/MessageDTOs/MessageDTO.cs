using disclone_api.DTOs.ChannelDTOs;
using disclone_api.DTOs.UserDTOs;

namespace disclone_api.DTOs.MessageDTOs
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
