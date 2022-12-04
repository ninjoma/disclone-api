﻿using AutoMapper;
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
            CreateMap<Server, ServerDTO>().ReverseMap(); 
            #endregion
        }
    }
}
