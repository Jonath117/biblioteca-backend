using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Features.Assignments.AssignReviewer;
using Workflow.Application.Features.Comments.AddComment;

namespace Workflow.Presentation.Controllers;

[ApiController]
[Route("api/workflow/revisions")]
public class RevisionsController : ControllerBase
{
    private readonly ISender _sender;

    public RevisionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignReviewer([FromBody] AssignReviewerCommand command, CancellationToken cancellationToken)
    {
        var revisionId = await _sender.Send(command, cancellationToken);
        
        return Ok(new { RevisionId = revisionId });
    }

    [HttpPost("comments")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentCommand command, CancellationToken cancellationToken)
    {
        var commentId = await _sender.Send(command, cancellationToken);
        
        return Ok(new { CommentId = commentId });
    }
}