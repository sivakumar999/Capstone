using BlogTrackerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;




namespace BlogTrackerApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(LoginInfo loginInfo)
        {
            string connection = _configuration.GetConnectionString("Capstone");
            SqlConnection con = new SqlConnection(connection);
            string cmd = "Select EmailId,Password from AdminInfo where EmailId=@Emailid and Password=@Password";
            con.Open();
            SqlCommand command = new SqlCommand(cmd, con);
            command.Parameters.AddWithValue("@EmailId", loginInfo.EmailId);
            command.Parameters.AddWithValue("@Password", loginInfo.Password);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("EmailId", loginInfo.EmailId.ToString());

                return RedirectToAction("EmpList", "Admin");
            }

            else
            {
                ViewData["Message"] = "Admin Login Details Failed";
            }
            con.Close();
            return View();
        }
        public ActionResult Employee()
        {
            LoginInfo loginInfo = new LoginInfo(); // Create a new instance of LoginInfo
            return View(loginInfo); // Pass it to the view
        }

        [HttpPost]
        public ActionResult Employee(LoginInfo loginInfo)
        {
            string connection = _configuration.GetConnectionString("Capstone");

            SqlConnection con = new SqlConnection(connection);
            string cmd = "Select EmailId, PassCode from EmpInfo where EmailId=@Emailid and PassCode=@Password"; // Use PassCode column from EmpInfo table
            con.Open();
            SqlCommand command = new SqlCommand(cmd, con);
            command.Parameters.AddWithValue("@EmailId", loginInfo.EmailId);
            command.Parameters.AddWithValue("@Password", loginInfo.Password); // Use Password property
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("EmailId", loginInfo.EmailId.ToString());

                return RedirectToAction("Index", "Blog"); // Redirect to EmpList action in Admin controller // Redirect to the employee dashboard or the desired page
            }
            else
            {
                ViewData["Message"] = "Employee Login Details Failed";
            }
            con.Close();
            return View();
        }

        public ActionResult Logout()
        {
           // FormsAuthentication.SignOut();
            HttpContext.Session.Clear(); // Clear the session to log out the user
            return RedirectToAction("Index", "Home"); // Redirect to the home page or another appropriate page
        }

    }
}
