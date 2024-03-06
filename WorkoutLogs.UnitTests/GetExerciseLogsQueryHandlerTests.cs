using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Queries;
using WorkoutLogs.Application.MappingProfiles;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class GetExerciseLogsQueryHandlerTests
    {
        private GetExerciseLogsQueryHandler _handler;
        private Mock<ISessionRepository> _mockSessionRepository;
        private Mock<IExerciseLogRepository> _mockExerciseLogRepository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockExerciseLogRepository = new Mock<IExerciseLogRepository>();

            // Configure AutoMapper
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ExerciseLogMappingProfiles>();
            });
            _mapper = configurationProvider.CreateMapper();

            _handler = new GetExerciseLogsQueryHandler(
                _mockSessionRepository.Object,
                _mockExerciseLogRepository.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_WhenSessionExists_ReturnsExerciseLogs()
        {
            // Arrange
            var sessionId = 1;
            var session = new Session { Id = sessionId };
            var exerciseLogs = new List<ExerciseLog>
            {
                new ExerciseLog { Id = 1, SessionId = sessionId, Sets = 3, Reps = 10, Weight = 50 },
                new ExerciseLog { Id = 2, SessionId = sessionId, Sets = 3, Reps = 12, Weight = 55 }
            };

            _mockSessionRepository.Setup(repo => repo.GetByIdAsync(sessionId))
                .ReturnsAsync(session);
            _mockExerciseLogRepository.Setup(repo => repo.GetExerciseLogsBySessionId(sessionId))
                .ReturnsAsync(exerciseLogs);

            // Act
            var result = await _handler.Handle(new GetExerciseLogsQuery { SessionId = sessionId }, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exerciseLogs.Count, result.Count);
        }

        [Test]
        public void Handle_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var sessionId = 1;
            _mockSessionRepository.Setup(repo => repo.GetByIdAsync(sessionId))
                .ReturnsAsync((Session)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetExerciseLogsQuery { SessionId = sessionId }, CancellationToken.None));
        }
    }
}
