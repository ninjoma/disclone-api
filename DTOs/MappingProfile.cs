using AutoMapper;
using disclone_api.Entities;

namespace disclone_api.DTO
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

            #region Message
            CreateMap<Message, MessageDTO>()
                .ReverseMap();
            #endregion
        }
    }
}
