using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using EvaluatePa.Models;

namespace EvaluatePa.Controllers
{
    public class EvaluatePAController : Controller
    {
        public string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EvaluateWork;Trusted_Connection=True";

        public IActionResult EvaluatePA_Index()
        {
            return View();
        }
        public IActionResult PA_Form()
        {
            DevelopPA DevelopPA = new DevelopPA();
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlString = "Select * from EvaluateWork.dbo.DevelopPA";
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            { 
               
                
            }
            
            connection.Close();

           // return View("PA_Form", errorModel);

            return View("PA_Form", DevelopPA);
        }
    }
}
