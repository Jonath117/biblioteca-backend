using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workspace.Presentation.Controllers;

[ApiController]
[Route("api/workspace/[controller]")]
[Authorize]
public class WorkspaceController : ControllerBase
{
    private readonly ISender _sender;

    public WorkspaceController(ISender sender)
    {
        _sender = sender;
    }
}
