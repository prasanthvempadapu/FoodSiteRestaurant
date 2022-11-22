using System.ComponentModel.DataAnnotations;

namespace OwnerApp.Models
{
    public class Order
    {
        
            [Key]
            public int Id { get; set; }
            [Required]

            public string UserName { get; set; }
            [Required]
            public string OrderName { get; set; }
            [Required]

            public string Quantity { get; set; }

            public int? Price { get; set; }
            [Required]
            public string Address { get; set; }

            public string Status { get; set; }
            public Order() { }
            public Order(int id, string username, string orderName, string quantity, int price, string address, string status)
            {
                Id = id;
                UserName = username;
                OrderName = orderName;
                Quantity = quantity;
                Price = price;
                Address = address;
                Status = status;
            }
        
    }
}
