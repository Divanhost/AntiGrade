using System.Collections.Generic;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Shared.InputModels
{
    public class CriteriaDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public decimal Points {get;set;}
    }
}