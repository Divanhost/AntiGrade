using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AntiGrade.Shared.Models.Identity
{
    public class User : IdentityUser<int>, IEntity<int>
    { 
        public Employee Employee
        {
            get;
            set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }
        public virtual ICollection<UserRole> Roles
        {
            get;
            set;
        }
    }
}
