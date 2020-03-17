using System.Collections.Generic;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Shared.InputModels
{
    public class SubjectPlan
    {
        public int SubjectId {get;set;}
        public List<WorkDto> Works {get;set;}
    }
}