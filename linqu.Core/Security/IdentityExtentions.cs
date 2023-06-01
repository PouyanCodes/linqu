

using System.Security.Claims;
using System.Security.Principal;

namespace linqu.Core.Security
{

    // Get The Saved Information In Claim

    public static class IdentityExtentions
    {
        public static int GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return 0;
            else
                return int.Parse(claim.Value);
        }

        public static int GetUserRoleId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            if (claim == null)
                return 0;
            else
                return int.Parse(claim.Value);
        }
    }
}
