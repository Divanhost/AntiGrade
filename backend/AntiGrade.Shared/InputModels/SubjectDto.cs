using System.Collections.Generic;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Shared.InputModels
{
    public class SubjectDto
    {
        public string Name {get;set;}
        public int ExamTypeId {get;set;}
        public ExamType ExamType {get;set;}
        public GroupView Group {get;set;}
        public List<SubjectEmployee> SubjectEmployees {get;set;}

    }
}