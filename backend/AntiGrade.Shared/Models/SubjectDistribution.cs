using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.Models
{
    public class SubjectDistribution : IEntityWithId<int>
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Group))]
        public int GroupId {get;set;}

        [ForeignKey(nameof(Subject))]
        public int SubjectId {get;set;}

        [ForeignKey(nameof(Teacher))]
        public int TeacherId {get;set;}
        public Group Group {get;set;}
        public Subject Subject {get;set;}
        public Employee Teacher {get;set;}
        public string TeacherStatus {get;set;}

    }
}