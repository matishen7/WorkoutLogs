using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Exercises.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using FluentAssertions;
using AutoMapper;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class CreateExerciseCommandHandlerTests
    {
        private Mock<IExerciseRepository> _exerciseRepositoryMock;
        private Mock<IExerciseGroupRepository> _exerciseGroupRepositoryMock;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _exerciseRepositoryMock = new Mock<IExerciseRepository>();
            _exerciseGroupRepositoryMock = new Mock<IExerciseGroupRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task Handle_ValidCommand_CreatesExercise()
        {
            // Arrange
            var command = new CreateExerciseCommand { Name = "Test Exercise", TutorialUrl = "http://example.com", ExerciseGroupId = 1 };
            var exercise = new Exercise { Name = "Test Exercise", TutorialUrl = "http://example.com", ExerciseGroupId = 1 };
            _exerciseGroupRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
            _mapper.Setup(m => m.Map<Exercise>(command)).Returns(exercise);
            var handler = new CreateExerciseCommandHandler(_exerciseRepositoryMock.Object, _exerciseGroupRepositoryMock.Object, _mapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _exerciseRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Core.Exercise>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsValidationExceptionWithMessages()
        {
            // Arrange
            var invalidCommand = new CreateExerciseCommand { Name = "", TutorialUrl = "http://example.com", ExerciseGroupId = 0 }; // Invalid command
            var handler = new CreateExerciseCommandHandler(_exerciseRepositoryMock.Object, _exerciseGroupRepositoryMock.Object, _mapper.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, CancellationToken.None));
            ex.Errors.ContainsKey("Name").Should().BeTrue();
            ex.Errors["Name"].Should().Contain("'Name' must not be empty.");
            ex.Errors.ContainsKey("ExerciseGroupId").Should().BeTrue();
            ex.Errors["ExerciseGroupId"].Should().Contain("'Exercise Group Id' must not be empty.");
        }

        [Test]
        public async Task Handle_InvalidExerciseGroupId_ThrowsValidationExceptionWithMessage()
        {
            // Arrange
            var invalidCommand = new CreateExerciseCommand { Name = "Test Exercise", TutorialUrl = "http://example.com", ExerciseGroupId = 999 }; // Invalid ExerciseGroupId
            _exerciseGroupRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
            var handler = new CreateExerciseCommandHandler(_exerciseRepositoryMock.Object, _exerciseGroupRepositoryMock.Object, _mapper.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, CancellationToken.None));
            ex.Errors.ContainsKey("ExerciseGroupId").Should().BeTrue();
            ex.Errors["ExerciseGroupId"].Should().Contain("Exercise group does not exist.");
        }
    }

}
