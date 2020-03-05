using System.Collections.Generic;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.Models
{
    public class SubjectDistribution : IEntityWithId<int>
    {
        public int Id { get; set; }
        public Group Group {get;set;}
        public Subject Subject {get;set;}
        public Employee Teacher {get;set;}
        public string TeacherStatus {get;set;}

    }
}