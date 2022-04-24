using CompleteExample.Entities;
using CompleteExample.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteExample.Logic.TestsKD
{
    public class UnitTest1
    {
        private CompleteExampleDBContext _context;
        private IStudentRepository _repository;
        private List<Student> students;
        private List<Instructor> instr;
        private List<Course> courses;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            var optionBuilder = new DbContextOptionsBuilder<CompleteExampleDBContext>();
            optionBuilder.UseSqlServer(config.GetConnectionString("SchoolContext"));
            _context = new CompleteExampleDBContext(optionBuilder.Options);

            students = new List<Student>() {new Student
            {
                Email = "Test@test.com",
                FirstName = "Tester",
                LastName = "Testerson",
                TimeZone = "asia/delhi"
            }};
            _context.Students.AddRange(students);


            instr = new List<Instructor>{ new Instructor
            {
                Email = "Instrcut@test.com",
                FirstName = "Neo",
                LastName = "Anderson",
                StartDate = System.DateTime.Today
            }};

            courses = new List<Course> { new Course
            {
                Credits = 20,
                Description = "Test Description",
                InstructorId = 28,
                Title = "Test Title"
            } };

            _context.Courses.AddRange(courses);
            _context.Instructors.AddRange(instr);
            _context.SaveChanges();
            _repository = new StudentRepository(_context);
        }

        [Test]
        public async Task Verify_EnrollStudent_Returns_Entity_When_Valid_Values_Are_Passed()
        {
            //Arrange
            var enrollRequst = new Enrollment { CourseId = courses[0].CourseId, StudentId = students[0].StudentId };

            //Act
            var response = await _repository.EnrollStudentInCourse(enrollRequst);

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public async Task Verify_UpdateGrade_Returns_Valid_When_Values_Are_Passed()
        {
            //Arrange
            var enrollRequst = new Enrollment { CourseId = courses[0].CourseId, StudentId = students[0].StudentId };
            var response = await _repository.EnrollStudentInCourse(enrollRequst);
            enrollRequst.EnrollmentId = response.EnrollmentId;
            enrollRequst.Grade = 80;

            //Act
            response = await _repository.UpdateStudentGrade(enrollRequst);

            //Assert
            Assert.NotNull(response);
        }

        [Test]
        public async Task Verify_GetStudentsGradesByIntsructorId_Returns_Valid_When_Values_Are_Passed()
        {
            //Arrange
            var enrollRequst = new Enrollment { CourseId = courses[0].CourseId, StudentId = students[0].StudentId };
            var response = await _repository.EnrollStudentInCourse(enrollRequst);
            enrollRequst.EnrollmentId = response.EnrollmentId;
            enrollRequst.Grade = 80;

            //Act
           var finalResponse = await _repository.GetStudentsGradesByIntsructorId(28);

            //Assert
            Assert.NotNull(finalResponse);
        }
    }
}