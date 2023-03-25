using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace server.Models
{
    [Table("Meals")]
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        public int AmountLeft { get; set; }
        [Required]
        [StringLength(250)]
        public string ImageURL { get; set; }
        [Required]
        public string Type { get; set;}

        public ICollection<OrderItem> oderItems { get; set; }
        public ICollection<CartItem> cartItems { get; set; }
    }
}