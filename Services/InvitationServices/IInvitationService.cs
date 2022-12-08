using disclone_api.DTOs.InvitationDTOs;
using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.InvitationServices
{
    public interface IInvitationService
    {
        Task<InvitationGridDTO> GetById(int id, bool isActive = true);
        Task<List<InvitationGridDTO>> ListByUserId(int id, bool isActive = true);
        Task<InvitationGridDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<InvitationGridDTO>> ListByServerId(int id, bool isActive = true);
        Task<InvitationDTO> AddEditAsync(InvitationDTO invitation);
        Task<InvitationDTO> ToggleInactiveById(int id);
    }
}
