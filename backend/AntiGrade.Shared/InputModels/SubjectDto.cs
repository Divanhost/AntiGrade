using System.Collections.Generic;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Shared.InputModels
{
    public class SubjectDto
    {
        public string Name {get;set;}
        public int ExamTypeId {get;set;}
        public List<EmployeeView> Teachers {get;set;}
        public EmployeeDto MainTeacher {get;set;}
    }
}