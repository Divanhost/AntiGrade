using System.Linq;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;
using AutoMapper;


namespace AntiGrade.Core.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserView>()
                .ForMember(x => x.Role, _ => _.MapFrom(x => x.Roles.Select(r => r.Role.Name).ToList()));
            CreateMap<Role,RolesView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name));
            CreateMap<Employee, EmployeeView>()
                .ForMember(x => x.FullName, _ => _.MapFrom(r => $"{r.LastName} {r.FirstName}"))
                .ForMember(x => x.Id, _ => _.MapFrom(r => r.Id));
            CreateMap<Subject, SubjectDto>()
                .ForMember(x => x.MainTeacher, _ => _.Ignore());
             CreateMap<Subject,SubjectView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name));
            CreateMap<CriteriaDto,Criteria>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.MaxPoints, opt=>opt.MapFrom(src => src.MaxPoints));
            CreateMap<Criteria,CriteriaDto>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.MaxPoints, opt=>opt.MapFrom(src => src.MaxPoints));
            CreateMap<Work,WorkDto>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.MaxPoints, opt=>opt.MapFrom(src => src.MaxPoints))
                .ForMember(x=> x.Criterias, opt=>opt.MapFrom(src => src.Criterias));
            CreateMap<Work,WorkView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.MaxPoints, opt=>opt.MapFrom(src => src.MaxPoints))
                .ForMember(x=> x.Criterias, opt=>opt.MapFrom(src => src.Criterias));
            CreateMap<Student,StudentView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.FirstName, opt=>opt.MapFrom(src => src.FirstName))
                .ForMember(x=> x.LastName, opt=>opt.MapFrom(src => src.LastName))
                .ForMember(x=> x.GroupId, opt=>opt.MapFrom(src => src.GroupId));

        }
    }
}
