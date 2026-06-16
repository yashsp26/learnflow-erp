using FluentAssertions;
using LearnFlowERP.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace LearnFlowERP.Application.Tests.UnitTests.Common.Behaviors
{
    public class PerformanceBehaviorTests
    {
        private readonly Mock<
            ILogger<
                PerformanceBehavior<TestRequest, string>>> _logger;

        public PerformanceBehaviorTests()
        {
            _logger =
                new Mock<
                    ILogger<
                        PerformanceBehavior<TestRequest, string>>>();
        }

        [Fact]
        public async Task Should_Return_Response()
        {
            var behavior =
                new PerformanceBehavior<TestRequest, string>(
                    _logger.Object);

            var result =
                await behavior.Handle(
                    new TestRequest("John"),
                    () => Task.FromResult("success"),
                    CancellationToken.None);

            result.Should().Be("success");
        }

        [Fact]
        public async Task Should_Call_Next()
        {
            var behavior =
                new PerformanceBehavior<TestRequest, string>(
                    _logger.Object);

            var nextCalled = false;

            RequestHandlerDelegate<string> next = () =>
            {
                nextCalled = true;
                return Task.FromResult("success");
            };

            await behavior.Handle(
                new TestRequest("John"),
                next,
                CancellationToken.None);

            nextCalled.Should().BeTrue();
        }
    }
}