 using Application.DTOs;
 using Application.UseCases.Commands.Requests;
 using MediatR;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 
 namespace Infrastructure.Controllers;
 
 [ApiController]
 [Route("api/[controller]")]
 [Authorize]
 public class WorkspaceController(IMediator mediator) : AuthorizedBaseController
 {
     [HttpPost("create")]
     public async Task<IActionResult> CreateWorkspace(
         [FromBody] CreateWorkspaceDto createWorkspaceDto, CancellationToken cancellationToken)
     {
         var result = await mediator.Send(
             new CreateWorkspaceCommand(UserId, createWorkspaceDto),
             cancellationToken
         );
         
         return Ok(result);
     }
 
     [HttpDelete("delete")]
     public async Task<IActionResult> DeleteWorkspace(Guid workspaceId, CancellationToken cancellationToken)
     {
         var result = await mediator.Send(
             new DeleteWorkspaceCommand(UserId, workspaceId), cancellationToken);
 
         return Ok(result);
     }
 }