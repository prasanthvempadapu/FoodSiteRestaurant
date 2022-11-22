using System.ComponentModel.DataAnnotations;

namespace OwnerApp.Models
{
    public class Orders
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
        public Orders() { }
        public Orders(int id,string username, string orderName, string quantity,int price, string address)
        {
            Id = id;
            UserName = username;
            OrderName = orderName;
            Quantity = quantity;
            Price = price;
            Address = address;
        }
    }
}
