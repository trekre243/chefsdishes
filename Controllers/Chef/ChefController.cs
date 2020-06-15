using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ChefsDishes.Models;

namespace ChefsDishes.Controllers
{
    public class ChefController : Controller
    {   
        private MyContext dbContext;

        public ChefController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("new")]
        public ViewResult NewChef()
        {
            return View();
        }

        [HttpPost("chef")]
        public IActionResult Create(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("Entered is valid");
                if(newChef.DOB > DateTime.Now)
                {
                    Console.WriteLine(newChef.DOB);
                    ModelState.AddModelError("DOB", "Birthdate must be in the past");
                    return View("NewChef");
                }
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("NewChef");
            }
            else
            {
                return View("NewChef");
            }
        }

        [HttpGet("")]
        public ViewResult Chefs()
        {
            List<HomeChef> dispChefs = new List<HomeChef>();
            List<Chef> chefs = dbContext.Chefs.ToList();
            foreach(var chef in chefs)
            {
                string name = chef.FirstName + " " + chef.LastName;
                int numDishes = dbContext.Dishes
                    .Include(d => d.Chef)
                    .Where(d => d.ChefId == chef.ChefId)
                    .ToList()
                    .Count();
                int age = new DateTime(DateTime.Now.Subtract(chef.DOB).Ticks).Year - 1; 
                dispChefs.Add(new HomeChef() { Name = name, Age = age, NumDishes = numDishes });
            }

            return View(dispChefs);
        }

    }
}
