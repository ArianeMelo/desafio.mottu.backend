using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Mottu.Locacao.Motos.Api.RoleFilter
{
    /// <inheritdoc/>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RoleAuthorizerAttribute : TypeFilterAttribute
    {
        /// <inheritdoc/>
        public RoleAuthorizerAttribute(string[] roles) : base(typeof(RoleAuthorizerFilter))
        {
            Arguments = [roles];
        }
    }

    /// <inheritdoc/>
    public class RoleAuthorizerFilter(string[] roles) : IAsyncAuthorizationFilter
    {
        /// <inheritdoc/>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var roleUser = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (!roles.Contains(roleUser?.Value))
                    context.Result = new StatusCodeResult(403);
            }
            catch (Exception)
            {
                context.Result = new StatusCodeResult(401);
            }

            await Task.CompletedTask;
        }
    }
}
