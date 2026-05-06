using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LearnFlowERP.Application.Features.Students.Commands.UpdateStudentDocument
{
    public record UpdateStudentDocumentCommand(
     long StudentId,
     string DocumentUrl
 ) : IRequest<Unit>;
}
