using AutoMapper;
using disclone_api.DTO;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services
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
        public async Task<MemberDTO> Add(MemberDTO member)
        {
            var newMember = _mapper.Map<Member>(member);
            await _context.Member.AddAsync(newMember);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(newMember);
        }
        public async Task<MemberDTO> EditById(MemberDTO member)
        {
            var oldMember = await _context.Member.FirstOrDefaultAsync(x => x.Id.Equals(member.Id));
            _mapper.Map<MemberDTO, Member>(member,oldMember);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(oldMember);
        }
        #endregion

        #region Get
        public async Task<MemberDetailDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<MemberDetailDTO>(await _context.Member
                .Include(x => x.User)
                .Include(x => x.Server)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true)
        {
            return _mapper.Map<MemberDTO>(await _context.Member
                .Include(x => x.User)
                .Include(x => x.Server)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId) 
                && x.ServerId.Equals(serverId) 
                && x.IsActive == isActive));
        }

        public async Task<List<MemberDTO>> ListByServerId(int id, bool isActive = true)
        {
            return _mapper.Map<List<MemberDTO>>(await _context.Member
                .Where(x => x.ServerId.Equals(id) && x.IsActive == isActive)
                .Include(x => x.User)
                .Include(x => x.Server)
                .ToListAsync());
        }

        public async Task<List<MemberDTO>> ListByUserId(int id, bool isActive = true)
        {
            return _mapper.Map<List<MemberDTO>>(await _context.Member
                .Where(x => x.UserId.Equals(id) && x.IsActive == isActive)
                .Include(x => x.User)
                .Include(x => x.Server)
                .ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<MemberDTO> DeleteById(int id)
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
