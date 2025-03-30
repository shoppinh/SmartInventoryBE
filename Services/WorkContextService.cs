using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Services;

public class WorkContextService : IWorkContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly WorkContext _workContext;

    public WorkContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _workContext = new WorkContext();
        InitializeContext();
    }

    private void InitializeContext()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity == null || !httpContext.User.Identity.IsAuthenticated)
        {
            _workContext.IsAuthenticated = false;
            return;
        }

        _workContext.IsAuthenticated = true;
        _workContext.UserId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        _workContext.UserName = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? string.Empty;
        _workContext.Email = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
        
        // Extract roles
        var roleClaims = httpContext.User.FindAll(System.Security.Claims.ClaimTypes.Role);
        foreach (var roleClaim in roleClaims)
        {
            _workContext.Roles.Add(roleClaim.Value);
        }
    }

    public WorkContext GetContext()
    {
        return _workContext;
    }

    public string GetUserId()
    {
        return _workContext.UserId;
    }

    public string GetUserName()
    {
        return _workContext.UserName;
    }

    public string GetEmail()
    {
        return _workContext.Email;
    }

    public bool IsAuthenticated()
    {
        return _workContext.IsAuthenticated;
    }

    public bool IsInRole(string role)
    {
        return _workContext.Roles.Contains(role);
    }
} 