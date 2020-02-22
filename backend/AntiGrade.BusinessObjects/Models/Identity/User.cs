using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AntiGrade.BusinessObjects.Models.Identity
{
    public class User : IdentityUser<int>, IEntity<int>
    {
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
        // public virtual List<AccountAccess> AccountAccess
        // {
        //     get;
        //     set;
        // }
        // public virtual List<SourcePasswordAccess> SourcePasswordAccess
        // {
        //     get;
        //     set;
        // }
        // public virtual List<Dashboard> Dashboards
        // {
        //     get;
        //     set;
        // }
    }
}
