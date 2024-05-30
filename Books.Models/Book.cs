using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required, Range(1, 100)] //List Price
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ListPrice { get; set; }

        [Required, Range(1, 100)] //Price for 1-50
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required, Range(1, 100)] //Price for 50-100
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price50 { get; set; }

        [Required, Range(1, 100)] //Price for 100+
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId"), ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
