using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.ViewModels
{
    public class SubjectExamStatusView
    {
        public int Id { get; set; }
        public int SubjectId {get;set;}
        public bool IsExamStarted {get;set;}
        public bool IsExamClosed {get;set;}
        public bool IsFirstRetakeStarted  {get;set;}
        public bool IsFirstRetakeClosed {get;set;}
        public bool IsSecondRetakeStarted {get;set;}
        public bool IsSecondRetakeClosed {get;set;}
    }
}