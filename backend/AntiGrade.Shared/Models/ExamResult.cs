using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.Models
{
    public class ExamResult : IEntityWithId<int>
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId {get;set;}

        [ForeignKey(nameof(Student))]
        public int StudentId {get;set;}
        
        [Column(TypeName = "decimal(18,5)")]
        public decimal Points {get;set;}

        [Column(TypeName = "decimal(18,5)")]
        public decimal SecondPassPoints {get;set;}

        [Column(TypeName = "decimal(18,5)")]
        public decimal ThirdPassPoints {get;set;}
        public virtual Subject Subject {get;set;}
        public virtual Student Student {get;set;}
    }
}