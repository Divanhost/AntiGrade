using Microsoft.AspNetCore.Identity;

namespace AntiGrade.Shared.Models.Identity
{
    public class UserRole : IdentityUserRole<int>, IEntity<int>
    {
        public virtual User User
        {
            get;
            set;
        }
        public virtual Role Role
        {
            get;
            set;
        }
    }
}
