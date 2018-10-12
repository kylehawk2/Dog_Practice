using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dog_Practice.Models;
using DbConnection;

namespace Dog_Practice.Controllers
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
                List<Dictionary<string, object>> result = DbConnector.Query(query);

                if(result.Count > 0)
                {
                    ModelState.AddModelError("Name", "Name/Breed combination exists already!");
                }

                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
