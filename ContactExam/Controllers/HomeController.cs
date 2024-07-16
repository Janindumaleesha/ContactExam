using ContactExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace ContactExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContactDbContext _contactDb;

        public HomeController(ILogger<HomeController> logger, ContactDbContext contactDb)
        {
            _logger = logger;
            _contactDb = contactDb;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ContactViewModel();
            viewModel.countries = await _contactDb.Country.ToListAsync();
            var contacts = await _contactDb.Contact.ToListAsync();
            ViewBag.Contact = contacts;
            return View(viewModel);
 
        }

        /*public async Task<IActionResult> Index()
        {
            return View(await _contactDb.Contacts.ToListAsync());
        }*/

        /*public async Task<IActionResult> CreateEndUpdate()
        {
            var viewModel = new ContactViewModel();
            viewModel.countries = await _contactDb.Country.ToListAsync();
            return View();
        }*/

        [HttpPost]
        public async Task<IActionResult> CreateEndUpdate(int? id, [Bind("ContactID,Name,Address,Tel,Mobile,Email,CountryID")] Contact contact)
        {
            if (id != 0)
            {
                contact.ContactID = 4;
                _contactDb.Update(contact);
                await _contactDb.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                contact.ContactID = 6;
                _contactDb.Add(contact);
                await _contactDb.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var contact = await _contactDb.Contact.FirstOrDefaultAsync(m => m.ContactID == id);

            return View(contact);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _contactDb.Contact.FindAsync(id);
            _contactDb.Contact.Remove(contact);
            _contactDb.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
