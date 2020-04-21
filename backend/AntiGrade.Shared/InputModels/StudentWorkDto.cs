using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.Models
{
    public class StudentWorkDto
    {
        public int Id {get;set;}
        public int StudentId {get;set;}
        public int WorkId {get;set;}
        public decimal SumOfPoints {get;set;}
        public bool Touched {get;set;}
        public bool IsAdditional {get;set;}
    }
}