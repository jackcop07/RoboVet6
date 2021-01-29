using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboVet6.API.Auth
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            //var scopes = context.User.FindFirst(c => c.Type == "permissions" && c.Issuer == requirement.Issuer).Value.Split(' ');
            var scopes = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer);
            // Succeed if the scope array contains the required scope
            foreach (var scope in scopes)
            {
                if (scope.Value == requirement.Scope)
                {
                    context.Succeed(requirement);
                }
            }


            return Task.CompletedTask;
        }
    }
}
