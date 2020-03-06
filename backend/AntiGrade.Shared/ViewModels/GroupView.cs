using System.Collections.Generic;

namespace AntiGrade.Shared.InputModels 
{
    public class GroupView
    {
        public string Name {get;set;}
        public string LastName {get;set;}
        public List<StudentView> StudentsDto {get;set;}
    }
}