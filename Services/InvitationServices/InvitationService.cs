﻿using AutoMapper;
using disclone_api.DTO;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services
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
        public async Task<InvitationDTO> EditById(InvitationDTO invitation)
        {
            var oldInvitation = await _context.Invitation.FirstOrDefaultAsync(x => x.Id.Equals(invitation.Id));
            _mapper.Map<InvitationDTO, Invitation>(invitation, oldInvitation);
            await _context.SaveChangesAsync();
            return _mapper.Map<InvitationDTO>(oldInvitation);
        }

        public async Task<InvitationDTO> Add(InvitationDTO invitation)
        {
            var newInvitation = _mapper.Map<Invitation>(invitation);
            await _context.Invitation.AddAsync(newInvitation);
            await _context.SaveChangesAsync();
            return _mapper.Map<InvitationDTO>(newInvitation);
        }
        #endregion

        #region Get
        public async Task<InvitationDetailDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<InvitationDetailDTO>(await _context.Invitation
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<InvitationDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true)
        {
            return _mapper.Map<InvitationDTO>(await _context.Invitation
                .FirstOrDefaultAsync(x => x.Receiver.Equals(userId) 
                && x.ServerId.Equals(serverId) && x.IsActive == isActive));
        }

        public async Task<List<InvitationDTO>> ListByServerId(int id, bool isActive = true)
        {
            return _mapper.Map<List<InvitationDTO>>(await _context.Invitation
                .Where(x => x.ServerId.Equals(id) 
                && x.IsActive == isActive)
                .ToListAsync());
        }

        public async Task<List<InvitationDTO>> ListByUserId(int id, bool isActive = true)
        {
            return _mapper.Map<List<InvitationDTO>>(await _context.Invitation
                .Where(x => x.Receiver.Equals(id) 
                && x.IsActive == isActive)
                .ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<InvitationDTO> DeleteById(int id)
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
