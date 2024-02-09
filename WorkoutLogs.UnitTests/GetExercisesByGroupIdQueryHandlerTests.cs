using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Exercises.Queries;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class GetExercisesByGroupIdQueryHandlerTests
    {
        private Mock<IExerciseRepository> _exerciseRepositoryMock;
        private Mock<IExerciseGroupRepository> _exerciseGroupRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetExercisesByGroupIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _exerciseRepositoryMock = new Mock<IExerciseRepository>();
            _exerciseGroupRepositoryMock = new Mock<IExerciseGroupRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetExercisesByGroupIdQueryHandler(
                _exerciseRepositoryMock.Object,
                _exerciseGroupRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ValidQuery_ReturnsExercises()
        {
            // Arrange
            var query = new GetExercisesByGroupIdQuery { ExerciseGroupId = 1 };
            var exercises = new List<Exercise>()
            {
                new Exercise { Id = 1, Name = "Exercise 1", ExerciseGroupId = 1 },
                new Exercise { Id = 2, Name = "Exercise 2", ExerciseGroupId = 1 },
            };
            var exerciseDtos = new List<ExerciseDto>()
            {
                new ExerciseDto { Id = 1, Name = "Exercise 1" },
                new ExerciseDto { Id = 2, Name = "Exercise 2" },
            };
            _exerciseRepositoryMock.Setup(repo => repo.GetByGroupIdAsync(query.ExerciseGroupId)).ReturnsAsync(exercises);
            _exerciseGroupRepositoryMock.Setup(repo => repo.ExistsAsync(query.ExerciseGroupId)).ReturnsAsync(true);
            _mapperMock.Setup(mapper => mapper.Map<List<ExerciseDto>>(exercises)).Returns(exerciseDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exerciseDtos, result);
        }
    }

}
