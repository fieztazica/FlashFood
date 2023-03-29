using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace server.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderAt { get; set; }
        [Required]
        public DateTime PaidAt { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        public double Total_money { get; set; }
        [Required]
        [Range (0,Double.MaxValue)]
        public double Paid { get; set; }
        [Range(0, Double.MaxValue)]
        public double Change { get; set;}
        [Required]
        public string SellerId { get; set; }
        [Required]
        public string UserId { get; set; }
       
        public ApplicationUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderAt = DateTime.Now;
        }      
    }
}