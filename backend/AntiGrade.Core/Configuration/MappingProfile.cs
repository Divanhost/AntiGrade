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
                .ForMember(x => x.Roles, _ => _.MapFrom(x => x.Roles.Select(r => r.Role.Name).ToList()));
            CreateMap<User, UserDto>()
                .ForMember(x => x.Roles, _ => _.MapFrom(x => x.Roles.Select(r => r.Role.Name).ToList()));
            CreateMap<Role,RolesView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name));
            CreateMap<Employee, EmployeeView>()
                .ForMember(x => x.FullName, _ => _.MapFrom(r => $"{r.LastName} {r.FirstName}"))
                .ForMember(x => x.Id, _ => _.MapFrom(r => r.Id));
            CreateMap<EmployeeDto, Employee>()
                .ForMember(x => x.FirstName, _ => _.MapFrom(r => r.FirstName))
                .ForMember(x => x.LastName, _ => _.MapFrom(r => r.LastName))
                .ForMember(x => x.UserId, _ => _.MapFrom(r => r.UserId))
                .ForMember(x => x.Patronymic, _ => _.MapFrom(r => r.Patronymic))
                .ForMember(x => x.EmployeePositionId, _ => _.Ignore())
                .ForMember(x => x.Id, _ => _.MapFrom(r => r.Id));
            CreateMap<Employee,EmployeeDto>().ReverseMap();
            CreateMap<Subject,SubjectView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.ExamType, opt=>opt.MapFrom(src => src.Type))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name));
            CreateMap<Subject,MainSubjectView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.ExamType, opt=>opt.MapFrom(src => src.Type))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name));
            CreateMap<Subject,SubjectDto>()
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.SubjectEmployees, opt=>opt.MapFrom(src => src.SubjectEmployees))
                .ForMember(x=> x.ExamType, opt=>opt.MapFrom(src => src.Type))
                .ForMember(x=> x.Group, opt=>opt.MapFrom(src => src.Group))
                .ForMember(x=> x.Works, opt=>opt.MapFrom(src => src.Works))
                .ForMember(x=> x.SubjectEmployees, opt=>opt.Ignore());
            CreateMap<SubjectDto,Subject>()
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.SubjectEmployees, opt=>opt.Ignore())
                .ForMember(x=> x.Type, opt=>opt.MapFrom(src => src.ExamType))
                .ForMember(x=> x.Group, opt=>opt.Ignore())
                .ForMember(x=> x.TypeId, opt=>opt.Ignore());
            CreateMap<CriteriaDto,Criteria>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.MaxPoints, opt=>opt.MapFrom(src => src.Points))
                .ForMember(x=> x.WorkId, opt=>opt.MapFrom(src => src.WorkId));
            CreateMap<Criteria,CriteriaDto>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.Points, opt=>opt.MapFrom(src => src.MaxPoints));
            CreateMap<Work,WorkDto>(MemberList.Source)
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.Points, opt=>opt.MapFrom(src => src.MaxPoints))
                .ForMember(x=> x.SubjectId, opt=>opt.MapFrom(src => src.SubjectId))
                .ForMember(x=> x.WorkTypeId, opt=>opt.MapFrom(src => src.WorkType.Id))
                .ForMember(x=> x.Criterias, opt=>opt.MapFrom(src => src.Criterias));
             CreateMap<WorkDto,Work>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.MaxPoints, opt=>opt.MapFrom(src => src.Points))
                .ForMember(x=> x.Criterias, opt=>opt.MapFrom(src => src.Criterias))
                .ForMember(x=> x.WorkTypeId, opt=>opt.MapFrom(src => src.WorkTypeId));
            CreateMap<Work,WorkView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.Points, opt=>opt.MapFrom(src => src.MaxPoints))
                .ForMember(x=> x.Criterias, opt=>opt.MapFrom(src => src.Criterias));
            CreateMap<Group,GroupView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.Students, opt=>opt.MapFrom(src => src.Students));
             CreateMap<GroupView,Group>().ReverseMap();
                // .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                // .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                // .ForMember(x=> x.Students, opt=>opt.MapFrom(src => src.Students));
            CreateMap<GroupDto,Group>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.Name, opt=>opt.MapFrom(src => src.Name))
                .ForMember(x=> x.Students, opt=>opt.MapFrom(src => src.Students));
            CreateMap<Student,StudentView>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.FirstName, opt=>opt.MapFrom(src => src.FirstName))
                .ForMember(x=> x.LastName, opt=>opt.MapFrom(src => src.LastName))
                .ForMember(x=> x.Patronymic, opt=>opt.MapFrom(src => src.Patronymic))
                .ForMember(x=> x.GroupId, opt=>opt.MapFrom(src => src.GroupId));
             CreateMap<StudentDto,Student>()
                .ForMember(x=> x.Id, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x=> x.FirstName, opt=>opt.MapFrom(src => src.FirstName))
                .ForMember(x=> x.LastName, opt=>opt.MapFrom(src => src.LastName))
                .ForMember(x=> x.Patronymic, opt=>opt.MapFrom(src => src.Patronymic))
                .ForMember(x=> x.GroupId, opt=>opt.MapFrom(src => src.GroupId));
            CreateMap<Student,StudentDto>().ReverseMap();
            CreateMap<StudentCriteriaDto,StudentCriteria>()
                .ForMember(x=> x.Id, opt=>opt.Ignore())
                .ForMember(x=> x.StudentId, opt=>opt.MapFrom(src => src.StudentId))
                .ForMember(x=> x.CriteriaId, opt=>opt.MapFrom(src => src.CriteriaId))
                .ForMember(x=> x.Touched, opt=>opt.MapFrom(src => src.Touched))
                .ForMember(x=> x.IsAdditional, opt=>opt.MapFrom(src => src.IsAdditional))
                .ForMember(x=> x.TotalPoints, opt=>opt.MapFrom(src => src.Points));
            CreateMap<StudentWorkDto,StudentWork>()
                .ForMember(x=> x.Id, opt=>opt.Ignore())
                .ForMember(x=> x.StudentId, opt=>opt.MapFrom(src => src.StudentId))
                .ForMember(x=> x.WorkId, opt=>opt.MapFrom(src => src.WorkId))
                .ForMember(x=> x.Touched, opt=>opt.MapFrom(src => src.Touched))
                .ForMember(x=> x.IsAdditional, opt=>opt.MapFrom(src => src.IsAdditional))
                .ForMember(x=> x.SumOfPoints, opt=>opt.MapFrom(src => src.SumOfPoints));
            CreateMap<StudentCriteria,StudentCriteriaDto>()
                .ForMember(x=> x.Points, opt=>opt.MapFrom(src => src.TotalPoints))
                .ForMember(x=> x.IsAdditional, opt=>opt.MapFrom(src => src.IsAdditional));
            CreateMap<StudentWork,StudentWorkDto>()
                .ForMember(x=> x.SumOfPoints, opt=>opt.MapFrom(src => src.SumOfPoints))
                .ForMember(x=> x.IsAdditional, opt=>opt.MapFrom(src => src.IsAdditional));
             CreateMap<ExamResult,ExamResultDto>()
                .ForMember(x=> x.Id, opt=>opt.Ignore())
                .ForMember(x=> x.StudentId, opt=>opt.MapFrom(src => src.StudentId))
                .ForMember(x=> x.SubjectId, opt=>opt.MapFrom(src => src.SubjectId))
                .ForMember(x=> x.Points, opt=>opt.MapFrom(src => src.Points))
                .ForMember(x=> x.SecondPassPoints, opt=>opt.MapFrom(src => src.SecondPassPoints))
                .ForMember(x=> x.ThirdPassPoints, opt=>opt.MapFrom(src => src.ThirdPassPoints));
             CreateMap<ExamResultDto,ExamResult>().ReverseMap();
        }
    }
}
