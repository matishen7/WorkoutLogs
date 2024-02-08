using AutoMapper;
using FluentValidation;
using WorkoutLogs.Application.Middleware;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Sessions.Commands;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using ValidationException = WorkoutLogs.Application.Middleware.ValidationException;
using FluentAssertions;

namespace WorkoutLogs.UnitTests
{
    [TestFixture]
    public class CreateSessionCommandHandlerTests
    {
        private Mock<ISessionRepository> _mockSessionRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IMemberRepository> _mockMemberRepository;
        private CreateSessionCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockMemberRepository = new Mock<IMemberRepository>();
            _handler = new CreateSessionCommandHandler(_mockSessionRepository.Object, _mockMapper.Object, _mockMemberRepository.Object);
        }

        [Test]
        public void Handle_WithValidCommand_ReturnsSessionId()
        {
            // Arrange
            var command = new CreateSessionCommand { MemberId = 1 };
            var session = new Session { Id = 1 };
            _mockMapper.Setup(m => m.Map<Session>(command)).Returns(session);
            _mockMemberRepository.Setup(m => m.Exists(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = _handler.Handle(command, CancellationToken.None).Result;

            // Assert
            Assert.AreEqual(session.Id, result);
        }

        [Test]
        public void Handle_WithInvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateSessionCommand();
            _mockMemberRepository.Setup(m => m.Exists(It.IsAny<int>())).ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            ex.Errors.ContainsKey("MemberId").Should().BeTrue();
            ex.Errors["MemberId"].Should().Contain("MemberId must be greater than 0");
        }

        [Test]
        public void Handle_WithNonExistingMember_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateSessionCommand { MemberId = 1 };
            _mockMemberRepository.Setup(m => m.Exists(1)).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            ex.Errors.ContainsKey("MemberId").Should().BeTrue();
            ex.Errors["MemberId"].Should().Contain("Member does not exist.");
        }
    }

}
