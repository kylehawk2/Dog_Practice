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
        private DogFactory factory;
        public HomeController()
        {
            factory = new DogFactory();
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.dogs = DbConnector.Query("SELECT * FROM dogs");
            return View();
        }

        [HttpGet("{dogId}")]
        public IActionResult Show(int dogId)
        {
            return View(factory.GetDogById(dogId));
        }

        [HttpPost("create")]
        public IActionResult Create(Dog dog, string Name, string Breed, string Weight) 
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
                string sql = $"INSERT INTO dogs(Name, Breed, Weight, CreatedAt, UpdatedAt) VALUES ('{Name}', '{Breed}', '{Weight}', NOW(), NOW())";
                DbConnector.Execute(sql);
                return RedirectToAction("Index");
            }
            
            return View("Index");
        }
    }
}
