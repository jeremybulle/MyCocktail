using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace MyCocktail.Api.Services.Authentication
{
    public static class AuthentificationHelper
    {

        public static string GetUserId(this IPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            var identity = (ClaimsIdentity)user.Identity;
            var claims = identity.Claims;

            return claims.FirstOrDefault(c => c.Type == "id")?.Value;
        }
    }
}
