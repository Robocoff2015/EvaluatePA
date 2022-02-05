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
        public IActionResult Admin()
        {

            string session_ = HttpContext.Session.GetString("userName");
            if (session_ == "" || session_ == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public User setHttpContext_session(string user_id)
        {
            User user = getUser_Details(user_id);
            if (user != null)
            {
                HttpContext.Session.SetString("userId", user_id);
                HttpContext.Session.SetString("userName", user.UserName);
                HttpContext.Session.SetString("userLastname", user.LastName);
                HttpContext.Session.SetString("userPrefix", user.Prefix);
                HttpContext.Session.SetString("userFullname", user.Prefix + " " + user.UserName + " " + user.LastName);
                HttpContext.Session.SetString("userStatus", user.Status.ToString());
                HttpContext.Session.SetString("userPosition", user.UserPosition);
                HttpContext.Session.SetString("userRole", user.UserRole);
            }
            return user;

        }
        public IActionResult Main(string user_id)
        {   
            User user = setHttpContext_session(user_id); //Also implement HttpContext.Session.SetString("userId", user_id); 


            if (user.Status == 0 || user.Status == 1)
            {
                return View(user);
            }
            //
            return RedirectToAction("EvaluatePA_Index", "EvaluatePa", new { user = user_id });
        }

        public JsonResult UpdateUser_AJ(String Prefix, String FirstName, String LastName, String position, String school_id, String CDate, String memberOf, String salaryLevel, 
            String salaryRate, String classroomType, String user_Id, String UserRole, String submit)
        {
            if (UserRole == null)
            { UserRole = "user"; }
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);

                sqlString += " UPDATE [" + dbName + "].[dbo].[PA_User] SET [Prefix] = " + Prefix + ",[UserName] = '" + FirstName + "',[LastName] = '" + LastName + "',[UserPosition] = '" + position + "',[CDate] = '" + CDate + "'";
                sqlString += " ,[School] = '" + school_id + "',[memberOf] = '" + memberOf + "'";
                sqlString += " ,[salaryLevel] = " + salaryLevel + ",[salaryRate] = " + salaryRate + ",[classroomType] = '" + classroomType + "',[Status] = 2,[UserRole] = '" + UserRole + "'";
                sqlString += " WHERE Id = '" + user_Id + "'";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();

            }
            return Json("Ok");
        }

        public IActionResult Logout()
        {
            //if (firstName == null || Password == null)
            //    return View();

            //string myPassSHA = GenerateSHA256String(Password);

            //List<User> user_ = getUser(firstName, myPassSHA);

            //if (user_.Count == 1)
            //{
            //    HttpContext.Session.SetString("userId", user_[0].Id.ToString());
            //    HttpContext.Session.SetString("userEmail", user_[0].Email);
            //    HttpContext.Session.SetString("userName", user_[0].Email);
            //    return RedirectToAction("Main", new { user_Id = user_[0].Id });
            //    // return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = user_[0].Id });
            //}

            //if (firstName == "service" && Password == "12345")
            //{
            //    HttpContext.Session.SetString("userName", firstName);
            //    return RedirectToAction("EvaluatePA_Index", "EvaluatePA");
            //}
            HttpContext.Session.SetString("userId", "");
            HttpContext.Session.SetString("userEmail", "");
            HttpContext.Session.SetString("userName", "");
            HttpContext.Session.SetString("userStatus", "0");
            return View("Login");
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
                HttpContext.Session.SetString("userName", user_[0].Email);
                HttpContext.Session.SetString("userRole", user_[0].UserRole);
                return RedirectToAction("Main", new { user_Id = user_[0].Id });
               // return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = user_[0].Id });
            }

            if (firstName == "service" && Password == "12345")
            {
                HttpContext.Session.SetString("userName", firstName);
                return RedirectToAction("EvaluatePA_Index","EvaluatePA");
            }

            if (firstName == "admin@email.com" && Password == "12345")
            {
                HttpContext.Session.SetString("userName", firstName);
                return RedirectToAction("Admin", "Home");
            }

            HttpContext.Session.SetString("userId", "");
            HttpContext.Session.SetString("userEmail", "");
            HttpContext.Session.SetString("userName", "");
            HttpContext.Session.SetString("userStatus", "0");
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
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sqlString += "IF NOT EXISTS (SELECT [Email] FROM [" + dbName + "].[dbo].[PA_User] WHERE [Email] = '" + Email + "')";
                sqlString += " BEGIN INSERT INTO [" + dbName + "].[dbo].[PA_User] ([UserName],[LastName],[UserPosition],[School_Id],[Phonenumber],[Email],[Password],[JoinDate],[Status],[UserRole]) VALUES('" + UserName + "','" + LastName + "','" + UserPosition + "',0,'" + phoneNumber + "','" + Email + "','" + myPassSHA + "','" + DateTime_ + "',1,'na') END";
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


        /// <summary>
        /// Begin common function


        public List<User> getUser(string uname, string pwd)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            sqlString += " SELECT [Id],[UserName],[LastName],[UserPosition],[School_Id],[Phonenumber],[Email],[Password],[Status],[UserRole] FROM [" + dbName + "].[dbo].[PA_User]";
            //sqlString = " SELECT [Event_id],[site_id],[Event_Name],[Event_Code],[DateTime],[Event_Detail_Old_SN],[Event_Detail_SN],[Event_Detail_Current_P],[Event_Detail_Alarm_P_HH],[Event_Detail_Alarm_P_H],[Event_Detail_Alarm_P_L],[Event_Detail_Alarm_P_LL],[Event_Detail_Battery],[Event_Detail],[DateTime_Occured],[DateTime_Acked],[Acked_person],[DateTime_Cleared]"
            //sqlString += " FROM [PTTNGD].[dbo].[PTTNGD_EVENT]";
            sqlString += " where (UserName = '" + uname + "' or Email = '" + uname + "') "
                + "and Password = '" + pwd + "'";
            Console.WriteLine(sqlString);
            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            int c = dt.Rows.Count;
            List<User> user_ = new List<User>();
            if (c > 0)
            {
                for (int i = 0; i <= c - 1; i++)
                {
                    User user = new User();
                    user.Id = dt.Rows[i].ItemArray[0].ToString();
                    user.UserName = dt.Rows[i].ItemArray[1].ToString();
                    user.LastName = dt.Rows[i].ItemArray[2].ToString();
                    user.UserPosition = dt.Rows[i].ItemArray[3].ToString();
                    user.School = dt.Rows[i].ItemArray[4].ToString();
                    user.Phonenumber = dt.Rows[i].ItemArray[5].ToString();
                    user.Email = dt.Rows[i].ItemArray[6].ToString();
                    user.Password = dt.Rows[i].ItemArray[7].ToString();
                    user.Status = Convert.ToInt32(dt.Rows[i].ItemArray[8].ToString());
                    user.UserRole = dt.Rows[i].ItemArray[9].ToString();
                    //user.DateTime_Cleared = dt.Rows[i].ItemArray[8].ToString();
                    //Event_.Factory_LL = Convert.ToDouble(dt.Rows[i].ItemArray[9]);

                    user_.Add(user);
                }
            }
            return user_;


        }

        public User getUser_Details(string user_Id)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            string sqlString = null;
            // sqlString += " SELECT [Id],[UserName],[LastName],[UserPosition],[School_Id],[Phonenumber],[Email],[Password],[Status] FROM [" + dbName + "].[dbo].[PA_User]";
            //sqlString = " SELECT [Id],[UserName],[LastName],[UserPosition],b.Name,[Phonenumber],[Email],[Password],[Status],b.Province,[Prefix],[memberOf],[CDate],[salaryLevel],[salaryRate],[classroomType] "
            //    + "FROM [" + dbName + "].[dbo].[PA_User] a LEFT join [" + dbName + "].[dbo].PA_School b on a.School_Id = b.School_Id";
            sqlString = " SELECT [Id],[UserName],[LastName],[UserPosition],[School],[Phonenumber],[Email],[Password],[Status],b.Province,[Prefix],[memberOf],[CDate],[salaryLevel],[salaryRate],[classroomType],[UserRole] "
                + "FROM [" + dbName + "].[dbo].[PA_User] a LEFT join [" + dbName + "].[dbo].PA_School b on a.School_Id = b.School_Id";
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
                    
                    user.Id = dt.Rows[i].ItemArray[0].ToString();
                    user.UserName = dt.Rows[i].ItemArray[1].ToString();
                    user.LastName = dt.Rows[i].ItemArray[2].ToString();
                    user.UserPosition = dt.Rows[i].ItemArray[3].ToString();
                    user.School = dt.Rows[i].ItemArray[4].ToString();
                    user.Phonenumber = dt.Rows[i].ItemArray[5].ToString();
                    user.Email = dt.Rows[i].ItemArray[6].ToString();
                    user.Password = dt.Rows[i].ItemArray[7].ToString();
                    user.Status = Convert.ToInt32(dt.Rows[i].ItemArray[8].ToString());
                    user.Province = dt.Rows[i].ItemArray[9].ToString();
                    user.Prefix = (dt.Rows[i].ItemArray[10].ToString())=="1"?"นาย":(dt.Rows[i].ItemArray[10].ToString() == "2")?"นาง":"นางสาว";
                    user.memberOf = dt.Rows[i].ItemArray[11].ToString();
                    user.CDate = dt.Rows[i].ItemArray[12].ToString();
                    user.salaryLevel = dt.Rows[i].ItemArray[13].ToString();
                    user.salaryRate = dt.Rows[i].ItemArray[14].ToString();
                    user.classroomType = dt.Rows[i].ItemArray[15].ToString();
                    user.UserRole = dt.Rows[i].ItemArray[16].ToString();
                    //user.DateTime_Cleared = dt.Rows[i].ItemArray[8].ToString();
                    //Event_.Factory_LL = Convert.ToDouble(dt.Rows[i].ItemArray[9]);

                    //user_.Add(user);
                }
                return user;
            }
            return new User();


        }
        /// End common function
        /// </summary>



    }
}
