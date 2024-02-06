using AutoMapper;
using FluentAssertions;
using Moq;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class CreateExerciseTypeCommandHandlerTests
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
        public async Task Handle_ValidCommand_CreatesExerciseType()
        {
            // Arrange
            var command = new CreateExerciseTypeCommand { Name = "Test Exercise Type" };
            var handler = new CreateExerciseTypeCommandHandler(_exerciseTypeRepositoryMock.Object, _mapperMock.Object);

            _mapperMock.Setup(m => m.Map<ExerciseType>(command)).Returns(new ExerciseType());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _exerciseTypeRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<ExerciseType>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateExerciseTypeCommand { Name = null }; // Invalid command
            var handler = new CreateExerciseTypeCommandHandler(_exerciseTypeRepositoryMock.Object, _mapperMock.Object);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
            exception.Errors.ContainsKey("Name").Should().BeTrue();
            exception.Errors["Name"].Should().Contain("'Name' must not be empty.");
        }

    }

}