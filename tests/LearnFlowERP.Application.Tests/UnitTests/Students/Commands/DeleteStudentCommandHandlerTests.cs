using FluentAssertions;
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Students.Commands.DeleteStudent;
using LearnFlowERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LearnFlowERP.Application.Tests.UnitTests.Students.Commands
{
    public class DeleteStudentCommandHandlerTests
    {
        [Fact]
        public async Task Should_Throw_When_Student_Not_Found()
        {
            var context =
                new Mock<IApplicationDbContext>();

            var students =
                new Mock<DbSet<Student>>();

            context.Setup(x => x.Students)
                .Returns(students.Object);

            var handler =
                new DeleteStudentCommandHandler(
                    context.Object);

            await FluentActions
                .Invoking(() =>
                    handler.Handle(
                        new DeleteStudentCommand(1),
                        CancellationToken.None))
                .Should()
                .ThrowAsync<NotFoundException>();
        }
    }
}
