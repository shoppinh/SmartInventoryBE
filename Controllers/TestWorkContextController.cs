using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventoryBE.Interfaces.Services;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestWorkContextController : ControllerBase
{
    private readonly IWorkContextService _workContextService;

    public TestWorkContextController(IWorkContextService workContextService)
    {
        _workContextService = workContextService;
    }

    [HttpGet("public")]
    public ActionResult<string> GetPublic()
    {
        return Ok("This is a public endpoint");
    }

    [HttpGet("context")]
    public ActionResult<WorkContext> GetContext()
    {
        var context = _workContextService.GetContext();
        return Ok(new
        {
            IsAuthenticated = context.IsAuthenticated,
            UserId = context.UserId,
            UserName = context.UserName,
            Email = context.Email,
            Roles = context.Roles
        });
    }

    [Authorize]
    [HttpGet("secured")]
    public ActionResult<string> GetSecured()
    {
        var context = _workContextService.GetContext();
        
        return Ok($"Hello {context.UserName}, your user ID is {context.UserId} and your email is {context.Email}");
    }
} 