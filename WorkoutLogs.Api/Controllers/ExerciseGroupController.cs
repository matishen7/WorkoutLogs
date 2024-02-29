using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries;
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

        [HttpPost("CreateExerciseGroup")]
        public async Task<ActionResult<int>> CreateExerciseGroup([FromBody] CreateExerciseGroupCommand createExerciseGroupCommand)
        {
            try
            {
                var exerciseGroupId = await _mediator.Send(createExerciseGroupCommand);

                return Ok(exerciseGroupId);
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

        [HttpPut("UpdateExerciseGroup")]
        public async Task<IActionResult> UpdateExerciseGroup([FromBody] UpdateExerciseGroupCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }
        }

        [HttpGet("byExerciseType/{exerciseTypeId}")]
        public async Task<ActionResult<IEnumerable<ExerciseGroupDto>>> GetExerciseGroupsByExerciseTypeId(int exerciseTypeId, CancellationToken cancellationToken)
        {
            var query = new GetAllExerciseGroupsByExerciseTypeIdQuery { ExerciseTypeId = exerciseTypeId };
            try
            {
                var exerciseGroupDtos = await _mediator.Send(query, cancellationToken);
                return Ok(exerciseGroupDtos);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request {ex.Message}");
            }

        }
    }
}
