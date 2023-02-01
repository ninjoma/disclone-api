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

            CreateMap<User, UserDetailDTO>()
                .ReverseMap();
            #endregion

            #region Server
            CreateMap<Server, ServerDTO>()
                .ReverseMap();

            CreateMap<Server, ServerDetailDTO>()
                .ReverseMap();
            #endregion

            #region Member
            CreateMap<Member, MemberDTO>()
                .ReverseMap();

            CreateMap<Member, MemberDetailDTO>()
                .ReverseMap();
            #endregion

            #region Invitation
            CreateMap<Invitation, InvitationDTO>()
                .ReverseMap();

            CreateMap<Invitation, InvitationDetailDTO>()
                .ReverseMap();
            #endregion

            #region Channel
            CreateMap<Channel, ChannelDTO>()
                .ReverseMap();

            CreateMap<Channel, ChannelDetailDTO>()
                .ReverseMap();
            #endregion

            #region Message
            CreateMap<Message, MessageDTO>()
                .ReverseMap();

            CreateMap<Message, MessageDetailDTO>()
                .ReverseMap();
            #endregion
        }
    }
}
