using AntiGrade.Shared.InputModels;

namespace AntiGrade.Shared.ViewModels
{
    public class WorkView:WorkDto
    {
        public bool CanBeQuickRated {get;set;}
        public bool AdditionalsCanBeQuickRated {get;set;}
    }
}