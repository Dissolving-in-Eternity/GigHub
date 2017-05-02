using System.Security.Claims;
using System.Security.Principal;

namespace GigHub.Core.Extensions
{
    public static class GenericPrincipalExtensions
    {
        public static string GetName(this IPrincipal usr)
        {
            var nameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("Name");
            if (nameClaim != null)
                return nameClaim.Value;

            return "";
        }
    }
}