using System.Security.Claims;
using Application.UseCases.Workspaces.DTOs;
using Application.UseCases.Workspaces.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkspaceController(IMediator mediator) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateWorkspace(
        [FromBody] CreateWorkspaceDto createWorkspaceDto, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(
            new CreateWorkspaceCommand(Guid.Parse(userId), createWorkspaceDto),
            cancellationToken
        );
        
        return Ok(result);
    }
}