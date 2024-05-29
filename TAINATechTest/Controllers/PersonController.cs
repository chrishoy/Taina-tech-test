using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using log4net;
using Microsoft.AspNetCore.Mvc;
using TAINATechTest.Data.Models;
using TAINATechTest.Models;
using TAINATechTest.Services;
using TAINATechTest.Services.Helpers;

namespace TAINATechTest.Controllers
{
    public class PersonController : Controller
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            List<Person> people = _personService.GetAllPeople();

            return View(people);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Person person = _personService.GetPersonById(id.Value);
            if (person is null)
            {
                return NotFound();
            }

            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Edit()
        //{
        //    return View();
        //}

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            try
            {
                _personService.Delete(id);
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to delete person with id {id}", ex);
                ModelState.AddModelError("", "Unable to delete. " + ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Gender,EmailAddress,PhoneNumber")] Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!EmailHelper.IsEmailValid(person.EmailAddress))
                    {
                        ModelState.AddModelError("", "Unable to save changes - invalid email address format. ");
                    }
                    else
                    {
                        var newId = _personService.AddOrUpdate(person);
                        if (newId == 0)
                        {
                            ModelState.AddModelError("", "Unable to save changes. ");
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
            }
            return View(person);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person person = _personService.GetPersonById(id.Value);

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,FirstName,LastName,Gender,EmailAddress,PhoneNumber")] Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!EmailHelper.IsEmailValid(person.EmailAddress))
                    {
                        ModelState.AddModelError("", "Unable to save changes - invalid email address format. ");
                    }
                    else
                    {
                        long newId = _personService.AddOrUpdate(person);
                        if (newId == 0)
                        {
                            ModelState.AddModelError("", "Unable to save changes. ");
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
            }
            return View(person);
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
