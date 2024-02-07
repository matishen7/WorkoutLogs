using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Commands;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Queries;
using WorkoutLogs.Application.Contracts.Features.Exercises.Commands;
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

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateDifficulty([FromBody] UpdateDifficultyCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok($"Difficulty level is updated successfully with ID: {command.Id}");
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDifficultyById(int id)
        {
            try
            {
                var query = new GetDifficultyByIdQuery { Id = id };
                var difficultyDto = await _mediator.Send(query);
                return Ok(difficultyDto);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DifficultyDto>>> GetAllDifficulties()
        {
            var difficulties = await _mediator.Send(new GetAllDifficultiesQuery());
            return Ok(difficulties);
        }
    }
}
