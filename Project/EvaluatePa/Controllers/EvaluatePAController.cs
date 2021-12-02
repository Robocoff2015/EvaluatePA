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

            string connectionString = configuration.GetConnectionString("DefaultConnectionString1_");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],[Status]  FROM [" + dbName + "].[dbo].[PA_Media]";
            sqlString += " where [OwnerId] = '" + uname + "'";
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
                    PA_Form.Add(PA_Form_);
                }
            }
            return PA_Form;


        }

    }
}
