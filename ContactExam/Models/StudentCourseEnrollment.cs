namespace ContactExam.Models
{
    public class StudentCourseEnrollment
    {
        public StudentCourseEnrollment() { }
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
