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
using System.Windows;
using System.IO;

namespace EvaluatePa2.Controllers
{
    public class EvaluatePA2Controller : Controller
    {
        private readonly ILogger<EvaluatePA2Controller> _logger;
        private readonly IConfiguration configuration;
        public EvaluatePA2Controller(IConfiguration config, ILogger<EvaluatePA2Controller> logger)
        {
            this.configuration = config;
            _logger = logger;
        }
        public IActionResult EvaluatePA2_Index()
        {

            return View(getUsereEvInfo());

        }
        //

        public DataTable getUsereEvInfo()
        {



            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Media] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";

            sqlString += " SELECT [Prefix],[UserName],[LastName],[CDate],[Ev_Status] FROM [devpa].[dbo].[PA_User]";

            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            //int c = dt.Rows.Count;

            //if (c > 0)
            //{
            //    for (int i = 0; i <= c - 1; i++)
            //    {


            //    }
            //}

            return dt;


        }
    }

    
}
