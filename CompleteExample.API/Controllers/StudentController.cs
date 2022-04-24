using CompleteExample.Entities;
using CompleteExample.Logic.Repositories;
using CompleteExample.Models.Models.Response;
using CompleteExample.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Get the grade given by instructor
        /// </summary>
        /// <param name="instructorId">The Instructor Identifier</param>
        /// <returns>A response specifying the requested data</returns>
        [HttpGet("GetGradesByInstructor/{instructorId}", Name = "GetGradesByInstructor")]
        [ProducesResponseType(typeof(StudentGradesByInstructor), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentGradesByInstructor>> GetGradesByInstructor(int instructorId)
        {
            var result = await _studentRepository.GetStudentsGradesByIntsructorId(instructorId);
            if(result == null)
                return NotFound();
            var response = new StudentGradesByInstructor()
            {
                InstructorId = instructorId,
                StudentGrades = result
            };
            return Ok(response);
        }

        /// <summary>
        /// Gets top grade students for a course or all courses 
        /// </summary>
        /// <param name="courseId">The identifier for course</param>
        /// <param name="filter">A filter to specify how many top results are needed</param>
        /// <returns>List of students with top grades</returns>
        [HttpGet("GetTopStudentsByCourse/{courseId?}", Name = "GetTopStudentsByCourse")]
        [ProducesResponseType(typeof(List<TopStudentsByCourse>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<TopStudentsByCourse>>> GetTopStudentsByCourse(int? courseId = null, [FromQuery] int filter = 3)
        {
            var result = await _studentRepository.GetTopStudentsByCourses(courseId, filter);
            if(result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Enrolls a student into a course
        /// </summary>
        /// <param name="enrollmentRequest">The enrollment request</param>
        /// <returns>Enrolled student</returns>
        [HttpPost("EnrollStudent", Name = "EnrollStudent")]
        [ProducesResponseType(typeof(Enrollment), 201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Enrollment>> EnrollStudent(Enrollment enrollmentRequest)
        {
            var result = await _studentRepository.EnrollStudentInCourse(enrollmentRequest);

            return Created("/GetEnrollMentRecord/" + result.EnrollmentId, result);
        }

        /// <summary>
        /// Updates the grade for a student
        /// </summary>
        /// <param name="gradeRequest">Update grade request</param>
        /// <returns>Status code specifying the success of operation</returns>
        [HttpPut("UpdateGrade", Name = "UpdateGrade")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateGradeOfStudent(Enrollment gradeRequest)
        {
            var result = await _studentRepository.UpdateStudentGrade(gradeRequest);
            return NoContent();
        }
    }
}
