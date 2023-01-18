using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IInvitationService
    {
        Task<InvitationGridDTO> GetById(int id, bool isActive = true);
        Task<List<InvitationGridDTO>> ListByUserId(int id, bool isActive = true);
        Task<InvitationGridDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<InvitationGridDTO>> ListByServerId(int id, bool isActive = true);
        Task<InvitationDTO> AddEdit(InvitationDTO invitation);
        Task<InvitationDTO> Delete(int id);
    }
}
