using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Queries;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class GetDifficultyByIdQueryHandlerTests
    {
        private Mock<IDifficultyRepository> _mockRepository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IDifficultyRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Difficulty, DifficultyDto>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public async Task Handle_ExistingId_ReturnsDifficultyDto()
        {
            // Arrange
            var query = new GetDifficultyByIdQuery { Id = 1 };
            var existingDifficulty = new Difficulty { Id = 1, Level = "Easy" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(query.Id)).ReturnsAsync(existingDifficulty);
            var handler = new GetDifficultyByIdQueryHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(existingDifficulty.Id, result.Id);
            Assert.AreEqual(existingDifficulty.Level, result.Level);
        }

        [Test]
        public void Handle_NonExistentId_ThrowsNotFoundException()
        {
            // Arrange
            var query = new GetDifficultyByIdQuery { Id = 999 }; // Non-existent ID
            _mockRepository.Setup(repo => repo.GetByIdAsync(query.Id)).ReturnsAsync((Difficulty)null);
            var handler = new GetDifficultyByIdQueryHandler(_mockRepository.Object, _mapper);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }

}
