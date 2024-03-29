﻿using Moq;
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
using AutoMapper;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class CreateDifficultyCommandHandlerTests
    {
        private Mock<IDifficultyRepository> _difficultyRepositoryMock;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _difficultyRepositoryMock = new Mock<IDifficultyRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task Handle_ValidCommand_CreatesDifficulty()
        {
            // Arrange
            var command = new CreateDifficultyCommand { Level = "Easy" };
            var difficulty = new Difficulty { Level = "Easy" };
            var handler = new CreateDifficultyCommandHandler(_difficultyRepositoryMock.Object, _mapper.Object);
            _mapper.Setup(m => m.Map<Difficulty>(command)).Returns(difficulty);
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
            var handler = new CreateDifficultyCommandHandler(_difficultyRepositoryMock.Object, _mapper.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, CancellationToken.None));
            ex.Errors.ContainsKey("Level").Should().BeTrue();
            ex.Errors["Level"].Should().Contain("'Level' must not be empty.");
        }
    }

}
