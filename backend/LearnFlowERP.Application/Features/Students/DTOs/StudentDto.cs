using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Students.DTOs
{
    public class StudentDto
    {
        public long StudentId { get; set; }

        public long? UserId { get; set; }

        public string EnrollmentNo { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        public string? DocumentUrl { get; set; }

        public string Email { get; set; } = null!;
    }
}
