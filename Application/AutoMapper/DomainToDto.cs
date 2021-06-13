
using AutoMapper;
using System;
using System.Linq;
using WebApi.Application.Dto;
using WebApi.Domain.Models;

namespace WebApi.Application.AutoMapper
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<Domain.Models.User, Dto.User>()
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.UserRoles.FirstOrDefault().Role.Name));

            CreateMap<RegisterModel, Domain.Models.User>()
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => DateTime.Now));
        }



    }
}
