using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labolatorium3___app.Models
{
    public class Product
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwe!")]
        [StringLength(maximumLength: 50, ErrorMessage = "Za długa nazwa! Maksymalnie 50 znaków.")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwe!")]
        [StringLength(maximumLength: 50, ErrorMessage = "Za długa nazwa! Maksymalnie 50 znaków.")]
        public string Producer { get; set; }
        public DateTime DateOfProduction { get; set; }

        public string description { get; set; }


    }
}
