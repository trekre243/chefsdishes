using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }

        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Chef")]
        public int ChefId { get; set; }

        public Chef Chef { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}