﻿using System;
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
            HttpContext.Session.SetString("userId", user);
            if (user != null)
            {
                return View(getPAForm(user));
            }
            return View(new List<PA_Form_Short>());
        }

        public IActionResult EvaluatePA_Work_ListAll(string user)
        {
            HttpContext.Session.SetString("userId", user);
            if (user != null)
            {
                return View(getPAForm(user));
            }
            return View(new List<PA_Form_Short>());
        }

        public IActionResult Management_Index(string user_id, string mgSection)
        {
            ViewBag.mgSection = mgSection;  // "School";// mgSection;
            HttpContext.Session.SetString("userId", user_id);
            DateTime dt = DateTime.Now;
            int today_ = (int)dt.DayOfWeek;
            ViewBag.Today = today_;
            if (user_id != null)
            {
                return View(getPAForm(user_id));
            }
            return View(new List<PA_Form_Short>());
        }
        public IActionResult Admin_Index(string user)
        {
            HttpContext.Session.SetString("userId", user);
            if (user != null)
            {
                return View(getPAForm(user));
            }
            return View(new List<PA_Form_Short>());
        }


        public IActionResult PA_Add(string PA_Name, string Id)
        {
            string sessionUser_ = HttpContext.Session.GetString("userId");
            PA_Form_Full PA_Form = getPAForm_detail(sessionUser_, Id);
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
            return View("PA_Add",PA_Form);
        }

        public IActionResult PA_View(string PA_Name, string Id)
        {
            string sessionUser_ = HttpContext.Session.GetString("userId");
            PA_Form_Full PA_Form = getPAForm_detail(sessionUser_, Id);
            //string sessionUser_ = HttpContext.Session.GetString("userId");
            if (PA_Form.Sbj_Hr == null || PA_Form.Sbj_Hr == "")
            { PA_Form.Sbj_Hr = "[{\"Total\":0}]"; }
            if (PA_Form == null)
            { ViewBag.Sbj_Hr = "[{\"Total\":0}]"; }
            else ViewBag.Sbj_Hr = PA_Form.Sbj_Hr;
            //"[{\"Total\":10}]";
            HttpContext.Session.SetString("formId", PA_Form.Form_Id);
            return View("PA_View", PA_Form);
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

        public PA_Form_Full getPAForm_detail(string User_Id,string Form_Id)
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
            sqlString += " where a.Id = CAST('" + Form_Id + "' as uniqueidentifier)  and a.[OwnerId] = " + User_Id + " and a.[Status] <> 204 ORDER BY[DateTime] ASC";
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

        public IActionResult UpdateUserAll_AJ(string formId,
            string Prefix, string FirstName, string LastName, string position
            , string school_id, string CDate, string memberOf, string salaryLevel
            , string Subject_Hour, string Total_Hour_Schedule_str
            , string Subject_1_Hour, string Total_Hour_Learning_Promotion_Support_str
            , string Subject_2_Hour, string Total_Hour_Q_Education_Mng_Devstr
            , string Subject_3_Hour, string Total_Hour_Policy_Focus_Sup_str
            , string salaryRate,string classroomType,string user_Id
            , string LM_Task,string LM_Outcomes,string LM_Indicators
            , string PS_Task,string PS_Outcomes,string PS_Indicators
            , string SP_Dev_Task, string SP_Dev_Dev_Outcomes, string SP_Dev_Dev_Indicators
            , string CL_Point,string CL_Point_Text,string Problem_State
            , string Method_To_Acheivment,string QT_Expect_Result,string QL_Expect_Result)
            {
            
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Total_Hour_Schedule_str = (Total_Hour_Schedule_str == null) ? "0" : Total_Hour_Schedule_str;
                Total_Hour_Learning_Promotion_Support_str = (Total_Hour_Learning_Promotion_Support_str == null) ? "0" : Total_Hour_Learning_Promotion_Support_str;
                Total_Hour_Q_Education_Mng_Devstr = (Total_Hour_Q_Education_Mng_Devstr == null) ? "0" : Total_Hour_Q_Education_Mng_Devstr;
                Total_Hour_Policy_Focus_Sup_str = (Total_Hour_Policy_Focus_Sup_str == null) ? "0" : Total_Hour_Policy_Focus_Sup_str;
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                //sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Name] = '" + PA_Name + "' and [Status] <> 204)";
                //sqlString += " BEGIN INSERT INTO [" + dbName + "].[dbo].[PA_Form]([Id],[Name],[DateTime],[OwnerId],[Status]) VALUES (default,'" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0) END";
                sqlString += " UPDATE [" + dbName + "].[dbo].[PA_Form]";
                sqlString += " SET [MDateTime] = '" + DateTime_ + "', [Total_Hour_Schedule] = " + Total_Hour_Schedule_str + ",[Subject_Hour] = '" + Subject_Hour + "'"; 
                sqlString += " ,[Total_Hour_Learning_Promotion_Support] = " + Total_Hour_Learning_Promotion_Support_str + ",[Subject_1_Hour] = '" + Subject_1_Hour + "'"; 
                sqlString += " ,[Total_Hour_Q_Education_Mng_Dev] = " + Total_Hour_Q_Education_Mng_Devstr + ",[Subject_2_Hour] = '" + Subject_2_Hour + "'"; 
                sqlString += " ,[Total_Hour_Policy_Focus_Sup] = " + Total_Hour_Policy_Focus_Sup_str + ",[Subject_3_Hour] = '" + Subject_3_Hour + "'";
                sqlString += " ,[LM_Task] = '" + LM_Task + "',[LM_Outcomes] = '" + LM_Outcomes + "',[LM_Indicators] = '" + LM_Indicators + "'";
                sqlString += " ,[PS_Task] = '" + PS_Task + "',[PS_Outcomes] = '" + PS_Outcomes + "',[PS_Indicators] = '" + PS_Indicators + "'";
                sqlString += " ,[SP_Dev_Task] = '" + SP_Dev_Task + "',[SP_Dev_Dev_Outcomes] = '" + SP_Dev_Dev_Outcomes + "',[SP_Dev_Dev_Indicators] = '" + SP_Dev_Dev_Indicators + "'";
                sqlString += " ,[CL_Point] = '" + CL_Point + "',[CL_Point_Text] = '" + CL_Point_Text + "',[Problem_State] = '" + Problem_State + "'";
                sqlString += " ,[Method_To_Acheivment] = '" + Method_To_Acheivment + "',[QT_Expect_Result] = '" + QT_Expect_Result + "',[QL_Expect_Result] = '" + QL_Expect_Result + "',[status] = " + "203" + "";
                sqlString += " WHERE [Id] = CAST('" + formId + "' as uniqueidentifier)";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = user_Id });

            //return View();
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
                sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Name] = '" + PA_Name + "' and [Status] <> 204 and OwnerId = '" + user_Id + "')";
                sqlString += " BEGIN INSERT INTO [" + dbName + "].[dbo].[PA_Form]([Id],[Name],[DateTime],[MDateTime],[OwnerId],[Status]) VALUES (default,'" + PA_Name + "','" + DateTime_ + "','" + DateTime_ + "'," + user_Id + ",200) END";
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

        public IActionResult GetApprovedForm_AJ(String PA_Name, String Id)
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
                sqlString += " UPDATE [" + dbName + "].[dbo].[PA_Form] SET [Status] = 205, MDateTime = '" + DateTime_ + "' WHERE [Id] = '" + Id + "'";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
        }


        public IActionResult EditForm_AJ(String PA_Name, String Id)
        {
            if (PA_Name == "" || PA_Name == null)
            {
                return RedirectToAction("EvaluatePA_Index", "EvaluatePA", new { user = HttpContext.Session.GetString("userId") });
            }
            //string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
            ////connection.Open();
            //System.Data.DataTable dt = new DataTable();
            //String sqlString = null;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    //string myPassSHA = GenerateSHA256String(Password);
            //    string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
            //    //sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Name] = '" + PA_Name + "')";
            //    //sqlString += " DELETE FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Id] = '" + Id + "'";
            //    sqlString += " UPDATE [" + dbName + "].[dbo].[PA_Form] SET [Status] = 204, MDateTime = '" + DateTime_ + "' WHERE [Id] = '" + Id + "'";
            //    connection.Open();
            //    SqlCommand command2 = new SqlCommand(sqlString, connection);
            //    command2.ExecuteNonQuery();
            //    command2.Dispose();
            //    connection.Close();
            //}
            return RedirectToAction("PA_Add", "EvaluatePA", new { user = HttpContext.Session.GetString("userId"), Form_id = Id});
        }


    }
}
