using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Sessions.Commands;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class UpdateSessionCommandHandlerTests
    {
        private Mock<ISessionRepository> _sessionRepositoryMock;
        private UpdateSessionCommandHandler _handler;
        private UpdateSessionCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _sessionRepositoryMock = new Mock<ISessionRepository>();
            _handler = new UpdateSessionCommandHandler(_sessionRepositoryMock.Object);
            _validator = new UpdateSessionCommandValidator(_sessionRepositoryMock.Object);
        }

        [Test]
        public async Task UpdateSession_Success()
        {
            // Arrange
            var command = new UpdateSessionCommand { Id = 1 };
            _sessionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Session());
            _sessionRepositoryMock.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(Unit.Value, result);
            _sessionRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Session>()), Times.Once);

        }
        [Test]
        public async Task UpdateSessionIdDoesNotExist_ThrowsException()
        {
            // Arrange
            var command = new UpdateSessionCommand { Id = 1 };
            _sessionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Session());
            _sessionRepositoryMock.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            // Assert
            _sessionRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Session>()), Times.Never);
            result.Errors.ContainsKey("Id").Should().BeTrue();
            result.Errors["Id"].Should().Contain("Session does not exist.");

        }
        [Test]
        public async Task UpdateSessionValidationErrors_ThrowsException()
        {
            // Arrange
            var command = new UpdateSessionCommand { Id = 0 };
            _sessionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Session());
            //_sessionRepositoryMock.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            // Assert
            _sessionRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Session>()), Times.Never);
            result.Errors.ContainsKey("Id").Should().BeTrue();
            result.Errors["Id"].Should().Contain("SessionId must be greater than 0");

        }

    }

}
