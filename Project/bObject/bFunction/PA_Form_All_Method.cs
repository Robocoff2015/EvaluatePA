using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace bObject
{
    partial class PA_Form_Full
    {
        public string connectionString = ""; //configuration.GetConnectionString("DefaultConnectionString2");
        public string dbName = "";
        public void setDbProperty(string constring, string dbName_)
        {
            connectionString = constring;
            dbName = dbName_;
        }
        public PA_Form_Full getPAForm_detail(string User_Id, string Form_Id)
        {

            
            // string dbName = configuration.GetConnectionString("dbSource");
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

        public PA_Form_Full getPAForm_detail_Mng(string Form_Id)
        {

            //string connectionString = configuration.GetConnectionString("DefaultConnectionString2");
            //string dbName = configuration.GetConnectionString("dbSource");
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

    }
}
