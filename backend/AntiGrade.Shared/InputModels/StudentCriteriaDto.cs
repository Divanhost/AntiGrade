using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiGrade.Shared.InputModels
{
    public class StudentCriteriaDto
    {
        public int StudentId {get;set;}
        public int CriteriaId {get;set;}
        public decimal Points {get;set;}
        public bool Touched {get;set;}
    }
}