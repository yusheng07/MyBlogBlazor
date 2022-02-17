using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace MyBlogWebAssembly.Client.Authentication
{
    public class RoleAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public RoleAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
        {

        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)user.Identity;

                //clear default role values
                var roleClaims = identity.FindAll(identity.RoleClaimType).ToList();
                if (roleClaims != null && roleClaims.Any())
                {
                    foreach (var existingClaim in roleClaims)
                    {
                        identity.RemoveClaim(existingClaim);
                    }
                }

                var rolesElem = account.AdditionalProperties[identity.RoleClaimType];
                if (rolesElem is JsonElement roles) //get the JSON node where the roles can be found
                {
                    if (roles.ValueKind == JsonValueKind.Array) //nodes return as an array of strings, add every item to the user
                    {
                        foreach (var role in roles.EnumerateArray())
                        {
                            var rolestring = role.GetString();
                            if (rolestring != null)
                            {
                                identity.AddClaim(new Claim(options.RoleClaim, rolestring));
                            }
                        }
                    }
                    else  //nodes return as a string, just add single item to the user
                    {
                        var rolestring = roles.GetString();
                        if (rolestring != null)
                        {
                            identity.AddClaim(new Claim(options.RoleClaim, rolestring));
                        }
                    }
                }
            }

            return user;
        }
    }
}
