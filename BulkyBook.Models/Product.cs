using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [DisplayName("Product Name")]
        public string Title { get; set; }

        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [DisplayName("List Price")]
        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
        public double ListPrice { get; set; }
        [DisplayName("Price")]
        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
        public double Price { get; set; }
        [DisplayName("Price (50)")]
        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
        public double Price50 { get; set; }
        [DisplayName("Price (100)")]
        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
        public double Price100 { get; set; }
        public string? ImageUrl { get; set; }
    }
}
