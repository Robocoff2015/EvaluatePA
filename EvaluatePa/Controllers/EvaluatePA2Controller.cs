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
        string dbName = "";
        string connectionString = "";
        public EvaluatePA2Controller(IConfiguration config, ILogger<EvaluatePA2Controller> logger)
        {
            this.configuration = config;
            _logger = logger;
            dbName = configuration.GetConnectionString("dbSource");
            connectionString = configuration.GetConnectionString("DefaultConnectionString2");
        }
        public IActionResult EvaluatePA2_Index()
        {

            return View(getUsereEvInfo(null));

        }

        public IActionResult PA_Evaluate(string user_Id)
        {
            return View(getUsereEvInfo(user_Id));

            //bObject.PA_Form_Full PA_Form = new bObject.PA_Form_Full();

            //PA_Form.setDbProperty(connectionString, dbName);
            //string sessionUser_ = HttpContext.Session.GetString("userId");
            //PA_Form = PA_Form.getPAForm_detail(sessionUser_, null);
            ////string sessionUser_ = HttpContext.Session.GetString("userId");
            //if (PA_Form.Sbj_Hr == null || PA_Form.Sbj_Hr == "")
            //{ PA_Form.Sbj_Hr = "[{\"Total\":0}]"; }
            //if (PA_Form == null)
            //{ ViewBag.Sbj_Hr = "[{\"Total\":0}]"; }
            //else ViewBag.Sbj_Hr = PA_Form.Sbj_Hr;
            ////
            //if (PA_Form.Sbj_Hr_1 == null || PA_Form.Sbj_Hr_1 == "")
            //{ PA_Form.Sbj_Hr_1 = "[{\"Total\":0}]"; }
            //if (PA_Form == null)
            //{ ViewBag.Sbj_Hr_1 = "[{\"Total\":0}]"; }
            //else ViewBag.Sbj_Hr_1 = PA_Form.Sbj_Hr_1;
            ////
            //if (PA_Form.Sbj_Hr_2 == null || PA_Form.Sbj_Hr_2 == "")
            //{ PA_Form.Sbj_Hr_2 = "[{\"Total\":0}]"; }
            //if (PA_Form == null)
            //{ ViewBag.Sbj_Hr_2 = "[{\"Total\":0}]"; }
            //else ViewBag.Sbj_Hr_2 = PA_Form.Sbj_Hr_2;
            ////
            //if (PA_Form.Sbj_Hr_3 == null || PA_Form.Sbj_Hr_3 == "")
            //{ PA_Form.Sbj_Hr_3 = "[{\"Total\":0}]"; }
            //if (PA_Form == null)
            //{ ViewBag.Sbj_Hr_3 = "[{\"Total\":0}]"; }
            //else ViewBag.Sbj_Hr_3 = PA_Form.Sbj_Hr_3;

            ////"[{\"Total\":10}]";
            //HttpContext.Session.SetString("formId", PA_Form.Form_Id);
            //return View("PA_Evaluate", PA_Form);

            ////return View();

        }
        //

        public DataTable getUsereEvInfo(string user_id)
        {
            


            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Media] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";

            sqlString += " SELECT [Prefix],[UserName],[LastName],[CDate],[Ev_Status],[Id] FROM [devpa].[dbo].[PA_User]";
            //[Id],[Prefix],[UserName],[LastName],[UserPosition],[CDate],[School_Id]
            //,[School],[Phonenumber],[Email],[Password],[memberOf],[salaryLevel],[salaryRate]
            //,[classroomType],[JoinDate],[Status],[Ev_Status],[Ev_Date],[UserRole]
            sqlString = "Select [Id],[Prefix],[UserName],[LastName],[UserPosition],[CDate],[School_Id]";
            sqlString += ",[School],[Phonenumber],[Email],[Password],[memberOf],[salaryLevel],[salaryRate]";
            sqlString += ",[classroomType],[JoinDate],[Status],[Ev_Status],[Ev_Date],[UserRole]";
            sqlString += " FROM [devpa].[dbo].[PA_User]";
            if (user_id != null)
            {
                sqlString += " where id ='" + user_id + "'";
            }

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
