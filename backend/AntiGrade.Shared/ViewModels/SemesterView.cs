using System.Collections.Generic;

namespace AntiGrade.Shared.ViewModels
{
    public class SemesterView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isFirstHalf {get;set;}
        public int Year {get;set;}
    }
}