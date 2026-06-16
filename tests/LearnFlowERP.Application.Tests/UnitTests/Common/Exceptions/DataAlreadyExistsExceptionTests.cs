using FluentAssertions;
using LearnFlowERP.Application.Common.Exceptions;

namespace LearnFlowERP.Application.Tests.UnitTests.Common.Exceptions
{
    public class DataAlreadyExistsExceptionTests
    {
        [Fact]
        public void Should_Set_Message()
        {
            var ex =
                new DataAlreadyExistsException(
                    "Already exists");

            ex.Message.Should()
                .Be("Already exists");
        }
    }
}