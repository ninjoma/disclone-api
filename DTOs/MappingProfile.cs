using AutoMapper;
using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
