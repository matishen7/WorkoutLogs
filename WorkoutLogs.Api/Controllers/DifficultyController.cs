using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.Difficulty.Commands;
using WorkoutLogs.Application.Contracts.Features.Exercise.Commands;
using WorkoutLogs.Application.Middleware;

namespace WorkoutLogs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DifficultyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateDifficulty([FromBody] CreateDifficultyCommand createDifficultyCommand)
        {
            try
            {
                var id = await _mediator.Send(createDifficultyCommand);

                return Ok($"Difficulty level is created successfully with ID: {id}");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }
        }
    }
}
