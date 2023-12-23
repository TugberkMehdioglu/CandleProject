﻿using AutoMapper;
using Project.ENTITIES.Models;
using MemberViewModels = Project.MVCUI.Areas.Member.MemberViewModels;
using Project.MVCUI.ViewModels;

namespace Project.MVCUI.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<AppUser, AppUserViewModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

            CreateMap<AppUserProfile, AppUserProfileViewModel>().ReverseMap();

            CreateMap<AppUser, MemberViewModels.AppUserViewModel>()
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

            CreateMap<AppUserProfile, MemberViewModels.AppUserProfileViewModel>().ReverseMap();

            CreateMap<Address, MemberViewModels.AddressViewModel>().ReverseMap();
        }
    }
}
