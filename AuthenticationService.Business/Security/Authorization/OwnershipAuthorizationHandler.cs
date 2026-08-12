using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business.Security.Authorization
{
    public class OwnershipAuthorizationHandler : AuthorizationHandler<OwnershipRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnershipRequirement requirement)
        {
            if (context.User.IsInRole("Administrator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.Resource is not HttpContext httpContext) return Task.CompletedTask;

            Claim? userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return Task.CompletedTask;

            if (!int.TryParse(userIdClaim.Value, out int userId)) return Task.CompletedTask;

            if (!httpContext.Request.RouteValues.TryGetValue("id", out object? routeValue)) return Task.CompletedTask;

            if (!int.TryParse(routeValue?.ToString(), out int requestedUserId)) return Task.CompletedTask;

            if (userId == requestedUserId) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
