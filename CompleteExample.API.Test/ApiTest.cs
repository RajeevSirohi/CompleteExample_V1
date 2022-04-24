using CompleteExample.API.Controllers;
using CompleteExample.Entities;
using CompleteExample.Logic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace CompleteExample.API.Test
{
    public class Tests
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly StudentController _studentController;

        public Tests()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _studentController = new StudentController(_studentRepository.Object);
        }

        [Test]
        public async Task Verify_that_EnrollStudent_returns_201_When_Valid_values_Are_Passed()
        {
            _studentRepository.Setup(x => x.EnrollStudentInCourse(It.IsAny<Enrollment>())).Returns(Task.FromResult(new Enrollment { CourseId = 10, EnrollmentId = 1, Grade = 21, StudentId = 3 }));
            var response = await _studentController.EnrollStudent(It.IsAny<Enrollment>());
            Assert.IsInstanceOf<CreatedResult>(response.Result);
            var enrolled = ((CreatedResult)response.Result).Value;
            Assert.IsNotNull(enrolled);
            Assert.IsInstanceOf<Enrollment>(enrolled);
        }
    }
}