using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{ 
    [TestFixture]
    public class CreateExerciseLogCommandHandlerTests
    {
        private CreateExerciseLogCommandHandler _handler;
        private Mock<IExerciseLogRepository> _exerciseLogRepositoryMock;
        private Mock<IExerciseRepository> _exerciseRepositoryMock;
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IDifficultyRepository> _difficultyRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _exerciseLogRepositoryMock = new Mock<IExerciseLogRepository>();
            _exerciseRepositoryMock = new Mock<IExerciseRepository>();
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _difficultyRepositoryMock = new Mock<IDifficultyRepository>();

            _handler = new CreateExerciseLogCommandHandler(
                _exerciseLogRepositoryMock.Object,
                _exerciseRepositoryMock.Object,
                _memberRepositoryMock.Object,
                _difficultyRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsExerciseLogId()
        {
            // Arrange
            var command = new CreateExerciseLogCommand
            {
                MemberId = 1, 
                Date = DateTime.Now,
                ExerciseId = 2,
                Sets = 3,
                Reps = 10, 
                Weight = 50,
                DifficultyId = 3, 
                AdditionalNotes = "This is a valid additional note."
            };


            _memberRepositoryMock.Setup(repo => repo.MemberExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _difficultyRepositoryMock.Setup(repo => repo.DifficultyExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _exerciseRepositoryMock.Setup(repo => repo.ExerciseExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _exerciseLogRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<ExerciseLog>()), Times.Once);
        }

        [Test]
        public async Task Handle_MemberDoesNotExist_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateExerciseLogCommand
            {
                MemberId = 0,
                Date = DateTime.Now,
                ExerciseId = 0,
                Sets = 0,
                Reps = 0,
                Weight = 0,
                DifficultyId = 0,
                AdditionalNotes = "This is a valid additional note."
            };

            // Setup
            _memberRepositoryMock.Setup(repo => repo.MemberExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);
            _difficultyRepositoryMock.Setup(repo => repo.DifficultyExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _exerciseRepositoryMock.Setup(repo => repo.ExerciseExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            ex.Errors.ContainsKey("MemberId").Should().BeTrue();
            ex.Errors["MemberId"].Should().Contain("'Member Id' must not be empty.");

            ex.Errors.ContainsKey("ExerciseId").Should().BeTrue();
            ex.Errors["ExerciseId"].Should().Contain("ExerciseId is required.");

            ex.Errors.ContainsKey("Sets").Should().BeTrue();
            ex.Errors["Sets"].Should().Contain("Sets must be greater than 0.");

            ex.Errors.ContainsKey("Reps").Should().BeTrue();
            ex.Errors["Reps"].Should().Contain("Reps must be greater than 0.");

            ex.Errors.ContainsKey("Weight").Should().BeTrue();
            ex.Errors["Weight"].Should().Contain("Weight must be greater than 0.");
            _exerciseLogRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<ExerciseLog>()), Times.Never);
        }
        [Test]
        public async Task Handle_DoesNotExist_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateExerciseLogCommand
            {
                MemberId = 99,
                Date = DateTime.Now,
                ExerciseId = 2,
                Sets = 3,
                Reps = 10,
                Weight = 50,
                DifficultyId = 3,
                AdditionalNotes = "This is a valid additional note."
            };

            // Setup
            _memberRepositoryMock.Setup(repo => repo.MemberExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(false);
            _difficultyRepositoryMock.Setup(repo => repo.DifficultyExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _exerciseRepositoryMock.Setup(repo => repo.ExerciseExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            ex.Errors.ContainsKey("MemberId").Should().BeTrue();
            ex.Errors["MemberId"].Should().Contain("Member does not exist.");
            _exerciseLogRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<ExerciseLog>()), Times.Never);

            ex.Errors.ContainsKey("ExerciseId").Should().BeTrue();
            ex.Errors["ExerciseId"].Should().Contain("Exercise does not exist.");

            ex.Errors.ContainsKey("DifficultyId").Should().BeTrue();
            ex.Errors["DifficultyId"].Should().Contain("Difficulty does not exist.");
        }
    }

}
