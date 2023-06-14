using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UKParliament.CodeTest.Data.Models;
using UKParliament.CodeTest.Services.Interfaces.InMemory;

namespace UKParliament.CodeTest.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private List<Person> _people;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _personService.GetAllPersons();
            return View(lst);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Person _person)
        {
            if (ModelState.IsValid)
            {
                if (_person.Id > 0)
                {
                   await _personService.UpdatePersonAsync(_person);

                }
                else
                {
                    _people = await _personService.AddPersonyAsync(_person);
                }
            }

            return RedirectToAction("Index", _people);
        }


    }
}
