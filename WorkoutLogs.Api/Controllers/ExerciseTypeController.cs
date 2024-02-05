using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.ExerciseType.Commands;
using WorkoutLogs.Application.Middleware;

namespace WorkoutLogs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateExerciseType([FromBody] CreateExerciseTypeCommand createExerciseTypeCommand)
        {
            try
            {
                var projectId = await _mediator.Send(createExerciseTypeCommand);

                return Ok($"Project created successfully with ID: {projectId}");
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