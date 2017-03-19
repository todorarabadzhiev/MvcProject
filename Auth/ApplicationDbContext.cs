using CommonUtilities.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Auth
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(Utilities.DbConnectionName, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
