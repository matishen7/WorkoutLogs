using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Commands;
using WorkoutLogs.Application.Middleware;

namespace WorkoutLogs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateExerciseLog([FromBody] CreateExerciseLogCommand createExerciseLogCommand)
        {
            try
            {
                var id = await _mediator.Send(createExerciseLogCommand);

                return Ok($"Exercise log is created successfully with ID: {id}");
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
