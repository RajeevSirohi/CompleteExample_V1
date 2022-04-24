using CompleteExample.Entities;
using CompleteExample.Models.Models.Response;
using CompleteExample.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteExample.Logic.Repositories
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InstructorId"></param>
        /// <returns></returns>
        Task<List<StudentGrade>> GetStudentsGradesByIntsructorId(int InstructorId);

        /// <summary>
        /// Gets the top students in courses as identified by Id Parameter
        /// </summary>
        /// <param name="courseId">optional course Id</param>
        /// <param name="filter">filters how many top grades to be considered</param>
        /// <returns></returns>
        Task<List<TopStudentsByCourse>> GetTopStudentsByCourses(int? courseId, int filter);

        /// <summary>
        /// Enrolls a new student
        /// </summary>
        /// <param name="enrollmentRequest"></param>
        /// <returns></returns>
        Task<Enrollment> EnrollStudentInCourse(Enrollment enrollmentRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enrollmentRequest"></param>
        /// <returns></returns>
        Task<Enrollment> UpdateStudentGrade(Enrollment enrollmentRequest);
    }
}
