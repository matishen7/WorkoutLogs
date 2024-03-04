using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Queries;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class GetAllExerciseTypesHandlerTests
    {
        private Mock<IExerciseTypeRepository> _exerciseTypeRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _exerciseTypeRepositoryMock = new Mock<IExerciseTypeRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Test]
        public async Task Handle_ValidCommand_GetExerciseType()
        {
            // Arrange
            var command = new GetAllExerciseTypesQuery();
            var handler = new GetAllExerciseTypesQueryHandler(_exerciseTypeRepositoryMock.Object, _mapperMock.Object);
            var exerciseTypes = new List<ExerciseType>() { new ExerciseType() { Id = 1, Name = "Test1" } };
            _exerciseTypeRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(exerciseTypes);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _exerciseTypeRepositoryMock.Verify(repo => repo.GetAsync(), Times.Once);
        }
    }
}
