using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class DeleteDifficultyCommandHandlerTests
    {
        private Mock<IDifficultyRepository> _mockDifficultyRepository;
        private DeleteDifficultyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockDifficultyRepository = new Mock<IDifficultyRepository>();
            _handler = new DeleteDifficultyCommandHandler(_mockDifficultyRepository.Object);
        }

        [Test]
        public void Handle_WithValidId_ShouldDeleteDifficulty()
        {
            // Arrange
            var difficultyId = 1;
            var command = new DeleteDifficultyCommand { Id = difficultyId };

            _mockDifficultyRepository.Setup(repo => repo.GetByIdAsync(difficultyId)).ReturnsAsync(new Difficulty { Id = difficultyId });

            // Act
            var result = _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDifficultyRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Difficulty>()), Times.Once);
        }

        [Test]
        public void Handle_WithInvalidId_ShouldThrowNotFoundException()
        {
            // Arrange
            var difficultyId = 1;
            var command = new DeleteDifficultyCommand { Id = difficultyId };

            _mockDifficultyRepository.Setup(repo => repo.GetByIdAsync(difficultyId)).ReturnsAsync((Difficulty)null);

            // Act + Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
