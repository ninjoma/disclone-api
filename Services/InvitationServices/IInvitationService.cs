using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IInvitationService : IMainService<InvitationDTO, InvitationDetailDTO>
    {
        Task<List<InvitationDTO>> ListByUserId(int id, bool isActive = true);
        Task<InvitationDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<InvitationDTO>> ListByServerId(int id, bool isActive = true);
    }
}