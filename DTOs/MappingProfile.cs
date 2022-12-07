using AutoMapper;
using disclone_api.DTOs.ChannelDTOs;
using disclone_api.DTOs.InvitationDTOs;
using disclone_api.DTOs.MemberDTOs;
using disclone_api.DTOs.ServerDTOs;
using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ReverseMap();
            #endregion

            #region Server
            CreateMap<Server, ServerDTO>()
                .ReverseMap();
            #endregion

            #region Member
            CreateMap<Member, MemberDTO>()
                .ReverseMap();
            #endregion

            #region Invitation
            CreateMap<Invitation, InvitationDTO>()
                .ReverseMap();
            #endregion

            #region Channel
            CreateMap<Channel, ChannelDTO>()
                .ReverseMap();
            #endregion
        }
    }
}
