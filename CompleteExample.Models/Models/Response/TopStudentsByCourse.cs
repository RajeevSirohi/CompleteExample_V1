using CompleteExample.Models.Response;
using System.Collections.Generic;

namespace CompleteExample.Models.Models.Response
{
    public class TopStudentsByCourse
    {
        public int CourseId { get; set; }
        public List<StudentGrade> StudentGradeList { get; set;}

    }
}
