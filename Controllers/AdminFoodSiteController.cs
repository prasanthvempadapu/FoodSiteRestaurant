using Microsoft.AspNetCore.Mvc;
using OwnerApp.Models;
using Serilog;
using System.Data.SqlClient;

namespace OwnerApp.Controllers
{
    public class AdminFoodSiteController : Controller
    {
        static List<AdminUser> users = new List<AdminUser>();
        
        public AdminFoodSiteController()
        {
            
            SqlConnection conn = new SqlConnection("Data Source=PSL-FL527L3;Initial Catalog=DEMO;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("select * from adminuser", conn);
            conn.Open();
            SqlDataReader sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                AdminUser user = new AdminUser(sr["UserName"].ToString(), sr["Password"].ToString());
                users.Add(user);
            }
            conn.Close();
            
        }
        public IActionResult Index()
        {
            return View(new AdminUser());
        }
        [HttpPost]
        public IActionResult Index(AdminUser a)
        {
            if(ModelState.IsValid)
            {
                foreach (var i in users)
                {

                    if (i.UserName == a.UserName && i.Password == a.Password)
                    {
                        
                        return RedirectToAction("Display");


                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Display()
        {
            List<Orders> orders = new List<Orders>();
            SqlConnection conn2 = new SqlConnection("Data Source=PSL-FL527L3;Initial Catalog=DEMO;Integrated Security=True;");
            SqlCommand cmd2 = new SqlCommand("select * from orders", conn2);
            conn2.Open();
            SqlDataReader sr2 = cmd2.ExecuteReader();
            while(sr2.Read())
            {
                Orders o = new Orders(Convert.ToInt32(sr2["Id"]),sr2["userName"].ToString(),sr2["OrderName"].ToString(),sr2["Quantity"].ToString(),Convert.ToInt32(sr2["Price"]),sr2["Address"].ToString());
                orders.Add(o);
            }
            conn2.Close();

            return View(orders);
        }
        public IActionResult Delete(int id)
        {
            List<Order> allOrders = new List<Order>();
            SqlConnection conn4 = new SqlConnection("Data Source=PSL-FL527L3;Initial Catalog=DEMO;Integrated Security=True;");
            SqlCommand cmd4 = new SqlCommand(String.Format("select * from orders where Id={0}", id), conn4);
            conn4.Open();
            SqlDataReader sr4 = cmd4.ExecuteReader();
            Order o = new Order();
            while (sr4.Read())
            {
                o = new Order(Convert.ToInt32(sr4["Id"]), sr4["userName"].ToString(), sr4["OrderName"].ToString(), sr4["Quantity"].ToString(), Convert.ToInt32(sr4["Price"]), sr4["Address"].ToString(),"Delivered");
                allOrders.Add(o);
            }
            Log.Debug(String.Format("Order delivered to {0} with order Id: {1}", o.UserName,o.Id));


            
            SqlConnection conn5 = new SqlConnection("Data Source=PSL-FL527L3;Initial Catalog=DEMO;Integrated Security=True;");
 
            SqlCommand cmd5 = new SqlCommand(String.Format("insert into CompletedOrders values({0},'{1}','{2}','{3}',{4},'{5}','{6}')",o.Id,o.UserName,o.OrderName,o.Quantity,o.Price,o.Address,o.Status), conn5);
            conn5.Open();
            cmd5.ExecuteNonQuery();
            conn5.Close();

            SqlConnection conn3 = new SqlConnection("Data Source=PSL-FL527L3;Initial Catalog=DEMO;Integrated Security=True;");
            SqlCommand cmd3 = new SqlCommand(String.Format("delete from orders where Id={0}",id), conn3);
            conn3.Open();
            cmd3.ExecuteNonQuery();
            conn3.Close();
            return RedirectToAction("Display");
                
        }
    }
}
