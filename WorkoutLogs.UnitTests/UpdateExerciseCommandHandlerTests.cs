using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Commands;
using WorkoutLogs.Application.Contracts.Features.Exercises.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class UpdateExerciseCommandHandlerTests
    {
        private Mock<IExerciseRepository> _mockExerciseRepository;
        private Mock<IExerciseGroupRepository> _mockExerciseGroupRepository;
        private IMapper _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockExerciseRepository = new Mock<IExerciseRepository>();
            _mockExerciseGroupRepository = new Mock<IExerciseGroupRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateExerciseCommand, Exercise>();
            });
            _mockMapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public void Handle_WhenExerciseExists_ShouldUpdateExercise()
        {
            // Arrange
            var handler = new UpdateExerciseCommandHandler(
                _mockExerciseRepository.Object,
                _mockExerciseGroupRepository.Object,
                _mockMapper);

            var command = new UpdateExerciseCommand
            {
                ExerciseId = 1,
                TutorialUrl = "http://updated-tutorial.com",
            };

            var existingExercise = new Exercise { Id = 1 };
            _mockExerciseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingExercise);
            _mockExerciseRepository.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(true);
            _mockExerciseGroupRepository.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.DoesNotThrowAsync(() => result);
            _mockExerciseRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Exercise>()), Times.Once);
        }

        [Test]
        public void Handle_WhenExerciseNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var handler = new UpdateExerciseCommandHandler(
                _mockExerciseRepository.Object,
                _mockExerciseGroupRepository.Object,
                _mockMapper);

            var command = new UpdateExerciseCommand
            {
                ExerciseId = 1,
                TutorialUrl = "http://updated-tutorial.com",
            };
            _mockExerciseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Exercise)null);
            _mockExerciseRepository.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(true);
            _mockExerciseGroupRepository.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_WhenExerciseValidationErrors_ShouldThrowBadrequest()
        {
            // Arrange
            var handler = new UpdateExerciseCommandHandler(
                _mockExerciseRepository.Object,
                _mockExerciseGroupRepository.Object,
                _mockMapper);

            var command = new UpdateExerciseCommand
            {
                ExerciseId = 1,
                TutorialUrl = "http://updated-tutorial.com",
            };
            _mockExerciseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((new Exercise()));
            _mockExerciseRepository.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(false);
            _mockExerciseGroupRepository.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
            ex.Errors.ContainsKey("ExerciseId").Should().BeTrue();
            ex.Errors["ExerciseId"].Should().Contain("Exercise with this ID does not exist.");
        }

        [Test]
        public void Handle_WhenExerciseIdInvalid_ShouldThrowBadrequest()
        {
            // Arrange
            var handler = new UpdateExerciseCommandHandler(
                _mockExerciseRepository.Object,
                _mockExerciseGroupRepository.Object,
                _mockMapper);

            var command = new UpdateExerciseCommand
            {
                ExerciseId = 0,
                TutorialUrl = "",
            };
            _mockExerciseRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((new Exercise()));
            _mockExerciseRepository.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(false);
            _mockExerciseGroupRepository.Setup(repo => repo.ExistsAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
            ex.Errors.ContainsKey("TutorialUrl").Should().BeTrue();
            ex.Errors.ContainsKey("ExerciseId").Should().BeTrue();
            ex.Errors["ExerciseId"].Should().Contain("ExerciseId must be greater than 0");
        }
    }
}
