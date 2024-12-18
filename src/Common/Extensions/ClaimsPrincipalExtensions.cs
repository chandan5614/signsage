using System.Collections.Generic;
using System.Security.Claims;

namespace Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        // Extension method to get the User's roles as a list of strings
        public static List<string> GetRoles(this ClaimsPrincipal user)
        {
            var roles = new List<string>();
            if (user?.Identity?.IsAuthenticated == true)
            {
                roles.AddRange(user.FindAll(ClaimTypes.Role).ToString());
            }
            return roles;
        }

        // Extension method to get the User's Email
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Email)?.Value;
        }

        // Extension method to get a specific Claim
        public static string GetClaimValue(this ClaimsPrincipal user, string claimType)
        {
            return user?.FindFirst(claimType)?.Value;
        }
    }
}
