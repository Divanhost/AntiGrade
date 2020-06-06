using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class SubjectExamStatus : IEntityWithId<int>
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId {get;set;}
        public bool IsExamStarted {get;set;}
        public bool IsExamClosed {get;set;}
        public bool IsFirstRetakeStarted  {get;set;}
        public bool IsFirstRetakeClosed {get;set;}
        public bool IsSecondRetakeStarted {get;set;}
        public bool IsSecondRetakeClosed {get;set;}
        public virtual Subject Subject {get;set;}
    }
}