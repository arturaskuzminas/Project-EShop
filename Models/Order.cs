using System;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }

        public Order()
        {

        }

        public Order(int orderId, decimal OrderTotalPrice, string userID, string address)
        {
            this.OrderID = OrderID;
            this.OrderTotalPrice = OrderTotalPrice;
            this.UserID = userID;
            this.Date = DateTime.Now;
            this.Status = "Apdorojamas";
            this.Address = address;
        }
    }
}
