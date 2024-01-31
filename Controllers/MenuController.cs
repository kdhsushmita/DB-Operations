using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using online.Models;

namespace online.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult burgerItems()
        {
            List<MenuItemModel> burgerItems = new List<MenuItemModel>();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Menu;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandText = "SELECT * FROM burgerItems";
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MenuItemModel burgerItem = new MenuItemModel();
                        burgerItem.Food = Convert.ToString(reader["Food"]);
                        burgerItem.Restaurant = Convert.ToString(reader["Restaurant"]);
                        burgerItem.Price =(int)reader["Price"];
                        burgerItems.Add(burgerItem);
                    }
                }
            }
            return View(burgerItems);
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditBurgerOrder(string Food)
        {
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Menu;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string command = "SELECT * FROM  burgerItems WHERE Food= '" + Food + "'";
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            PlaceAnOrderModel people = new PlaceAnOrderModel();
            while (dr.Read())
            {
                people.Food = Convert.ToString(dr["Food"]);
                people.Restaurant = Convert.ToString(dr["Restaurant"]);
            }
            conn.Close();
            return View(people);
        }
        [HttpPost]
        public IActionResult AddtoDatabase(PlaceAnOrderModel peoples)
        {
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Online;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string command = "Insert into PeopleChoice Values (' " + peoples.CustomerName + " ', ' " + peoples.Restaurant + "','" + peoples.Food + "','" + peoples.Instruction + "')";
            SqlCommand cmd = new SqlCommand(command, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("ProceedToPay");
        }
        public IActionResult ProceedToPay()
        {
            return View();
        }
        [Authorize]
        public IActionResult ChefsFavorite()
        {
            List<MenuItemModel> burgerItems = new List<MenuItemModel>();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodOnline;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandText = "SELECT * FROM ChefsFavourite";
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MenuItemModel burgerItem = new MenuItemModel();
                        burgerItem.Food = Convert.ToString(reader["Food"]);
                        burgerItem.Restaurant = Convert.ToString(reader["Restaurant"]);
                        burgerItem.Chef = Convert.ToString(reader["Chef"]);
                        burgerItem.Price = (int)reader["Price"];
                        burgerItems.Add(burgerItem);
                    }
                }
            }
            return View(burgerItems);
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditChefFavourite(string Food)
        {
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodOnline;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string command = "SELECT * FROM ChefsFavourite WHERE Food= '" + Food + "'";
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            PlaceAnOrderModel people = new PlaceAnOrderModel();
            while (dr.Read())
            {
                people.Food = Convert.ToString(dr["Food"]);
                people.Restaurant = Convert.ToString(dr["Restaurant"]);
            }
            conn.Close();
            return View(people);
        }
    }
}
