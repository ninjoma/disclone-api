using disclone_api.DTOs.InvitationDTOs;
using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.InvitationServices
{
    public interface IInvitationService
    {
        // TODO: Implementar la busqueda por inactivos
        Task<InvitationDTO> GetById(int id);
        Task<List<InvitationDTO>> ListByUserId(int id);
        Task<InvitationDTO> GetByServerIdAndByUserId(int userId, int serverId);
        Task<List<InvitationDTO>> ListByServerId(int id);
        Task<InvitationDTO> AddEditAsync(InvitationDTO invitation);
        Task<InvitationDTO> ToggleInactiveById(int id);
    }
}
