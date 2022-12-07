using disclone_api.DTOs.InvitationDTOs;
using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.InvitationServices
{
    public interface IInvitationService
    {
        Task<InvitationDTO> GetById(int id, bool isActive = true);
        Task<List<InvitationDTO>> ListByUserId(int id, bool isActive = true);
        Task<InvitationDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<InvitationDTO>> ListByServerId(int id, bool isActive = true);
        Task<InvitationDTO> AddEditAsync(InvitationDTO invitation);
        Task<InvitationDTO> ToggleInactiveById(int id);
    }
}
