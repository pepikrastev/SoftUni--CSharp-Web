using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Andreys.Models
{
    public class Product
    {
        public int Id { get; set; }

        [/*MaxLength(20),*/ Required]
        public string Name { get; set; }

        //[MaxLength(10)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public Gender Gender { get; set; }
    }
}
