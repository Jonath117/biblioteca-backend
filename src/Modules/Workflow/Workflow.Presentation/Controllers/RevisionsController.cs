using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Workflow.Application.Features.Assignments.AssignReviewer;
using Workflow.Application.Features.Comments.AddComment;

namespace Workflow.Presentation.Controllers
{
    [ApiController]
    [Route("api/workflow/revisions")]
    public class RevisionsController : ControllerBase
    {
        private readonly ISender _sender;

        public RevisionsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("{id}/assign-reviewer")]
        public async Task<IActionResult> AssignReviewer(Guid id, [FromBody] AssignReviewerRequest request)
        {
            try
            {
                var command = new AssignReviewerCommand(id, request.AsesorId);
                var result = await _sender.Send(command);
                
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
                var command = new AddCommentCommand(id, request.AutorId, request.Contenido);
                var commentId = await _sender.Send(command);
                
                return Ok(new { 
                    CommentId = commentId, 
                    Message = "Comentario agregado exitosamente a la revisión." 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public record AssignReviewerRequest(Guid AsesorId);
    public record AddCommentRequest(Guid AutorId, string Contenido);
}