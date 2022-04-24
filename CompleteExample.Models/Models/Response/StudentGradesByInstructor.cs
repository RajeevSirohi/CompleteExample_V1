using System.Collections.Generic;

namespace CompleteExample.Models.Response
{
    public class StudentGradesByInstructor
    {
        public int InstructorId { get; set; }
        public List<StudentGrade> StudentGrades { get; set;}
    }
}
