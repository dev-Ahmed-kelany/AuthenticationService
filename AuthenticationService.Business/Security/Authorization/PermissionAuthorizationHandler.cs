using System.Security.Claims;
using AuthenticationService.Business;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Business.Security.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            Claim? permissionsMaskClaim = context.User.FindFirst("PermissionsMask");
            if (permissionsMaskClaim == null) return;

            if (!long.TryParse(permissionsMaskClaim.Value, out long permissionsMask)) return;

            var permission = await Permission.GetByNameAsync(requirement.PermissionName);

            if (!permission.IsSuccess) return;

            bool hasPermission = (permissionsMask & permission.Data?.BitValue) == permission.Data?.BitValue;

            if (hasPermission) context.Succeed(requirement);
        }
    }
}
