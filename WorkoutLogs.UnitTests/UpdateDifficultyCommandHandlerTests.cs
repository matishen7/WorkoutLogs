using AutoMapper;
using MediatR;
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
    public class UpdateDifficultyCommandHandlerTests
    {
        private Mock<IDifficultyRepository> _mockRepository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IDifficultyRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateDifficultyCommand, Difficulty>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsUnit()
        {
            // Arrange
            var command = new UpdateDifficultyCommand { Id = 1, Level = "Updated Level" };
            var existingDifficulty = new Difficulty { Id = 1, Level = "Original Level" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(existingDifficulty);
            var handler = new UpdateDifficultyCommandHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(Unit.Value, result);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Difficulty>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new UpdateDifficultyCommand { Id = 1, Level = null }; // Invalid command
            var handler = new UpdateDifficultyCommandHandler(_mockRepository.Object, _mapper);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_NonExistentDifficulty_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpdateDifficultyCommand { Id = 999, Level = "Updated Level" }; // Non-existent ID
            _mockRepository.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((Difficulty)null);
            var handler = new UpdateDifficultyCommandHandler(_mockRepository.Object, _mapper);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }

}
