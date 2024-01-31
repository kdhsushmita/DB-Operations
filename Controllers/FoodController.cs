using Microsoft.AspNetCore.Mvc;
using online.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace online.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult PlaceAnOrder()
        {
            return View();
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
        public IActionResult PeopleChoice()
        {
            List<PlaceAnOrderModel> peoples = new List<PlaceAnOrderModel>();
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Online;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string command = "SELECT * FROM PeopleChoice";
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PlaceAnOrderModel people = new PlaceAnOrderModel();
                people.CustomerName = reader["CustomerName"].ToString();
                people.Restaurant = reader["Restaurant"].ToString();
                people.Food = reader["Food"].ToString();
                people.Instruction = reader["Instruction"].ToString();
                peoples.Add(people);
            }
            reader.Close();
            conn.Close();
            return View(peoples);


        }
        [Authorize]
        [HttpGet]
        public IActionResult OrderEditFood(string Restaurant)
        {
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Online;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            string command = "SELECT * FROM PeopleChoice WHERE Restaurant= '" + Restaurant + "'";
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            PlaceAnOrderModel people= new PlaceAnOrderModel();
            while (dr.Read())
            {
                people.Food = Convert.ToString(dr["Food"]);
                people.Instruction = Convert.ToString(dr["Instruction"]);
            }
            conn.Close();
            return View(people);
        }

    }
}
