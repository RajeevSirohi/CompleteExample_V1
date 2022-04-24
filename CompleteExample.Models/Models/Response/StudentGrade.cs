using System;

namespace CompleteExample.Models.Response
{
    public class StudentGrade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public Decimal? Grade { get; set; }

    }
}