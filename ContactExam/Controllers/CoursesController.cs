using ContactExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactExam.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ContactDbContext _dbContext;
        public CoursesController(ContactDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Courses = await _dbContext.Courses.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            _dbContext.Add(course);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
