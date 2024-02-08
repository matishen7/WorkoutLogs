using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using FluentAssertions;
using WorkoutLogs.Application.Middleware;

namespace WorkoutLogs.UnitTests
{
    namespace WorkoutLogs.Application.UnitTests.Features.ExerciseGroups.Commands
    {
        [TestFixture]
        public class UpdateExerciseGroupTests
        {
            private Mock<IExerciseGroupRepository> _mockExerciseGroupRepository;
            private Mock<IExerciseTypeRepository> _mockExerciseTypeRepository;
            private Mock<IMapper> _mockMapper;

            [SetUp]
            public void Setup()
            {
                _mockExerciseGroupRepository = new Mock<IExerciseGroupRepository>();
                _mockExerciseTypeRepository = new Mock<IExerciseTypeRepository>();
                _mockMapper = new Mock<IMapper>();
            }

            [Test]
            public async Task UpdateExerciseGroupHandler_WithValidCommand_ShouldUpdateExerciseGroup()
            {
                // Arrange
                var handler = new UpdateExerciseGroupCommandHandler(_mockExerciseGroupRepository.Object, _mockMapper.Object, _mockExerciseTypeRepository.Object);
                var command = new UpdateExerciseGroupCommand
                {
                    Id = 1,
                    Name = "Updated Name",
                    ExerciseTypeId = 2
                };
                var existingExerciseGroup = new ExerciseGroup { Id = 1, Name = "Existing Name", ExerciseTypeId = 1 };

                _mockExerciseGroupRepository.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
                _mockExerciseGroupRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingExerciseGroup);
                _mockExerciseTypeRepository.Setup(repo => repo.ExerciseTypeExists(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(true);

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                _mockExerciseGroupRepository.Verify(repo => repo.UpdateAsync(existingExerciseGroup), Times.Once);
            }

            [Test]
            public void UpdateExerciseGroupHandler_WithInvalidCommand_ShouldThrowValidationException()
            {
                // Arrange
                var handler = new UpdateExerciseGroupCommandHandler(_mockExerciseGroupRepository.Object, _mockMapper.Object, _mockExerciseTypeRepository.Object);
                var command = new UpdateExerciseGroupCommand(); // Invalid command with missing properties

                // Act & Assert
                var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
                ex.Errors.ContainsKey("Id").Should().BeTrue();
                ex.Errors.ContainsKey("Name").Should().BeTrue();
                ex.Errors.ContainsKey("ExerciseTypeId").Should().BeTrue();
                ex.Errors["Id"].Should().Contain("'Id' must not be empty.");
                ex.Errors["Id"].Should().Contain("Exercise does not exist.");
                ex.Errors["Name"].Should().Contain("'Name' must not be empty.");
                ex.Errors["ExerciseTypeId"].Should().Contain("'Exercise Type Id' must not be empty.");
                ex.Errors["ExerciseTypeId"].Should().Contain("Exercise type does not exist.");
            }
        }
    }

}
