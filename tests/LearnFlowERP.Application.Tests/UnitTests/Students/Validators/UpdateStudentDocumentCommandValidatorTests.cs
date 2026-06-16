using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Students.Commands.UpdateStudentDocument;

namespace LearnFlowERP.Application.Tests.UnitTests.Students.Validators
{
    public class UpdateStudentDocumentCommandValidatorTests
    {
        private readonly UpdateStudentDocumentCommandValidator _validator =
            new();

        [Fact]
        public void Should_Fail_When_StudentId_Invalid()
        {
            var command =
                new UpdateStudentDocumentCommand(
                    0,
                    "https://test.com/doc.pdf");

            var result =
                _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(
                x => x.StudentId);
        }

        [Fact]
        public void Should_Fail_When_DocumentUrl_Empty()
        {
            var command =
                new UpdateStudentDocumentCommand(
                    1,
                    "");

            var result =
                _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(
                x => x.DocumentUrl);
        }

        [Fact]
        public void Should_Pass_When_Request_Is_Valid()
        {
            var command =
                new UpdateStudentDocumentCommand(
                    1,
                    "https://test.com/document.pdf");

            var result =
                _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
