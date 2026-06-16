using MediatR;

namespace LearnFlowERP.Application.Tests.UnitTests.Common.Behaviors
{
    public record TestRequest(string Name)
        : IRequest<string>;
}
