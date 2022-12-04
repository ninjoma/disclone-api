using AutoMapper;
using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.MemberServices
{
    public class MemberService : IMemberService
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MemberService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        public Task<MemberDTO> AddEditAsync(MemberDTO server)
        {
            throw new NotImplementedException();
        }

        public Task<MemberDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId)
        {
            throw new NotImplementedException();
        }

        public Task<MemberDTO> ListByServerId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MemberDTO> ListByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MemberDTO> ToggleInactiveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
