using Academia.Application.Behaviours.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        IMediator mediator,
        ILogger<UsersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost(Name ="Create a new User")]
    public async Task<IActionResult> CreateUser(CreateUserCommand request)
    {
        try
        {
            _logger.LogInformation("Received request to create user: {Username}", request.Username);
            await _mediator.Send(request);
            return Ok(new { Message = "User created successfully" });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unable to create user");
            return BadRequest(new { Error = "Unable to create user", Message = ex.Message });
        }
    }
}
