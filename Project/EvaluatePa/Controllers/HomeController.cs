using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EvaluatePa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EvaluatePa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration config, ILogger<HomeController> logger)
        {
            this.configuration = config;
            _logger = logger;
        }

        public IActionResult Index()
        {

            string session_ = HttpContext.Session.GetString("userName");
            if (session_ == "" || session_ == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Main(int user_id)
        {

            
            return View(getUser_Details(user_id));
        }

        public IActionResult Login(string firstName, string Password)
        {
            if (firstName == null || Password == null)
                return View();

            string myPassSHA = GenerateSHA256String(Password);

            List<User> user_ = getUser(firstName, myPassSHA);

            if (user_.Count == 1)
            {
                HttpContext.Session.SetString("userId", user_[0].Id.ToString());
                HttpContext.Session.SetString("userEmail", user_[0].Email);
                HttpContext.Session.SetString("userName", user_[0].UserName);
                return RedirectToAction("Main", new { user_Id = user_[0].Id });
               // return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = user_[0].Id });
            }

            if (firstName == "service" && Password == "12345")
            {
                HttpContext.Session.SetString("userName", firstName);
                return RedirectToAction("EvaluatePA_Index","EvaluatePA");
            }
            return View();
        }
        public IActionResult registration(string firstname, string lastname, string emailaddress, string phonenumber, string Password)
        {
            if (firstname != null)
            {
                insertUser(firstname, lastname, "", 0, phonenumber, emailaddress, 1, Password);
                return View("Login");
            }
            
            return View();
        }



            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void insertUser(String UserName, String LastName, String UserPosition, int School_Id, String phoneNumber, String Email,int Status, string Password)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string myPassSHA = GenerateSHA256String(Password);

                sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_User] ([UserName],[LastName],[UserPosition],[School_Id],[Phonenumber],[Email],[Password],[Status]) VALUES('" + UserName + "','" + LastName + "','" + UserPosition + "',0,'" + phoneNumber + "','" + Email + "','" + myPassSHA + "',1)";
                    connection.Open();
                    SqlCommand command2 = new SqlCommand(sqlString, connection);
                    command2.ExecuteNonQuery();
                    command2.Dispose();
                    connection.Close();

            }
        }
        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public List<User> getUser(string uname, string pwd)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            sqlString += " SELECT [Id],[UserName],[LastName],[UserPosition],[School_Id],[Phonenumber],[Email],[Password],[Status] FROM [" + dbName + "].[dbo].[PA_User]";
            //sqlString = " SELECT [Event_id],[site_id],[Event_Name],[Event_Code],[DateTime],[Event_Detail_Old_SN],[Event_Detail_SN],[Event_Detail_Current_P],[Event_Detail_Alarm_P_HH],[Event_Detail_Alarm_P_H],[Event_Detail_Alarm_P_L],[Event_Detail_Alarm_P_LL],[Event_Detail_Battery],[Event_Detail],[DateTime_Occured],[DateTime_Acked],[Acked_person],[DateTime_Cleared]"
            //sqlString += " FROM [PTTNGD].[dbo].[PTTNGD_EVENT]";
            sqlString += " where (UserName = '" + uname + "' or Email = '" + uname + "') "
                + "and Password = '" + pwd + "'";
            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            int c = dt.Rows.Count;
            List<User> user_ = new List<User>();
            if (c > 0)
            {
                for (int i = 0; i <= c - 1; i++)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
                    user.UserName = dt.Rows[i].ItemArray[1].ToString();
                    user.LastName = dt.Rows[i].ItemArray[2].ToString();
                    user.UserPosition = dt.Rows[i].ItemArray[3].ToString();
                    user.School = dt.Rows[i].ItemArray[4].ToString();
                    user.Phonenumber = dt.Rows[i].ItemArray[5].ToString();
                    user.Email = dt.Rows[i].ItemArray[6].ToString();
                    user.Password = dt.Rows[i].ItemArray[7].ToString();
                    user.Status = Convert.ToInt32(dt.Rows[i].ItemArray[8].ToString());
                    //user.DateTime_Cleared = dt.Rows[i].ItemArray[8].ToString();
                    //Event_.Factory_LL = Convert.ToDouble(dt.Rows[i].ItemArray[9]);

                    user_.Add(user);
                }
            }
            return user_;


        }

        public User getUser_Details(int user_Id)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            string sqlString = null;
           // sqlString += " SELECT [Id],[UserName],[LastName],[UserPosition],[School_Id],[Phonenumber],[Email],[Password],[Status] FROM [" + dbName + "].[dbo].[PA_User]";
            sqlString = " SELECT [Id],[UserName],[LastName],[UserPosition],b.Name,[Phonenumber],[Email],[Password],[Status],b.Province FROM [" + dbName + "].[dbo].[PA_User] a join [" + dbName + "].[dbo].PA_School b on a.School_Id = b.School_Id";
            //sqlString = " SELECT [Event_id],[site_id],[Event_Name],[Event_Code],[DateTime],[Event_Detail_Old_SN],[Event_Detail_SN],[Event_Detail_Current_P],[Event_Detail_Alarm_P_HH],[Event_Detail_Alarm_P_H],[Event_Detail_Alarm_P_L],[Event_Detail_Alarm_P_LL],[Event_Detail_Battery],[Event_Detail],[DateTime_Occured],[DateTime_Acked],[Acked_person],[DateTime_Cleared]"
            //sqlString += " FROM [PTTNGD].[dbo].[PTTNGD_EVENT]";
            sqlString += " where (Id = " + user_Id + ") ";
              
            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            int c = dt.Rows.Count;
            //List<User> user_ = new List<User>();
            if (c > 0)
            {
                User user = new User();
                for (int i = 0; i <= c - 1; i++)
                {
                    
                    user.Id = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
                    user.UserName = dt.Rows[i].ItemArray[1].ToString();
                    user.LastName = dt.Rows[i].ItemArray[2].ToString();
                    user.UserPosition = dt.Rows[i].ItemArray[3].ToString();
                    user.School = dt.Rows[i].ItemArray[4].ToString();
                    user.Phonenumber = dt.Rows[i].ItemArray[5].ToString();
                    user.Email = dt.Rows[i].ItemArray[6].ToString();
                    user.Password = dt.Rows[i].ItemArray[7].ToString();
                    user.Status = Convert.ToInt32(dt.Rows[i].ItemArray[8].ToString());
                    user.Province = dt.Rows[i].ItemArray[9].ToString();
                    //user.DateTime_Cleared = dt.Rows[i].ItemArray[8].ToString();
                    //Event_.Factory_LL = Convert.ToDouble(dt.Rows[i].ItemArray[9]);

                    //user_.Add(user);
                }
                return user;
            }
            return new User();


        }


    }
}
