using System.Collections.Generic;

namespace AntiGrade.Shared.ViewModels 
{
    public class GroupView
    {
        public string Name {get;set;}
        public string LastName {get;set;}
        public List<StudentView> StudentsDto {get;set;}
    }
}