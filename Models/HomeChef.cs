using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsDishes.Models
{
    public class HomeChef
    {
        public string Name { get; set; }
        public int NumDishes { get; set; }
        public int Age { get; set; }
    }
}