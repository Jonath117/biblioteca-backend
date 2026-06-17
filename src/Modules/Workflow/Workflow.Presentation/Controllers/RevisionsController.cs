using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workflow.Application.Features.Assignments.AssignReviewer;
using Workflow.Application.Features.Comments.AddComment;
// IMPORTANTE: Asegúrate de importar el Namespace de tu Query de listar
// using Workflow.Application.Features.Revisions.GetAll; 

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

        // --- MÉTODO NUEVO PARA QUE EL FRONTEND NO DE ERROR 404 ---
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Si aún no tienes la query creada, esto al menos evita el 404
                // Cuando crees la query de listar, descomenta las líneas de abajo:
                // var query = new GetAllRevisionesQuery();
                // var result = await _sender.Send(query);
                // return Ok(result);
                
                return Ok(new List<object>()); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        // ---------------------------------------------------------

        [HttpPost("{id}/assign-reviewer")]
        public async Task<IActionResult> AssignReviewer(Guid id, [FromBody] AssignReviewerRequest request)
        {
            try
            {
                var command = new AssignReviewerCommand(id, request.AsesorId);
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