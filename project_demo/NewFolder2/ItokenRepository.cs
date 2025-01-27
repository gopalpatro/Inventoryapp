using Microsoft.AspNetCore.Identity;

namespace project_demo.NewFolder2
{
    public interface ItokenRepository
    {
        string CreateJwtToken(IdentityUser user, string role);
    }
}
