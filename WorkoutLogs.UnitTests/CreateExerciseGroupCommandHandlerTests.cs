using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroup.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class CreateExerciseGroupCommandHandlerTests
    {
        private Mock<IExerciseGroupRepository> _exerciseGroupRepositoryMock;
        private Mock<IExerciseTypeRepository> _exerciseTypeRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _exerciseGroupRepositoryMock = new Mock<IExerciseGroupRepository>();
            _exerciseTypeRepositoryMock = new Mock<IExerciseTypeRepository>();
        }

        [Test]
        public async Task Handle_ValidCommand_CreatesExerciseGroup()
        {
            // Arrange
            var command = new CreateExerciseGroupCommand { Name = "Test Exercise Group", ExerciseTypeId = 1 };
            var handler = new CreateExerciseGroupCommandHandler(_exerciseGroupRepositoryMock.Object, _exerciseTypeRepositoryMock.Object);
            _exerciseTypeRepositoryMock.Setup(repo => repo.ExerciseTypeExists(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _exerciseGroupRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<ExerciseGroup>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsValidationExceptionWithMessages()
        {
            // Arrange
            var invalidCommand = new CreateExerciseGroupCommand { Name = "", ExerciseTypeId = 0 }; // Invalid command
            var handler = new CreateExerciseGroupCommandHandler(_exerciseGroupRepositoryMock.Object, _exerciseTypeRepositoryMock.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, CancellationToken.None));
            ex.Errors.ContainsKey("Name").Should().BeTrue();
            ex.Errors["Name"].Should().Contain("'Name' must not be empty.");
            ex.Errors.ContainsKey("ExerciseTypeId").Should().BeTrue();
            ex.Errors["ExerciseTypeId"].Should().Contain("'Exercise Type Id' must not be empty.");
        }

        [Test]
        public async Task Handle_InvalidExerciseTypeId_ThrowsValidationExceptionWithMessage()
        {
            // Arrange
            var invalidCommand = new CreateExerciseGroupCommand { Name = "Test", ExerciseTypeId = 999 }; // Invalid ExerciseTypeId
            var handler = new CreateExerciseGroupCommandHandler(_exerciseGroupRepositoryMock.Object, _exerciseTypeRepositoryMock.Object);

            _exerciseTypeRepositoryMock.Setup(repo => repo.ExerciseTypeExists(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, CancellationToken.None));
            ex.Errors.ContainsKey("ExerciseTypeId").Should().BeTrue();
            ex.Errors["ExerciseTypeId"].Should().Contain("Exercise type does not exist.");
        }
    }
}
