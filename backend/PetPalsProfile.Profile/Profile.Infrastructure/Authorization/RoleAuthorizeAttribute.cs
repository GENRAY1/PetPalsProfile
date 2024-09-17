using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace Profile.Infrastructure.Authorization;


public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string[] AllowedRoles { get; set; }

    public RoleAuthorizeAttribute(params string[] roles)
    {
        AllowedRoles = roles;
    }

    public async Task HandleRequirementAsync(AuthorizationHandlerContext context)
    {
        var roles = context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();;

        if (roles.Count > 0)
        {
            if (AllowedRoles.Any(role => roles.Contains(role)))
            {
                context.Succeed(this);
            }
        }
    }
}