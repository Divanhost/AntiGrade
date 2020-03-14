using System.Collections.Generic;

namespace AntiGrade.Shared.InputModels 
{
    public class GroupDto
    {
        public string Name {get;set;}
        public List<StudentDto> StudentsDto {get;set;}
    }
}