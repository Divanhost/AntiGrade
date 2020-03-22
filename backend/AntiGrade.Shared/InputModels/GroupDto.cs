using System.Collections.Generic;

namespace AntiGrade.Shared.InputModels 
{
    public class GroupDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public List<StudentDto> Students {get;set;}
    }
}