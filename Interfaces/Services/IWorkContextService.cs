using SmartInventoryBE.Models;

namespace SmartInventoryBE.Interfaces.Services;

public interface IWorkContextService
{
    WorkContext GetContext();
    string GetUserId();
    string GetUserName();
    string GetEmail();
    bool IsAuthenticated();
    bool IsInRole(string role);
} 