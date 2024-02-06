using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Commands;
using WorkoutLogs.Application.Contracts.Features.Difficulties;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Application.Middleware;
using FluentAssertions;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class CreateDifficultyCommandHandlerTests
    {
        private Mock<IDifficultyRepository> _difficultyRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _difficultyRepositoryMock = new Mock<IDifficultyRepository>();
        }

        [Test]
        public async Task Handle_ValidCommand_CreatesDifficulty()
        {
            // Arrange
            var command = new CreateDifficultyCommand { Level = "Easy" };
            var handler = new CreateDifficultyCommandHandler(_difficultyRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _difficultyRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Core.Difficulty>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsValidationExceptionWithMessages()
        {
            // Arrange
            var invalidCommand = new CreateDifficultyCommand { Level = "" }; // Invalid command
            var handler = new CreateDifficultyCommandHandler(_difficultyRepositoryMock.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, CancellationToken.None));
            ex.Errors.ContainsKey("Level").Should().BeTrue();
            ex.Errors["Level"].Should().Contain("'Level' must not be empty.");
        }
    }

}
