using Laboratorium_3___App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laboratorium_3___App.Controllers
{
    // dotnet tool install --global dotnet-ef
    // dotnet ef migrations add init
    // dotnet ef database update
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            var contacts = _contactService.FindAll();
            // Zamień listę na słownik, jeśli to jest wymagane przez Twój widok
            var contactDictionary = contacts.ToDictionary(c => c.Id);
            return View(contactDictionary);
        }
        private List<SelectListItem> CreateOrganizationsList()
        {
            return _contactService.FindAllOrganization()
                .Select(e => new SelectListItem()
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                }).ToList();
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> organizations = _contactService.FindAllOrganization()
                .Select(e => new SelectListItem()
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                }).ToList();
            return View(new Contact() { OrganizationsList = organizations });
        }

        [HttpPost]
        public IActionResult Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                _contactService.add(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_contactService.FindByID(id));
        }
        [HttpPost]
        public IActionResult Update(Contact model)
        {
            if (ModelState.IsValid)
            {
                _contactService.Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_contactService.FindByID(id));
        }
        [HttpPost]
        public IActionResult Delete(Contact model)
        {
            _contactService.RemoveByID(model.Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_contactService.FindByID(id));
        }

    }
}
