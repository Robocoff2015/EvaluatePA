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
using Microsoft.AspNetCore.Http;
using System.IO;

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
        public IActionResult Index(int user)
        {
            User user_ = getUser_Details(user.ToString());
            ViewBag.user_id = user;
            List<MediaInfo> meadiainfo_ = getMediaInfo(user.ToString());
            return View("Index", meadiainfo_);
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

        //[HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string user_id, string detail)
        {
            if (user_id == "0")
            { user_id = ""; }
            String userPath = "";
            if (user_id == "")
            { }
            else
            {
                //user_id = "300001";
                userPath = "\\" + user_id;
                if (!Directory.Exists("wwwroot\\SharedData" + "\\" + user_id))
                {


                    Directory.CreateDirectory("wwwroot\\SharedData" + "\\" + user_id);
                }


            }

            if (file == null || file.Length == 0)
                return Content("file not selected");
            var fileName = file.FileName;
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\SharedData" + userPath,
                        fileName);
            //file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            InsertShareInfo(fileName, user_id, detail);
            List<MediaInfo> MediaInfo_ = getMediaInfo(user_id);
            //string currentDateTime = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
            //ViewBag.DateTime_ = currentDateTime;
            ViewBag.user_id = user_id;
            ////ViewBag.SupplierCode = vendor;
            ////ViewBag.SupplierName = getASNItemMRPTOP1("[Name 1]", " [Vendor] = '" + vendor + "'");     //ASNRelease_[0].SupplierName;
            //ViewBag.Plant = "3302";
            //ViewBag.vendor = user_id;
            //return View("Index", MediaInfo_);
            return View("Index", MediaInfo_);
        }


        private List<MediaInfo> getMediaInfo(string user_id)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string connectionString = "Server = localhost\\SQLEXPRESS; Database = SPHERE; Trusted_Connection = True;";  //configuration.GetConnectionString("DefaultConnectionString1_");
            string condition = " [ExpiredDate] >= '" + currentDateTime + "' AND (user_id = " + user_id + ")";         // AND [Deliv  Date] <= '2021-03-30'";
            string sqlString = "SELECT [MESSAGEID],[Date],[Message],[Information],[Information2],[user_id],[Status],[UpdateDate],[ExpiredDate] FROM [devpa].[dbo].[" + "PA_Media_" + "] WHERE " + condition + " order by information, [Date]";

            #region "Query SQL String Statement"
            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                System.Data.DataTable dt = new DataTable();
                Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
                da.Fill(dt);
                #endregion
                int c = dt.Rows.Count;
                if (c > 0)
                {
                    List<MediaInfo> MediaInfo_ = new List<MediaInfo>();
                    int i = 0;
                    MediaInfo MediaInfo = new MediaInfo();
                    foreach (DataRow dtRow in dt.Rows)
                    {

                        MediaInfo = new MediaInfo();
                        MediaInfo.ID = dtRow.ItemArray[0].ToString();
                        MediaInfo.Date = dtRow.ItemArray[1].ToString();
                        MediaInfo.Message = dtRow.ItemArray[2].ToString();
                        MediaInfo.Information = dtRow.ItemArray[3].ToString();
                        MediaInfo.Information2 = dtRow.ItemArray[4].ToString();
                        MediaInfo.user_id = dtRow.ItemArray[5].ToString();
                        MediaInfo.Status = dtRow.ItemArray[6].ToString();
                        MediaInfo.UpdateDate = dtRow.ItemArray[7].ToString();
                        MediaInfo.ExpiredDate = dtRow.ItemArray[8].ToString();

                        MediaInfo_.Add(MediaInfo);


                        i++;
                    }
                    return MediaInfo_;
                }
                else
                { return null; }
            }
        }


        private int InsertShareInfo(string FileName, String user_id, string detail_)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string ExpiredDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");

            //string connectionString = "Server = localhost\\SQLEXPRESS; Database = SPHERE; Trusted_Connection = True;";  //configuration.GetConnectionString("DefaultConnectionString1_");
            //string condition = "Vendor = '" + vendorId + "' or Vendor = ''";         // AND [Deliv  Date] <= '2021-03-30'";
            string sqlString = "IF EXISTS(select * from [devpa].[dbo].[PA_Media_] where Information = '" + FileName + "' AND user_id = '" + user_id + "')";
            sqlString += " update [devpa].[dbo].[PA_Media_] set ";
            sqlString += " Date = '" + currentDateTime + "', ";
            sqlString += " Information2 = '" + detail_ + "', ";
            sqlString += " UpdateDate = '" + currentDateTime + "', ";
            sqlString += " ExpiredDate = '" + ExpiredDate + "' where Information = '" + FileName + "' AND user_id = '" + user_id + "'";
            sqlString += " ELSE ";


            sqlString += "INSERT INTO [devpa].[dbo].[PA_Media_]([Date],[Message],[Information],[Information2],[user_id],[Status],[UpdateDate],[ExpiredDate]) VALUES ";
            sqlString += "('" + currentDateTime + "','" + "File" + "','" + FileName + "','" + detail_+ "','" + user_id + "','1','" + currentDateTime + "','" + ExpiredDate + "');";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    //string sqlString2 = "INSERT INTO [dbo].[ASN2] ([Serial number],[Vendor],[Character 100],[Deliv  Date],[Invoice],[Ship_Date],[QR_Code],[Alerting],[Act_Qty],[Acc_Qty],[Posted_Date],[Status_],[Updated_Date])  VALUES('" + 
                    //transactions[0].Serial_number + "', '" + transactions[0].Vendor + "', '" + transactions[0].Character_100 + "', '" + transactions[0].Deliv_Date + "', '" + transactions[0].Invoice + "', '" + transactions[0].Ship_Date + "', '', '', " + transactions[0].Acc_Qty + ", 0, '" + posted_date + "','11','1900-01-01 00:00:00')";
                    //InsertHistory("PROCESSED", "ASN PROCESSED BY " + "Warehouse", Invoice_ + "/" + "RECEIVED COMPLETE", asn);
                    return 0;
                }
                catch (Exception ex)
                {
                    string jsonOutPut = System.Text.Json.JsonSerializer.Serialize("DB Update Failed!!..");
                    return -1;
                }

            }
        }


    }


}
