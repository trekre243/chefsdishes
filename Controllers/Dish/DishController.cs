using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using ChefsDishes.Models;

namespace ChefsDishes.Controllers
{
    public class DishController : Controller
    {   
        private MyContext dbContext;

        public DishController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dishes/new")]
        public ViewResult NewDish()
        {
            ViewBag.AllChefs = dbContext.Chefs.ToList();
            return View();
        }

        [HttpPost("dish")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("NewDish");
            }
            else
            {
                return View("NewDish");
            }
        }

        [HttpGet("dishes")]
        public ViewResult Dishes()
        {
            List<Dish> dishes = dbContext.Dishes
                .Include(d => d.Chef)
                .ToList();
            return View(dishes);
        }
    }
}
