using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Workflow.Application.Features.Assignments.AssignReviewer;
using Workflow.Application.Features.Comments.AddComment;
using Workflow.Application.Features.Revisions.GetAll;
using Workflow.Application.Features.Revisions.ResolveRevision;
using Workflow.Domain.Entities;

namespace Workflow.Presentation.Controllers
{
    [ApiController]
    [Route("api/workflow/revisions")]
    [Authorize]
    public class RevisionsController : ControllerBase
    {
        private readonly ISender _sender;

        public RevisionsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllRevisionesQuery();
                var result = await _sender.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [Authorize(Roles = "2,3")]
        [HttpPost("{id}/assign-reviewer")]
        public async Task<IActionResult> AssignReviewer(Guid id)
        {
            try
            {
                var asesorIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);
                if (asesorIdClaim == null || !Guid.TryParse(asesorIdClaim.Value, out var asesorId))
                    return Unauthorized(new { Error = "No se pudo identificar al usuario." });

                var command = new AssignReviewerCommand(id, asesorId);
                await _sender.Send(command);
                return Ok(new { Message = "Revisor asignado con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(Guid id, [FromBody] AddCommentRequest request)
        {
            try
            {
                var autorIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);
                if (autorIdClaim == null || !Guid.TryParse(autorIdClaim.Value, out var autorId))
                    return Unauthorized(new { Error = "No se pudo identificar al usuario." });

                var command = new AddCommentCommand(id, autorId, request.Contenido);
                var commentId = await _sender.Send(command);
                return Ok(new { CommentId = commentId, Message = "Comentario agregado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("{id}/resolve")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> Resolve(Guid id, [FromBody] ResolveRevisionRequest request)
        {
            try
            {
                var command = new ResolveRevisionCommand(id, (EstadoRevision)request.NuevoEstado);
                await _sender.Send(command);
                return Ok(new { Message = "Estado de revision actualizado con exito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public record AddCommentRequest(string Contenido); 
    public record ResolveRevisionRequest(int NuevoEstado);
}