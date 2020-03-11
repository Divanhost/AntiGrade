using AntiGrade.Shared.InputModels;

namespace AntiGrade.Shared.ViewModels 
{
    public class EmployeeView:EmployeeDto
    {
        public string FullName
        {
            get
            {
                return LastName + ' ' + FirstName;
            }
        }
    }
}