using FluentValidation;

namespace LearnFlowERP.Application.Tests.UnitTests.Common.Behaviors
{
    public class TestRequestValidator
    : AbstractValidator<TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}