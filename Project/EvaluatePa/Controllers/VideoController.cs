using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvaluatePa.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EvaluatePa.Controllers
{
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IConfiguration configuration;
        public VideoController(IConfiguration config, ILogger<VideoController> logger)
        {
            this.configuration = config;
            _logger = logger;
        }
        public IActionResult Index(int userId)
        {
            User user_ = getUser_Details(userId.ToString());
            return View();
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
            sqlString = " SELECT [Id],[UserName],[LastName],[UserPosition],[School],[Phonenumber],[Email],[Password],[Status],b.Province,[Prefix],[memberOf],[CDate],[salaryLevel],[salaryRate],[classroomType] "
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
                    user.Prefix = dt.Rows[i].ItemArray[10].ToString();
                    user.memberOf = dt.Rows[i].ItemArray[11].ToString();
                    user.CDate = dt.Rows[i].ItemArray[12].ToString();
                    user.salaryLevel = dt.Rows[i].ItemArray[13].ToString();
                    user.salaryRate = dt.Rows[i].ItemArray[14].ToString();
                    user.classroomType = dt.Rows[i].ItemArray[15].ToString();
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
