using System.Collections.Generic;

namespace AntiGrade.Shared.ViewModels 
{
    public class GroupView
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public List<StudentView> Students {get;set;}
    }
}