using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EasyChatBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyChatBackend.Filters;

public class BlockExpiredJwtAsyncActionFilter : ActionFilterAttribute
{
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = context.HttpContext.RequestServices.GetService<EasyChatContext>()!;
        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            var token = authHeader.ToString();
            if (IsTokenExpired(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        await next();
        return;

        bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var issuesAt = jwtToken.IssuedAt;
            var claims = jwtToken.Claims;

            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var userInfo = dbContext.Users.FirstOrDefault(u => u.UserId == userId);

            if (userInfo == null || userInfo.Status == StatusEnum.Banned)
            {
                return true;
            }

            return issuesAt < userInfo.LastOffTime;
        }
    }
}