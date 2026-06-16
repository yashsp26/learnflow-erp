using FluentAssertions;
using LearnFlowERP.Application.Common.Exceptions;

namespace LearnFlowERP.Application.Tests.UnitTests.Common.Exceptions
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void Should_Set_Message()
        {
            var ex =
                new NotFoundException(
                    "User not found");

            ex.Message.Should()
                .Be("User not found");
        }
    }
}