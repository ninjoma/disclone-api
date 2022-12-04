using disclone_api.DTOs.ServerDTOs;
using disclone_api.DTOs.UserDTOs;

namespace disclone_api.DTOs.InvitationDTOs
{
    public class InvitationDTO
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public int Receiver { get; set; }
        public string? Url { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual UserDTO? User { get; set; }
        public virtual ServerDTO? Server { get; set; }
    }
}
