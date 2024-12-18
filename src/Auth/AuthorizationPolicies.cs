using Microsoft.AspNetCore.Authorization;

namespace Auth
{
    public static class AuthorizationPolicies
    {
        public const string AdminRole = "Admin";
        public const string UserRole = "User";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireRole(AdminRole)
                .Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireRole(UserRole)
                .Build();
        }

        public static AuthorizationPolicy AdminOrUserPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireRole(AdminRole, UserRole)
                .Build();
        }
    }
}
