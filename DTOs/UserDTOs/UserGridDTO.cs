using disclone_api.DTOs.InvitationDTOs;
using disclone_api.DTOs.MemberDTOs;
using disclone_api.DTOs.MessageDTOs;
using disclone_api.DTOs.ServerDTOs;

namespace disclone_api.DTOs.UserDTOs
{
    public class UserGridDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<MemberDTO>? Members { get; set; }
        public virtual List<InvitationDTO>? Invitations { get; set; }
        public virtual List<MessageDTO>? Messages { get; set; }
        public virtual List<ServerDTO>? Servers { get; set; }
    }
}
