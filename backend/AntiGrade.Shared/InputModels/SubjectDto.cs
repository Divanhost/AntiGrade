using System.Collections.Generic;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Shared.InputModels
{
    public class SubjectDto
    {
        public string Name {get;set;}
        public ExamType ExamType {get;set;}
        public GroupView Group {get;set;}
        public int SemesterId {get;set;}
        public bool HasBonuses {get;set;}
        public List<WorkDto> Works {get;set;}
        public List<SubjectEmployeeDto> SubjectEmployees {get;set;}

    }
}