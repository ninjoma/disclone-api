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
            await _context.Member.AddAsync(_mapper.Map<Member>(member));
            await _context.SaveChangesAsync();
            return member;
        }
        public async Task<MemberDTO> UpdateMemberAsync(MemberDTO member)
        {
            var oldMember = await _context.Member.FirstOrDefaultAsync(x => x.Id.Equals(member.Id));
            oldMember = _mapper.Map<Member>(member);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(oldMember);
        }
        #endregion

        #region Get
        public async Task<MemberDTO> GetById(int id)
        {
            return _mapper.Map<MemberDTO>(await _context.Member
                .Include(x => x.User)
                .Include(x => x.Server)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == true));
        }

        public async Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId)
        {
            return _mapper.Map<MemberDTO>(await _context.Member
                .Include(x => x.User)
                .Include(x => x.Server)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId) 
                && x.ServerId.Equals(serverId) 
                && x.IsActive == true));
        }

        public async Task<List<MemberDTO>> ListByServerId(int id)
        {
            return _mapper.Map<List<MemberDTO>>(await _context.Member
                .Where(x => x.ServerId.Equals(id) && x.IsActive == true)
                .Include(x => x.User)
                .Include(x => x.Server)
                .ToListAsync());
        }

        public async Task<List<MemberDTO>> ListByUserId(int id)
        {
            return _mapper.Map<List<MemberDTO>>(await _context.Member
                .Where(x => x.UserId.Equals(id) && x.IsActive == true)
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
