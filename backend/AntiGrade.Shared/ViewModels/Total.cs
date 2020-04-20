using AntiGrade.Shared.Models;

namespace AntiGrade.Shared.ViewModels
{
    public class Total
    {
       public int StudentId {get;set;}
       public StudentView Student {get;set;}
       public decimal Totals {get;set;}
    }
}