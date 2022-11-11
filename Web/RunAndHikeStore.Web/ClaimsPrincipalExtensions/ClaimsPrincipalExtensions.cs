using System.Security.Claims;

namespace RunAndHikeStore.Web.ClaimsPrincipalExtensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user) 
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
