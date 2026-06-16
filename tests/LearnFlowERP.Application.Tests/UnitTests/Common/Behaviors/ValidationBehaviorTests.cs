using FluentAssertions;
using FluentValidation;
using LearnFlowERP.Application.Common.Behaviors;
using MediatR;

namespace LearnFlowERP.Application.Tests.UnitTests.Common.Behaviors
{
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task Should_Call_Next_When_Validation_Passes()
        {
            var validators =
                new List<IValidator<TestRequest>>
                {
                new TestRequestValidator()
                };

            var behavior =
                new ValidationBehavior<TestRequest, string>(
                    validators);

            var nextCalled = false;

            RequestHandlerDelegate<string> next = () =>
            {
                nextCalled = true;
                return Task.FromResult("success");
            };

            var result =
                await behavior.Handle(
                    new TestRequest("John"),
                    next,
                    CancellationToken.None);

            result.Should().Be("success");
            nextCalled.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Throw_When_Validation_Fails()
        {
            var validators =
                new List<IValidator<TestRequest>>
                {
                new TestRequestValidator()
                };

            var behavior =
                new ValidationBehavior<TestRequest, string>(
                    validators);

            Func<Task> act = async () =>
                await behavior.Handle(
                    new TestRequest(""),
                    () => Task.FromResult("success"),
                    CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>();
        }
    }
}