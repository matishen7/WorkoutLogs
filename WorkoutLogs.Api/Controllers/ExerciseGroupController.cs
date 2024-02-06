using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Commands;
using WorkoutLogs.Application.Middleware;

namespace WorkoutLogs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateExerciseGroup([FromBody] CreateExerciseGroupCommand createExerciseGroupCommand)
        {
            try
            {
                var exerciseGroupId = await _mediator.Send(createExerciseGroupCommand);

                return Ok($"Exercise group is created successfully with ID: {exerciseGroupId}");
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
