namespace ContactExam.Models
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; }
    }
}
