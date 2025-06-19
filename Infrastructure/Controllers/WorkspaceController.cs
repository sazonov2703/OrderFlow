using System.Security.Claims;
using Application.UseCases.Workspaces.Commands.Requests;
using Infrastructure.DTOs.Workspace;
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
        [FromBody] WorkspaceRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(
            new CreateWorkspaceCommand(Guid.Parse(userId), request.Name),
            cancellationToken
        );
        
        return Ok(result);
    }
}