using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using SmartInventoryBE.Interfaces.Services;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SmartInventoryBE.Middleware;

public class WorkContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _tokenService;

    public WorkContextMiddleware(RequestDelegate next, ITokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // If the user is already authenticated through the standard authentication pipeline,
        // proceed to the next middleware
        if (context.User.Identity?.IsAuthenticated == true)
        {
            await _next(context);
            return;
        }

        // Try to extract the token from the Authorization header
        string token = string.Empty;
        if (context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
        {
            string auth = authHeader.FirstOrDefault() ?? string.Empty;
            if (auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = auth.Substring("Bearer ".Length).Trim();
            }
        }

        // If token is found, validate it
        if (!string.IsNullOrEmpty(token) && _tokenService.ValidateToken(token))
        {
            // Decode the token to get the claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            
            // Create claims identity
            var claims = jwtToken.Claims.ToList();
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            
            // Set the user on the current request
            context.User = principal;
        }

        await _next(context);
    }
} 