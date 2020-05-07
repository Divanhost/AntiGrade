using System.Collections.Generic;
using AntiGrade.Shared.ViewModels;
namespace AntiGrade.Shared.InputModels 
{
    public class GroupDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public List<StudentDto> Students {get;set;}
        public CourseView Course {get;set;}
    }
}