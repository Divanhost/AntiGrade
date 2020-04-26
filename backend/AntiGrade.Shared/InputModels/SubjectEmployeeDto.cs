using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AntiGrade.Shared.Enums;
using AntiGrade.Shared.Models;

namespace AntiGrade.Shared.InputModels
{
    public class SubjectEmployeeDto
    {
        public int Id {get;set;}
        public int SubjectId {get;set;}
        public int EmployeeId {get;set;}
        public List<Status> Statuses {get;set;}
    }
}