using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvaluatePa.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace EvaluatePa.Controllers
{
    public class EvaluatePAController : Controller
    {
        private readonly ILogger<EvaluatePAController> _logger;
        private readonly IConfiguration configuration;
        public EvaluatePAController(IConfiguration config, ILogger<EvaluatePAController> logger)
        {
            this.configuration = config;
            _logger = logger;
        }
        
       
        public IActionResult EvaluatePA_Index(string user)
        {
            if (user != null)
            {
                return View(getPAForm(user));
            }
            return View(new List<PA_Form_Short>());
        }

        public IActionResult PA_Add(string user)
        {
            string sessionUser_ = HttpContext.Session.GetString("userId");
            return View();
        }

        public List<PA_Form_Short> getPAForm(string uname)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Media] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";

            sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Form] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";
            sqlString += " where a.[OwnerId] = '" + uname + "' and a.[Status] <> 204 ORDER BY [DateTime] ASC";
            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            int c = dt.Rows.Count;
            List<PA_Form_Short> PA_Form = new List<PA_Form_Short>();
            if (c > 0)
            {
                for (int i = 0; i <= c - 1; i++)
                {
                    PA_Form_Short PA_Form_ = new PA_Form_Short();
                    PA_Form_.Form_Name = dt.Rows[i].ItemArray[0].ToString();
                    PA_Form_.Date_Time = dt.Rows[i].ItemArray[4].ToString();
                    PA_Form_.position = dt.Rows[i].ItemArray[6].ToString();
                    PA_Form_.Form_Id = dt.Rows[i].ItemArray[7].ToString();

                    PA_Form.Add(PA_Form_);
                }
            }
            
            return PA_Form;


        }
        public IActionResult NewForm_AJ(String PA_Name, String user_Id)
        {
            if (PA_Name == "" || PA_Name == null)
            {
                return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
            }
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Name] = '" + PA_Name + "' and [Status] <> 204)";
                sqlString += " BEGIN INSERT INTO [" + dbName + "].[dbo].[PA_Form]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0) END";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
        }

        public IActionResult DeleteForm_AJ(String PA_Name, String Id)
        {
            if (PA_Name == "" || PA_Name == null)
            {
                return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
            }
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                //sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Name] = '" + PA_Name + "')";
                //sqlString += " DELETE FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Id] = '" + Id + "'";
                sqlString += " UPDATE [" + dbName + "].[dbo].[PA_Form] SET [Status] = 204, MDateTime = '" + DateTime_ + "' WHERE [Id] = '" + Id + "'";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
        }


    }
}
