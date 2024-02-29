using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.Exercises.Commands;
using WorkoutLogs.Application.Contracts.Features.Exercises.Queries;
using WorkoutLogs.Application.Middleware;

namespace WorkoutLogs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateExercise")]
        public async Task<ActionResult<int>> CreateExercise([FromBody] CreateExerciseCommand createExerciseCommand)
        {
            try
            {
                var exerciseId = await _mediator.Send(createExerciseCommand);

                return Ok(exerciseId);
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

        [HttpGet("byGroupId/{groupId}")]
        public async Task<ActionResult<List<ExerciseDto>>> GetExercisesByGroupId(int groupId)
        {
            try
            {
                var query = new GetExercisesByGroupIdQuery { ExerciseGroupId = groupId };
                var exercises = await _mediator.Send(query);
                return Ok(exercises);
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

        [HttpPut]
        public async Task<IActionResult> UpdateExercise([FromBody] UpdateExerciseCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
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
