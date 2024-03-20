using Microsoft.AspNetCore.Identity;

namespace LibraryProject.DataAccess.Models
{
    public class User : IdentityUser<int>
    {

    }
    public class Role : IdentityRole<int>
    {
    }

}
