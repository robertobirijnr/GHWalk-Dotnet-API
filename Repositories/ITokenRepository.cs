

using Microsoft.AspNetCore.Identity;

namespace GHWalk.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}