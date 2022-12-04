using AutoMapper;
using disclone_api.DTOs.InvitationDTOs;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.InvitationServices
{
    public class InvitationService : IInvitationService
    {

        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public InvitationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Set
        public async Task<InvitationDTO> AddEditAsync(InvitationDTO invitation)
        {
            if (invitation.Id != 0)
            {
                return await UpdateInvitationAsync(invitation);
            }
            else
            {
                return await CreateInvitationAsync(invitation);
            }

        }
        public async Task<InvitationDTO> UpdateInvitationAsync(InvitationDTO invitation)
        {
            var oldInvitation = await _context.Invitation.FirstOrDefaultAsync(x => x.Id.Equals(invitation.Id));
            oldInvitation = _mapper.Map<Invitation>(invitation);
            await _context.SaveChangesAsync();
            return _mapper.Map<InvitationDTO>(oldInvitation);
        }

        public async Task<InvitationDTO> CreateInvitationAsync(InvitationDTO invitation)
        {
            await _context.Invitation.AddAsync(_mapper.Map<Invitation>(invitation));
            await _context.SaveChangesAsync();
            return invitation;
        }
        #endregion

        #region Get
        public async Task<InvitationDTO> GetById(int id)
        {
            return _mapper.Map<InvitationDTO>(await _context.Invitation.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == true));
        }

        public async Task<InvitationDTO> GetByServerIdAndByUserId(int userId, int serverId)
        {
            return _mapper.Map<InvitationDTO>(await _context.Invitation.FirstOrDefaultAsync(x => x.Receiver.Equals(userId) && x.ServerId.Equals(serverId) && x.IsActive == true));
        }

        public async Task<List<InvitationDTO>> ListByServerId(int id)
        {
            return _mapper.Map<List<InvitationDTO>>(await _context.Invitation.Where(x => x.ServerId.Equals(id) && x.IsActive == true).ToListAsync());
        }

        public async Task<List<InvitationDTO>> ListByUserId(int id)
        {
            return _mapper.Map<List<InvitationDTO>>(await _context.Invitation.Where(x => x.Receiver.Equals(id) && x.IsActive == true).ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<InvitationDTO> ToggleInactiveById(int id)
        {
            var invitation = await _context.Invitation.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (invitation.IsActive)
            {
                invitation.IsActive = false;
            }
            else
            {
                invitation.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<InvitationDTO>(invitation);
        } 
        #endregion
    }
}
