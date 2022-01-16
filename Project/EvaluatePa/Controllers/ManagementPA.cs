using EvaluatePa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bObject;

namespace EvaluatePa.Controllers
{
    public class ManagementPA : Controller
    {
        private readonly ILogger<ManagementPA> _logger;
        private readonly IConfiguration configuration;
        List<string> position_ = new List<string>(){"ครูเชี่ยวชาญพิเศษ", "ครูเชี่ยวชาญ", "ครูชำนาญการพิเศษ", "ครูชำนาญการ", "ครู", "ครูผู้ช่วย"};
        List<int> position_count = new List<int>(){ 0,0,0,0,0,0 };

        public ManagementPA(IConfiguration config, ILogger<ManagementPA> logger)
        {
            this.configuration = config;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult Dashboard_Index()
        {
            string userId = HttpContext.Session.GetString("userId");
            return RedirectToAction("Dashboard_Info", "ManagementPA", new { user_Id = userId });
            //return View();
        }
        public IActionResult Dashboard_Info(string user_id)
        {
            HttpContext.Session.SetString("userId", user_id);
            getUsereStat();
            ViewBag.position_count_0 = position_count[0].ToString();
            ViewBag.position_count_1 = position_count[1].ToString();
            ViewBag.position_count_2 = position_count[2].ToString();
            ViewBag.position_count_3 = position_count[3].ToString();
            ViewBag.position_count_4 = position_count[4].ToString();
            ViewBag.position_count_5 = position_count[5].ToString();

            ViewBag.position_count = "[" + position_count[0].ToString() + "," + position_count[1].ToString() + "," + position_count[2].ToString() + ","
                + position_count[3].ToString() + "," + position_count[4].ToString() + "," + position_count[5].ToString() + "," + "]";
            return View();
        }
        public IActionResult Dashboard_Evaluate(string user_id)
        {
            
            HttpContext.Session.SetString("userId", user_id);
            if (user_id != null)
            {
                return View(getPAForm_ALL(""));
            }
            return View(new List<PA_Form_Short>());

            
        }
        public IActionResult Dashboard_Result(string user_id)
        {
            
            HttpContext.Session.SetString("userId", user_id);
            

            if (user_id != null)
            {
                return View(getPAForm_ALL("result"));
            }
            return View(new List<PA_Form_Short>());
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

            sqlString += " SELECT [Name],a.[Description],[Subject],[Type],a.[MDateTime],a.[Status],b.[UserPosition],a.[Id],c.[description]  FROM [" + dbName + "].[dbo].[PA_Form] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id left join [" + dbName + "].[dbo].[PA_Submission_Status] as c on a.status = c.Id";
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
                    PA_Form_.status = dt.Rows[i].ItemArray[8].ToString();

                    PA_Form.Add(PA_Form_);
                }
            }

            return PA_Form;


        }
        public List<PA_Form_Short> getPAForm_ALL(string result)
        {
            string queryExtend = "";
            if (result == "")
            {
                queryExtend = " where  a.[Status] = 205 ORDER BY [DateTime] ASC";
            }

            if (result != "")
            {
                queryExtend = " where  a.[AP_Status] = 100 ORDER BY [DateTime] ASC";
            }


            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Media] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";

            sqlString += " SELECT [Name],a.[Description],[Subject],[Type],a.[MDateTime],a.[Status],b.[UserPosition],a.[Id],c.[description]  FROM [" + dbName + "].[dbo].[PA_Form] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id left join [" + dbName + "].[dbo].[PA_Submission_Status] as c on a.status = c.Id";
            sqlString += queryExtend; // " where  a.[Status] = 205 ORDER BY [DateTime] ASC";
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
                    PA_Form_.status = dt.Rows[i].ItemArray[8].ToString();

                    PA_Form.Add(PA_Form_);
                }
            }

            return PA_Form;


        }


        public void getUsereStat()
        {
            


            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Media] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";

            sqlString += " SELECT a.UserPosition,count(a.UserPosition) FROM [devpa].[dbo].[PA_User] as a GROUP BY UserPosition";
            
            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            int c = dt.Rows.Count;
           
            if (c > 0)
            {
                for (int i = 0; i <= c - 1; i++)
                {
                    for (int j = 0; j <= position_.Count - 1; j++)
                    {
                        if (dt.Rows[i].ItemArray[0].ToString() == position_[j])
                        {
                            position_count[j] = Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString());
                        }


                    }


                }
            }

            


        }
        public IActionResult PA_View(string PA_Name, string Id, string display)
        {
            ViewBag.Show = display;
            if (display == "block")
            {
                ViewBag.ReturnPage = "Dashboard_Evaluate";
            }
            if (display == "none")
            {
                ViewBag.ReturnPage = "Dashboard_Result";
            }

            //string sessionUser_ = HttpContext.Session.GetString("userId");
            PA_Form_Full PA_Form = getPAForm_detail(Id);
            //string sessionUser_ = HttpContext.Session.GetString("userId");
            //string sessionUser_ = HttpContext.Session.GetString("userId");
            if (PA_Form.Sbj_Hr == null || PA_Form.Sbj_Hr == "")
            { PA_Form.Sbj_Hr = "[{\"Total\":0}]"; }
            if (PA_Form == null)
            { ViewBag.Sbj_Hr = "[{\"Total\":0}]"; }
            else ViewBag.Sbj_Hr = PA_Form.Sbj_Hr;
            //
            if (PA_Form.Sbj_Hr_1 == null || PA_Form.Sbj_Hr_1 == "")
            { PA_Form.Sbj_Hr_1 = "[{\"Total\":0}]"; }
            if (PA_Form == null)
            { ViewBag.Sbj_Hr_1 = "[{\"Total\":0}]"; }
            else ViewBag.Sbj_Hr_1 = PA_Form.Sbj_Hr_1;
            //
            if (PA_Form.Sbj_Hr_2 == null || PA_Form.Sbj_Hr_2 == "")
            { PA_Form.Sbj_Hr_2 = "[{\"Total\":0}]"; }
            if (PA_Form == null)
            { ViewBag.Sbj_Hr_2 = "[{\"Total\":0}]"; }
            else ViewBag.Sbj_Hr_2 = PA_Form.Sbj_Hr_2;
            //
            if (PA_Form.Sbj_Hr_3 == null || PA_Form.Sbj_Hr_3 == "")
            { PA_Form.Sbj_Hr_3 = "[{\"Total\":0}]"; }
            if (PA_Form == null)
            { ViewBag.Sbj_Hr_3 = "[{\"Total\":0}]"; }
            else ViewBag.Sbj_Hr_3 = PA_Form.Sbj_Hr_3;
            //"[{\"Total\":10}]";
            HttpContext.Session.SetString("formId", PA_Form.Form_Id);
            return View("PA_View", PA_Form);
        }
        public PA_Form_Full getPAForm_detail(string Form_Id)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Media] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";
            sqlString += " SELECT a.[Id],[Name],[Description],[Subject],[Type],[DateTime],[MDateTime]"
                       + " ,[OwnerId],a.[Status],a.[ClassroomType],[Total_Hour_Schedule],[Total_Hour_Learning_Promotion_Support]"
                       + " ,[Total_Hour_Q_Education_Mng_Dev],[Total_Hour_Policy_Focus_Sup],[LM_Task]"
                       + " ,[LM_Outcomes],[LM_Indicators],[PS_Task],[PS_Outcomes],[PS_Indicators],[SP_Dev_Task]"
                       + " ,[SP_Dev_Dev_Outcomes],[SP_Dev_Dev_Indicators],[CL_Point],[CL_Point_Text]"
                       + " ,[Problem_State],[Method_To_Acheivment],[QT_Expect_Result],[QL_Expect_Result]"
                       + " ,b.[Prefix],b.[UserName],b.[LastName],b.[UserPosition],b.[CDate] ,b.[School_Id]"
                       + " ,b.[School],b.[Phonenumber],b.[Email],b.[Password] ,b.[memberOf],b.[salaryLevel]"
                       + " ,b.[salaryRate],b.[classroomType],b.[JoinDate],b.[Status],a.[Subject_Hour],a.[Subject_1_Hour],a.[Subject_2_Hour],a.[Subject_3_Hour]";
            sqlString += " FROM [" + dbName + "].[dbo].[PA_Form] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";
            sqlString += " where a.Id = CAST('" + Form_Id + "' as uniqueidentifier) and a.[Status] <> 204 ORDER BY[DateTime] ASC";
            //+ " [Problem_State],[Method_To_Acheivment],[QT_Expect_Result],[QL_Expect_Result]  FROM [" + dbName + "].[dbo].[PA_Form_] as a right join[" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";
            //sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],a.[Status],b.[UserPosition],a.[Id]  FROM [" + dbName + "].[dbo].[PA_Form] as a right join [" + dbName + "].[dbo].[PA_User] as b on a.OwnerId = b.Id";
            //sqlString += " where a.Id = " + Form_Id + " and a.[OwnerId] = " + User_Id + " and a.[Status] <> 204 ORDER BY [DateTime] ASC";
            //Console.WriteLine(sqlString);

            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string text = "First line" + Environment.NewLine;
            // Write the text to a new file named "WriteFile.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "sqlString.txt"), true))
            {
                outputFile.WriteLine(sqlString);
            }


            Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
            da.Fill(dt);
            int c = dt.Rows.Count;
            PA_Form_Full PA_Form_ = new PA_Form_Full();
            if (c > 0)
            {
                for (int i = 0; i <= c - 1; i++)
                {
                    //Id	   Name	Description	Subject	Type	DateTime	MDateTime	OwnerId	Status	ClassroomType	
                    //Total_Hour_Schedule	Total_Hour_Learning_Promotion_Support	Total_Hour_Q_Education_Mng_Dev	
                    //Total_Hour_Policy_Focus_Sup	LM_Task	LM_Outcomes	LM_Indicators	
                    //PS_Task	PS_Outcomes	PS_Indicators	SP_Dev_Task	SP_Dev_Dev_Outcomes	SP_Dev_Dev_Indicators	
                    //CL_Point	CL_Point_Text	Problem_State	Method_To_Acheivment	QT_Expect_Result	QL_Expect_Result
                    //0F07E6C1 - CD19 - 4A57 - BD18 - 1088C17A05C5 Develope PA NULL    NULL NULL    2021 - 12 - 07 14:37:29.000 NULL    1000001 0   NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL    NULL NULL
                    //PA_Form_Short PA_Form_ = new PA_Form_Short();
                    PA_Form_.Form_Id = dt.Rows[i].ItemArray[0].ToString();
                    PA_Form_.Form_Name = dt.Rows[i].ItemArray[1].ToString();
                    PA_Form_.Date_Time = dt.Rows[i].ItemArray[5].ToString();
                    PA_Form_.UserInfo_Id = dt.Rows[i].ItemArray[7].ToString();
                    PA_Form_.UserInfo_classroomType = dt.Rows[i].ItemArray[9].ToString();
                    PA_Form_.Total_Hour_Schedule_str = dt.Rows[i].ItemArray[10].ToString();
                    PA_Form_.Total_Hour_Learning_Promotion_Support_str = dt.Rows[i].ItemArray[11].ToString();
                    PA_Form_.Total_Hour_Q_Education_Mng_Dev_str = dt.Rows[i].ItemArray[12].ToString();
                    PA_Form_.Total_Hour_Policy_Focus_Sup_str = dt.Rows[i].ItemArray[13].ToString();
                    PA_Form_.LM_Task = dt.Rows[i].ItemArray[14].ToString();
                    PA_Form_.LM_Outcomes = dt.Rows[i].ItemArray[15].ToString();
                    PA_Form_.LM_Indicators = dt.Rows[i].ItemArray[16].ToString();
                    PA_Form_.PS_Task = dt.Rows[i].ItemArray[17].ToString();
                    PA_Form_.PS_Outcomes = dt.Rows[i].ItemArray[18].ToString();
                    PA_Form_.PS_Indicators = dt.Rows[i].ItemArray[19].ToString();
                    PA_Form_.SP_Dev_Task = dt.Rows[i].ItemArray[20].ToString();
                    PA_Form_.SP_Dev_Dev_Outcomes = dt.Rows[i].ItemArray[21].ToString();
                    PA_Form_.SP_Dev_Dev_Indicators = dt.Rows[i].ItemArray[22].ToString();
                    PA_Form_.CL_Point = dt.Rows[i].ItemArray[23].ToString();
                    PA_Form_.CL_Point_Text = dt.Rows[i].ItemArray[24].ToString();
                    PA_Form_.Problem_State = dt.Rows[i].ItemArray[25].ToString();
                    PA_Form_.Method_To_Acheiivment = dt.Rows[i].ItemArray[26].ToString();
                    PA_Form_.QT_Expect_Result = dt.Rows[i].ItemArray[27].ToString();
                    PA_Form_.QL_Expect_Result = dt.Rows[i].ItemArray[28].ToString();

                    PA_Form_.UserInfo_Prefix = dt.Rows[i].ItemArray[29].ToString();
                    PA_Form_.UserInfo_UserName = dt.Rows[i].ItemArray[30].ToString();
                    PA_Form_.UserInfo_LastName = dt.Rows[i].ItemArray[31].ToString();
                    PA_Form_.UserInfo_UserPosition = dt.Rows[i].ItemArray[32].ToString();
                    PA_Form_.UserInfo_CDate = dt.Rows[i].ItemArray[33].ToString();
                    //PA_Form_.UserInfo_School = dt.Rows[i].ItemArray[34].ToString();
                    PA_Form_.UserInfo_School = dt.Rows[i].ItemArray[35].ToString();
                    PA_Form_.UserInfo_Phonenumber = dt.Rows[i].ItemArray[36].ToString();
                    PA_Form_.UserInfo_Email = dt.Rows[i].ItemArray[37].ToString();
                    //PA_Form_.UserInfo_Password = dt.Rows[i].ItemArray[38].ToString();
                    PA_Form_.UserInfo_memberOf = dt.Rows[i].ItemArray[39].ToString();
                    PA_Form_.UserInfo_salaryLevel = dt.Rows[i].ItemArray[40].ToString();
                    PA_Form_.UserInfo_salaryRate = dt.Rows[i].ItemArray[41].ToString();
                    PA_Form_.UserInfo_classroomType = dt.Rows[i].ItemArray[42].ToString();
                    PA_Form_.Sbj_Hr = dt.Rows[i].ItemArray[45].ToString();
                    PA_Form_.Sbj_Hr_1 = dt.Rows[i].ItemArray[46].ToString();
                    PA_Form_.Sbj_Hr_2 = dt.Rows[i].ItemArray[47].ToString();
                    PA_Form_.Sbj_Hr_3 = dt.Rows[i].ItemArray[48].ToString();
                    //PA_Form_.UserInfo_CDate = dt.Rows[i].ItemArray[29].ToString();
                    //PA_Form_.user = dt.Rows[i].ItemArray[30].ToString();
                    //+" ,b.[Prefix],b.[UserName],b.[LastName],b.[UserPosition],b.[CDate] ,b.[School_Id]"
                    //   + " ,b.[School],b.[Phonenumber],b.[Email],b.[Password] ,b.[memberOf],b.[salaryLevel]"
                    //   + " ,b.[salaryRate],b.[classroomType],b.[JoinDate],b.[Status]";


                    //PA_Form.Add(PA_Form_);
                }
            }

            return PA_Form_;


        }

        public IActionResult SetAppoveForm_AJ(String status, String Id, string comment, string user_Id)
        {
            //if (PA_Name == "" || PA_Name == null)
            //{
            //    return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
            //}
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
                sqlString = " UPDATE [" + dbName + "].[dbo].[PA_Form] SET [Status] = " + status + ",[AP_Status] = " + status + ", APDateTime = '" + DateTime_ + "' WHERE [Id] = '" + Id + "'";

                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                if (status != "206")
                {
                    sqlString = "  INSERT INTO [devpa].[dbo].[PA_Form]([Name],[Description],[Subject],[Type],[DateTime]";
                    sqlString += ",[MDateTime],[OwnerId],[Status],[ClassroomType],[Total_Hour_Schedule],[Subject_Hour]";
                    sqlString += ",[Total_Hour_Learning_Promotion_Support],[Subject_1_Hour],[Total_Hour_Q_Education_Mng_Dev]";
                    sqlString += ",[Subject_2_Hour],[Total_Hour_Policy_Focus_Sup],[Subject_3_Hour],[LM_Task]";
                    sqlString += ",[LM_Outcomes],[LM_Indicators],[PS_Task],[PS_Outcomes],[PS_Indicators],[SP_Dev_Task]";
                    sqlString += ",[SP_Dev_Dev_Outcomes],[SP_Dev_Dev_Indicators],[CL_Point],[CL_Point_Text],[Problem_State]";
                    sqlString += ",[Method_To_Acheivment],[QT_Expect_Result],[QL_Expect_Result],[AP_Status],[AP_Comment]";
                    sqlString += ",[APDateTime],[AP_userId],[EV_Status],[EV_Comment],[EVDateTime],[EV_userId],[Org_Id])";
                    string status_ = "100";
                    sqlString += " SELECT [Name],[Description],[Subject],[Type],[DateTime],[MDateTime],[OwnerId]," + status_ + ",[ClassroomType]";
                    sqlString += ",[Total_Hour_Schedule],[Subject_Hour],[Total_Hour_Learning_Promotion_Support],[Subject_1_Hour]";
                    sqlString += ",[Total_Hour_Q_Education_Mng_Dev],[Subject_2_Hour],[Total_Hour_Policy_Focus_Sup],[Subject_3_Hour]";
                    sqlString += ",[LM_Task],[LM_Outcomes],[LM_Indicators],[PS_Task],[PS_Outcomes],[PS_Indicators],[SP_Dev_Task]";
                    sqlString += ",[SP_Dev_Dev_Outcomes],[SP_Dev_Dev_Indicators],[CL_Point],[CL_Point_Text],[Problem_State]";
                    sqlString += ",[Method_To_Acheivment],[QT_Expect_Result],[QL_Expect_Result]";
                    sqlString += "," + status + ",'" + comment + "',GETDATE()," + user_Id + "";
                    sqlString += ",Null,Null,Null,Null,id";
                    sqlString += " FROM [devpa].[dbo].[PA_Form]";
                    sqlString += " where id = CAST('" + Id + "' as uniqueidentifier);";
                    SqlCommand command3 = new SqlCommand(sqlString, connection);
                    command3.ExecuteNonQuery();
                    command3.Dispose();


                }
                connection.Close();
            }
            return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
        }



    }
}
