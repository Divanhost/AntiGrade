using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.InputModels
{
    public class SubjectEmployeeDto
    {
        public int Id {get;set;}
        public int SubjectId {get;set;}
        public int EmployeeId {get;set;}
        public string Status {get;set;}
    }
}