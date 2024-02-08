using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using FluentAssertions;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class GetAllExerciseGroupsByExerciseTypeIdQueryHandlerTests
    {
        private Mock<IExerciseGroupRepository> _exerciseGroupRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IExerciseTypeRepository> _exerciseTypeRepositoryMock;
        private GetAllExerciseGroupsByExerciseTypeIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _exerciseGroupRepositoryMock = new Mock<IExerciseGroupRepository>();
            _mapperMock = new Mock<IMapper>();
            _exerciseTypeRepositoryMock = new Mock<IExerciseTypeRepository>();

            _handler = new GetAllExerciseGroupsByExerciseTypeIdQueryHandler(
                _exerciseGroupRepositoryMock.Object,
                _mapperMock.Object,
                _exerciseTypeRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_WithValidRequest_ReturnsExerciseGroups()
        {
            // Arrange
            var query = new GetAllExerciseGroupsByExerciseTypeIdQuery { ExerciseTypeId = 1 };

            var expectedExerciseGroups = new List<ExerciseGroup>
        {
            new ExerciseGroup { Id = 1, Name = "Group 1" },
            new ExerciseGroup { Id = 2, Name = "Group 2" }
        };

            var expectedExerciseGroupDtos = new List<ExerciseGroupDto>
        {
            new ExerciseGroupDto { Id = 1, Name = "Group 1" },
            new ExerciseGroupDto { Id = 2, Name = "Group 2" }
        };

            _exerciseTypeRepositoryMock.Setup(x => x.ExerciseTypeExists(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _exerciseGroupRepositoryMock.Setup(x => x.GetAllByExerciseTypeIdAsync(It.IsAny<int>())).ReturnsAsync(expectedExerciseGroups);
            _mapperMock.Setup(x => x.Map<IEnumerable<ExerciseGroupDto>>(It.IsAny<IEnumerable<ExerciseGroup>>())).Returns(expectedExerciseGroupDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<ExerciseGroupDto>>(result);
            CollectionAssert.AreEqual(expectedExerciseGroupDtos, result);
        }

        [Test]
        public void Handle_WithInvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var query = new GetAllExerciseGroupsByExerciseTypeIdQuery(); // No ExerciseTypeId provided

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(query, CancellationToken.None));
            ex.Errors.ContainsKey("ExerciseTypeId").Should().BeTrue();
            ex.Errors["ExerciseTypeId"].Should().Contain("Exercise type ID must be greater than 0");
            ex.Errors["ExerciseTypeId"].Should().Contain("Exercise type does not exist");
        }
    }

}
