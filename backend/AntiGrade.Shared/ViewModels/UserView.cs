using System.Collections.Generic;

namespace AntiGrade.Shared.ViewModels
{
    public class UserView
    {
        public int Id
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public List<string> Role
        {
            get;
            set;
        }

    }
}