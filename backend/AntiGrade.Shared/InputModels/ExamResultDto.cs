using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AntiGrade.Shared.Enums;

namespace AntiGrade.Shared.InputModels
{
    public class ExamResultDto
    {
        public int Id { get; set; }
        public int SubjectId {get;set;}
        public int StudentId {get;set;}
        public decimal Points {get;set;}
        public decimal SecondPassPoints {get;set;}
        public decimal ThirdPassPoints {get;set;}
        public bool IsFailed {get;set;}
    }
}