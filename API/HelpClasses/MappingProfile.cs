using AutoMapper;
using OnionArch.Data.Entities;
using OnionArch.Repository.DTOs;

namespace API.HelpClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(d=>d.PositionType,o=>o.MapFrom(s=>s.PositionType.Name));
        }
    }
}
