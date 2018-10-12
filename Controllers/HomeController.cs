using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dog.Models;
using DbConnection;

namespace Dog.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.dogs = DbConnector.Query("SELECT * FROM dogs");
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(Dog dog)
        {
            // Check ModelState for Model-defined validations
            if (ModelState.IsValid)
            {
                // Prevent duplicate Name/Breed combos
                string query = $"SELECT * FROM dogs WHERE Name = '{dog.Name}' AND Breed = '{dog.Breed}';";

                return RedirectToAction("Login");
            }
            return View("Registration");
        }
    }
}
