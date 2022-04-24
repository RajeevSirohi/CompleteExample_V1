using CompleteExample.Entities;
using CompleteExample.Models.Models.Response;
using CompleteExample.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteExample.Logic.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CompleteExampleDBContext _completeExampleDBContext;

        public StudentRepository(CompleteExampleDBContext completeExampleDBContext)
        {
            _completeExampleDBContext = completeExampleDBContext;
        }

        ///<inheritdoc cref="IStudentRepository.EnrollStudentInCourse"/>
        public async Task<Enrollment> EnrollStudentInCourse(Enrollment enrollmentRequest)
        {
            try
            {
                var enrollMent = await _completeExampleDBContext.Enrollment.AddAsync(enrollmentRequest);
                await _completeExampleDBContext.SaveChangesAsync();
                return enrollMent.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<inheritdoc cref="IStudentRepository.GetStudentsGradesByIntsructorId"/>
        public async Task<List<StudentGrade>> GetStudentsGradesByIntsructorId(int Id)
        {
            var listStudentGrades = new List<StudentGrade>();
            var coursesByInstructor = _completeExampleDBContext.Courses.Where(c => c.InstructorId == Id).Select(c => c.CourseId).ToList();
            foreach (var course in coursesByInstructor)
            {
                var enrollMent = _completeExampleDBContext.Enrollment.Where(c => c.CourseId == course).ToList();
                enrollMent.ForEach(x => listStudentGrades.Add(new StudentGrade
                {
                    CourseId = course,
                    Grade = x.Grade,
                    Id = x.StudentId,
                    Name = _completeExampleDBContext.Students.Where(c => c.StudentId == x.StudentId).Select(x => String.Concat(x.FirstName + " " + x.LastName)).FirstOrDefault()
                }));
            }
            return listStudentGrades;
        }

        ///<inheritdoc cref="IStudentRepository.GetTopStudentsByCourses"/>
        public async Task<List<TopStudentsByCourse>> GetTopStudentsByCourses(int? courseId, int filter)
        {
            var studentList = new List<TopStudentsByCourse>();
            var courseIds = new List<int>();
            if (courseId == null)
            {
                courseIds = _completeExampleDBContext.Courses.Select(x => x.CourseId).ToList();
            }
            else
            {
                courseIds = _completeExampleDBContext.Courses.Where(x => x.CourseId == courseId).Select(x => x.CourseId).ToList();
            }
            var enrollmentLists = new List<Enrollment>();
            courseIds.ForEach(c => enrollmentLists.AddRange(_completeExampleDBContext.Enrollment.OrderByDescending(x => x.Grade).Where(x => x.CourseId == c).Take(filter)));

            enrollmentLists.ForEach(x => studentList.Add(new TopStudentsByCourse
            {
                CourseId = x.CourseId,
                StudentGradeList = new List<StudentGrade>()
                {
                    new StudentGrade()
                    {
                        Grade = x.Grade,
                        Id = x.StudentId,
                        Name = _completeExampleDBContext.Students.Where(c => c.StudentId == x.StudentId).Select(x => String.Concat(x.FirstName + " " + x.LastName)).FirstOrDefault()
                    }
                }
            }));
            return studentList;
        }

        ///<inheritdoc cref="IStudentRepository.UpdateStudentGrade(Enrollment)"/>
        public async Task<Enrollment> UpdateStudentGrade(Enrollment enrollmentRequest)
        {
            try
            {
                var enrollMent = _completeExampleDBContext.Enrollment.Update(enrollmentRequest);
                await _completeExampleDBContext.SaveChangesAsync();
                return enrollMent.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



