using AutoMapper;
using Moq;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Queries;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class GetAllDifficultiesQueryHandlerTests
    {
        private Mock<IDifficultyRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private GetAllDifficultiesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IDifficultyRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllDifficultiesQueryHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsDifficulties()
        {
            // Arrange
                     var difficulties = new List<Difficulty>
                    {
                        new Difficulty { Id = 1, Level = "Easy" },
                        new Difficulty { Id = 2, Level = "Intermediate" },
                        new Difficulty { Id = 3, Level = "Advanced" }
                    };

                    var difficultyDtos = new List<DifficultyDto>
                    {
                        new DifficultyDto { Id = 1, Level = "Easy" },
                        new DifficultyDto { Id = 2, Level = "Intermediate" },
                        new DifficultyDto { Id = 3, Level = "Advanced" }
                    };


            _mockRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(difficulties);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<DifficultyDto>>(difficulties)).Returns(difficultyDtos);

            var query = new GetAllDifficultiesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(difficultyDtos.Count, result.Count());
        }
    }

}
