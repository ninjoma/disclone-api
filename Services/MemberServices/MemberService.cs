using AutoMapper;
using disclone_api.DTOs.MemberDTOs;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

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

        #region Set
        public async Task<MemberDTO> AddEditAsync(MemberDTO member)
        {
            if (member.Id != 0)
            {
                return await UpdateMemberAsync(member);
            }
            else
            {
                return await CreateMemberAsync(member);
            }
        }
        public async Task<MemberDTO> CreateMemberAsync(MemberDTO member)
        {
            var newMember = _mapper.Map<Member>(member);
            await _context.Member.AddAsync(newMember);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(newMember);
        }
        public async Task<MemberDTO> UpdateMemberAsync(MemberDTO member)
        {
            var oldMember = await _context.Member.FirstOrDefaultAsync(x => x.Id.Equals(member.Id));
            _mapper.Map<MemberDTO, Member>(member,oldMember);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(oldMember);
        }
        #endregion

        #region Get
        public async Task<MemberGridDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<MemberGridDTO>(await _context.Member
                .Include(x => x.User)
                .Include(x => x.Server)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<MemberGridDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true)
        {
            return _mapper.Map<MemberGridDTO>(await _context.Member
                .Include(x => x.User)
                .Include(x => x.Server)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId) 
                && x.ServerId.Equals(serverId) 
                && x.IsActive == isActive));
        }

        public async Task<List<MemberGridDTO>> ListByServerId(int id, bool isActive = true)
        {
            return _mapper.Map<List<MemberGridDTO>>(await _context.Member
                .Where(x => x.ServerId.Equals(id) && x.IsActive == isActive)
                .Include(x => x.User)
                .Include(x => x.Server)
                .ToListAsync());
        }

        public async Task<List<MemberGridDTO>> ListByUserId(int id, bool isActive = true)
        {
            return _mapper.Map<List<MemberGridDTO>>(await _context.Member
                .Where(x => x.UserId.Equals(id) && x.IsActive == isActive)
                .Include(x => x.User)
                .Include(x => x.Server)
                .ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<MemberDTO> ToggleInactiveById(int id)
        {
            var member = await _context.Member.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (member.IsActive)
            {
                member.IsActive = false;
            }
            else
            {
                member.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(member);
        } 
        #endregion
    }
}
