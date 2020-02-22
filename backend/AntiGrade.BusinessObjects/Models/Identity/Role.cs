using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AntiGrade.BusinessObjects.Models.Identity
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public virtual ICollection<UserRole> Users { get; set; }
    }
}

