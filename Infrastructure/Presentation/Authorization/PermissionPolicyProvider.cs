using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Presentation.Authorization
{
    public sealed class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public const string Prefix = "Permission:";

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options) { }

        public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
            {
                var code = policyName.Substring(Prefix.Length).Trim();

                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(code))
                    .Build();

                return Task.FromResult<AuthorizationPolicy?>(policy);
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}
