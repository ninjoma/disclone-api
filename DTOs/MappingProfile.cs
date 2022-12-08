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

            CreateMap<User, UserGridDTO>()
                .ReverseMap();
            #endregion

            #region Server
            CreateMap<Server, ServerDTO>()
                .ReverseMap();

            CreateMap<Server, ServerGridDTO>()
                .ReverseMap();
            #endregion

            #region Member
            CreateMap<Member, MemberDTO>()
                .ReverseMap();

            CreateMap<Member, MemberGridDTO>()
                .ReverseMap();
            #endregion

            #region Invitation
            CreateMap<Invitation, InvitationDTO>()
                .ReverseMap();

            CreateMap<Invitation, MemberGridDTO>()
                .ReverseMap();
            #endregion

            #region Channel
            CreateMap<Channel, ChannelDTO>()
                .ReverseMap();

            CreateMap<Channel, ChannelGridDTO>()
                .ReverseMap();
            #endregion
        }
    }
}
