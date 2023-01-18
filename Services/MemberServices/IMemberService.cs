using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.MemberServices
{
    public interface IMemberService
    {
        Task<MemberGridDTO> GetById(int id, bool isActive = true);
        Task<List<MemberGridDTO>> ListByUserId(int id, bool isActive = true);
        Task<MemberGridDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<MemberGridDTO>> ListByServerId(int id, bool isActive = true);
        Task<MemberDTO> Add(MemberDTO member);
        Task<MemberDTO> EditById(MemberDTO member);
        Task<MemberDTO> Delete(int id);
    }
}
