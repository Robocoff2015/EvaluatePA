using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EvaluatePa.Controllers
{
    public class CurriculumController : Controller
    {
        private readonly ILogger<CurriculumController> _logger;
        private readonly IConfiguration configuration;
        string dbName = "";
        string connectionString = "";
        public CurriculumController(IConfiguration config, ILogger<CurriculumController> logger)
        {
            this.configuration = config;
            _logger = logger;
            dbName = configuration.GetConnectionString("dbSource");
            connectionString = configuration.GetConnectionString("DefaultConnectionString2");
        }
        public IActionResult Curriculum_Index(string user)
        {
            HttpContext.Session.SetString("userId", user);
            if (user != null)
            {
                return View("Curriculum_Index", getCL_datatable(user));
            }
            return View("Curriculum_Index",  new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());
        }

        public IActionResult View_CL(string id)
        {
            //HttpContext.Session.SetString("userId", user);
            if (id != null)
            {
                return View("Curriculum_View", getCL_datatable(id));
            }
            return View("Curriculum_View", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());
        }
        public IActionResult Edit_CL(string id)
        {
            //HttpContext.Session.SetString("userId", user);
            if (id != null)
            {
                return View("Curriculum_Edit", getCL_datatable(id));
            }
            return View("Curriculum_Edit", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());


        }

        public IActionResult Delete_CL(string id)
        {
            //HttpContext.Session.SetString("userId", user);
            if (id != null)
            {
                return View("Curriculum_View", getCL_datatable(id));
            }
            return View("Curriculum_View", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());


        }

        public System.Data.DataTable getCL_datatable(string Id)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                //sqlString += "SELECT [Id],[Name],[Description],[Learning_Area],[Sub_Learining_Area],[DateTime],[MDateTime],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Curriculum] order by [DateTime]";
                sqlString = "SELECT a.[Id],b.[Name],[Description],[Learning_Area],[Sub_Learining_Area],[DateTime],[MDateTime],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Curriculum] as a left join [" + dbName + "].[dbo].[Cu_Learning_Area] as b on a.Learning_Area = b.Id order by [DateTime]";

                connection.Open();
                Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
                da.Fill(dt);

               //SqlCommand command2 = new SqlCommand(sqlString, connection);
                //command2.ExecuteNonQuery();
                //command2.Dispose();
                connection.Close();
            }

            return dt;
        
        }
        public JsonResult NewCL_AJ(String CL_Id, String user_Id)
        {
            if (CL_Id == "" || CL_Id == null)
            {
                return Json("NOK");
            }
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[Curriculum] WHERE [Learning_Area] = '" + CL_Id + "' and [Status] <> 204 and OwnerId = '" + user_Id + "')";
                sqlString += " BEGIN INSERT INTO [" + dbName + "].[dbo].[Curriculum]([Id],[Name],[Learning_Area],[DateTime],[MDateTime],[OwnerId],[Status]) VALUES (NEWID(),'','" + CL_Id + "','" + DateTime_ + "','" + DateTime_ + "'," + 0 + ",200) END ELSE BEGIN SELECT -1 END";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return Json("OK");
        }



        public IActionResult UnitPlan_Index(string user)
        {
            HttpContext.Session.SetString("userId", user);
            if (user != null)
            {
                string sqlString = "SELECT [Id],[Name],[Description],[Cu_Id],[Subject_Id],[PlanDuration],[DateTime],[MDateTime],[Objective],[LearningStrands]"
                                    + ",[LearningStandards],[Indicators],[WorkLoad],[TeachingActivities],[Evaluate],[OwnerId],[Status]"
                                    + " FROM [devpa].[dbo].[Cu_UnitPlan] where [OwnerId] = " + user;
                return View("UnitPlan_Index", get_datatable(sqlString));
            }
            return View("UnitPlan_Index", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());
        }

        public System.Data.DataTable get_datatable(string sqlString)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            //String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                //sqlString += "SELECT [Id],[Name],[Description],[Learning_Area],[Sub_Learining_Area],[DateTime],[MDateTime],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Curriculum] order by [DateTime]";
               // sqlString = "SELECT a.[Id],b.[Name],[Description],[Learning_Area],[Sub_Learining_Area],[DateTime],[MDateTime],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Curriculum] as a left join [" + dbName + "].[dbo].[Cu_Learning_Area] as b on a.Learning_Area = b.Id order by [DateTime]";

                connection.Open();
                Microsoft.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(sqlString, connectionString);
                da.Fill(dt);

                //SqlCommand command2 = new SqlCommand(sqlString, connection);
                //command2.ExecuteNonQuery();
                //command2.Dispose();
                connection.Close();
            }

            return dt;

        }

        public JsonResult NewUnitPlan_AJ(String UP_Name, String user_Id)
        {
            if (UP_Name == "" || UP_Name == null)
            {
                return Json("NOK");
            }
            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[Cu_UnitPlan] WHERE [Name] = 'การออกเสียง' and [Status] <> 204 and OwnerId = NULL)";
                sqlString += " BEGIN INSERT INTO[" + dbName + "].[dbo].[Cu_UnitPlan]([Id],[Name],[Description],[Cu_Id],[Subject_Id],[PlanDuration],[DateTime],[MDateTime],[Objective],[LearningStrands],[LearningStandards]";
                sqlString += " ,[Indicators],[WorkLoad],[TeachingActivities],[Evaluate],[OwnerId],[Status]) VALUES (NEWID(), '" + UP_Name + "', '', '', 0, 0, GETDATE(), GETDATE(),'', '', '', '', '', '', '', " + user_Id + ", 200)";
                sqlString += " END ELSE BEGIN SELECT -1 END";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return Json("OK");
        }
        public IActionResult UpdateUP_AJ(string id, string Name, string PlanDuration, string Description,
            string Class, string LearningStrands, string LearningStandards,
            string Objective,string Symester, string SchoolYear,
            string Indicators, string TA_WarmUp, string TA_Presentation,
            string TA_Pratice, string TA_Production, string TA_Assessment,
            string TA_WarpUp, string WorkLoad, string ProgressReport,
            string DailyNote, string TeachingActivities, string Evaluate
            )
        {

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string ExpiredDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");

            //string connectionString = "Server = localhost\\SQLEXPRESS; Database = SPHERE; Trusted_Connection = True;";  //configuration.GetConnectionString("DefaultConnectionString1_");
            //string condition = "Vendor = '" + vendorId + "' or Vendor = ''";         // AND [Deliv  Date] <= '2021-03-30'";
            string sqlString = "";
            sqlString += " UPDATE [devpa].[dbo].[Cu_UnitPlan] ";
            sqlString += " SET [Name] = '" + Name + "' ";
            sqlString += " ,[Description] = '" + Description + "' ";
            sqlString += " ,[Cu_Id] = '' ";
            sqlString += " ,[Subject_Id] = 0 ";
            sqlString += " ,[PlanDuration] = " + PlanDuration;
            sqlString += " ,[DateTime] = '" + currentDateTime + "' ";
            sqlString += " ,[MDateTime] = '" + currentDateTime + "' ";
            sqlString += " ,[Objective] = '" + Objective + "' ";
            sqlString += " ,[Class] = '" + Class + "' ";
            sqlString += " ,[Symester] = '" + Symester + "' ";
            sqlString += " ,[SchoolYear] = '" + SchoolYear + "' ";
            sqlString += " ,[LearningStrands] = '" + LearningStrands + "' ";
            sqlString += " ,[LearningStandards] = '" + LearningStandards + "' ";
            sqlString += " ,[Indicators] = '" + Indicators + "' ";
            sqlString += " ,[TA_WarmUp] = '" + TA_WarmUp + "' ";
            sqlString += " ,[TA_Presentation] = '" + TA_Presentation + "' ";
            sqlString += " ,[TA_Pratice] = '" + TA_Pratice + "' ";
            sqlString += " ,[TA_Production] = '" + TA_Production + "' ";
            sqlString += " ,[TA_Assessment] = '" + TA_Assessment + "' ";
            sqlString += " ,[TA_WarpUp] = '" + TA_WarpUp + "' ";
            sqlString += " ,[WorkLoad] = '" + WorkLoad + "' ";
            sqlString += " ,[ProgressReport] = '" + ProgressReport + "' ";
            sqlString += " ,[DailyNote] = '" + DailyNote + "' ";
            sqlString += " ,[TeachingActivities] = '" + TeachingActivities + "' ";
            sqlString += " ,[Evaluate] = '" + Evaluate + "' ";
            //sqlString += " ,[OwnerId] = 10000 ";
            sqlString += " ,[Status] = 201 ";
            sqlString += " WHERE [Id] = CAST('" + id + "' as uniqueidentifier)";

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
                   
                }
                catch (Exception ex)
                {
                    string jsonOutPut = System.Text.Json.JsonSerializer.Serialize("DB Update Failed!!..");
                    
                }

            }

            //HttpContext.Session.SetString("userId", user);
             sqlString = "SELECT [Id],[Name],[Description],[Cu_Id],[Subject_Id],[PlanDuration],[DateTime],[MDateTime]";
             sqlString += ",[Objective],[Class],[Symester],[SchoolYear],[LearningStrands],[LearningStandards],[Indicators],[TA_WarmUp],[TA_Presentation]";
             sqlString += ",[TA_Pratice],[TA_Production],[TA_Assessment],[TA_WarpUp],[WorkLoad],[ProgressReport]";
             sqlString += ",[DailyNote],[TeachingActivities],[Evaluate],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Cu_UnitPlan]";
            sqlString += " where [Id] = CAST('" + id + "' as uniqueidentifier)";
            if (id != null)
            {
                return View("UnitPlan_Edit", get_datatable(sqlString));
            }
            return View("UnitPlan_Edit", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());


        }

        public IActionResult Edit_UP(string id)
        {
            //HttpContext.Session.SetString("userId", user);
            string sqlString = "SELECT [Id],[Name],[Description],[Cu_Id],[Subject_Id],[PlanDuration],[DateTime],[MDateTime]";
            sqlString += ",[Objective],[Class],[Symester],[SchoolYear],[LearningStrands],[LearningStandards],[Indicators],[TA_WarmUp],[TA_Presentation]";
            sqlString += ",[TA_Pratice],[TA_Production],[TA_Assessment],[TA_WarpUp],[WorkLoad],[ProgressReport]";
            sqlString += ",[DailyNote],[TeachingActivities],[Evaluate],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Cu_UnitPlan]";
            sqlString += " where [Id] = CAST('" + id + "' as uniqueidentifier)";
            if (id != null)
            {
                return View("UnitPlan_Edit", get_datatable(sqlString));
            }
            return View("UnitPlan_Edit", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());


        }


        public IActionResult DeleteUP_AJ(string Id)
        {
            //HttpContext.Session.SetString("userId", user);
            string sqlString = "SELECT [Id],[Name],[Description],[Cu_Id],[Subject_Id],[PlanDuration],[DateTime],[MDateTime],[Objective]"
                                + " ,[LearningStrands],[LearningStandards],[Indicators],[WorkLoad],[TeachingActivities],[Evaluate],[OwnerId],[Status] FROM [" + dbName + "].[dbo].[Cu_UnitPlan]"
                                + " where [Id] = CAST('" + Id + "' as uniqueidentifier)";
            if (Id != null)
            {
                //return View("UnitPlan_Edit", get_datatable(sqlString));
            }

            string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
            //connection.Open();
            System.Data.DataTable dt = new DataTable();
            String sqlString2 = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string myPassSHA = GenerateSHA256String(Password);
                string DateTime_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //sqlString += " INSERT INTO [" + dbName + "].[dbo].[PA_Media]([Name],[DateTime],[OwnerId],[Status]) VALUES ('" + PA_Name + "','" + DateTime_ + "'," + user_Id + ",0)";
                //sqlString += "IF NOT EXISTS (SELECT [Name] FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Name] = '" + PA_Name + "')";
                //sqlString += " DELETE FROM [" + dbName + "].[dbo].[PA_Form] WHERE [Id] = '" + Id + "'";
                sqlString2 += " delete from [" + dbName + "].[dbo].[Cu_UnitPlan]  WHERE [Id] = CAST('" + Id + "' as uniqueidentifier)";
                connection.Open();
                SqlCommand command2 = new SqlCommand(sqlString2, connection);
                command2.ExecuteNonQuery();
                command2.Dispose();
                connection.Close();
            }
            return View("UnitPlan_Edit", new System.Data.DataTable());

            //return View("Curriculum_Index", new System.Data.DataTable());


        }






    }
}
