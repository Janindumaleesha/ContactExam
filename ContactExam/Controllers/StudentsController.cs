using ContactExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactExam.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ContactDbContext _context;

        public StudentsController(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new StudentCourseViewModel();
            viewModel.Courses = await _context.Courses.ToListAsync();
            ViewBag.Students = await _context.Students.ToListAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndUpdate(int? id, StudentCourseViewModel studentCourseView)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == studentCourseView.CourseId);

            if (course == null)
            {
                return NotFound();
            }

            if (id == 0)
            {
                var newStudent = new Student
                {
                    Name = studentCourseView.Name,
                    Email = studentCourseView.Email,
                    Course = course.Name                
                };

                await _context.AddAsync(newStudent);
                await _context.SaveChangesAsync();

                var studentCourseEnrollment = new StudentCourseEnrollment
                {
                    StudentId = newStudent.Id,
                    CourseId = studentCourseView.CourseId
                };

                await _context.AddAsync(studentCourseEnrollment);
                await _context.SaveChangesAsync();
            }
            else
            {
                var updateStudent = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

                if (updateStudent == null)
                {
                    return NotFound();
                }

                updateStudent.Name = studentCourseView.Name;
                updateStudent.Email = studentCourseView.Email;
                updateStudent.Course = course.Name;

                _context.Update(updateStudent);
                await _context.SaveChangesAsync();

                var updateStudentCourseEnrollment = await _context.StudentCourseEnrollments.FirstOrDefaultAsync(x => x.StudentId == updateStudent.Id);

                updateStudentCourseEnrollment.StudentId = updateStudent.Id;
                updateStudentCourseEnrollment.CourseId = studentCourseView.CourseId;

                _context.Update(updateStudentCourseEnrollment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Remove(student);
            await _context.SaveChangesAsync();

            var studentEnrollment = await _context.StudentCourseEnrollments.FirstOrDefaultAsync(x => x.StudentId == student.Id);

            if (studentEnrollment == null)
            {
                return NotFound();
            }

            _context.Remove(studentEnrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
