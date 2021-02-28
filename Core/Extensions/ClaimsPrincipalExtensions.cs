using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    // ClaimsPrincipal'ı extent ediyoruz.
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); // aradaki ? --> null olabileceğini belirtir.
            return result;
        }

        // claimPrincipal.ClaimRoles -> diyince bana direk rolleri ver.
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) // claimsPrincipal.ClaimRoles diyip direk rolleri dönmesini sağlarız.
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
