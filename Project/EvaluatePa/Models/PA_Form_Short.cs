using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluatePa.Models
{
    public class PA_Form_Short
    {
        public string Form_Id { get; set; }
        public string Form_Name { get; set; }
        public string Date_Time { get; set; }
        public string position { get; set; }
        public string status { get; set; }

        //


        //
        //public string UserName { get; set; }
        ///public string UserName { get; set; }

    }
    public class PA_Form_Full
    {
        public string Form_Id { get; set; }
        public string Form_Name { get; set; }
        public string Date_Time { get; set; }
        public string position { get; set; }

        //

        public string UserInfo_Id { get; set; }
        public string UserInfo_Prefix { get; set; }
        public string UserInfo_UserName { get; set; }
        public string UserInfo_LastName { get; set; }
        public string UserInfo_UserPosition { get; set; }
        public string UserInfo_CDate { get; set; }     //Creation date
        public string UserInfo_School { get; set; }
        public string UserInfo_Province { get; set; }
        public string UserInfo_Phonenumber { get; set; }
        public string UserInfo_Email { get; set; }
        public string UserInfo_Password { get; set; }
        public string UserInfo_memberOf { get; set; }
        public string UserInfo_salaryLevel { get; set; }
        public string UserInfo_salaryRate { get; set; }
        public string UserInfo_classroomType { get; set; }
        public int Report_Status { get; set; }

        //public string UserName { get; set; }
        ///public string UserName { get; set; }
        ///
        //Agreement >> Work Load
        public string Total_Hour_Schedule_str { get; set; }
        public string Sbj_Hr { get; set; }
        //public List<Subject> Sbj_Hr { get; set; }
        public string Total_Hour_Learning_Promotion_Support_str { get; set; }      //Learning Promotion and Support Workload
        public string Sbj_Hr_1 { get; set; }
        public string Total_Hour_Q_Education_Mng_Dev_str { get; set; }   //Quality of Education Management Developing Workload
        public string Sbj_Hr_2 { get; set; }
        public string Total_Hour_Policy_Focus_Sup_str { get; set; }     //Policy and Focus Supporting Workload
        public string Sbj_Hr_3 { get; set; }
        //Agreement >> Learnin_Manage

        public string LM_Task { get; set; }
        public string LM_Outcomes { get; set; }
        public string LM_Indicators { get; set; }

        //Agreement >> Promotion and Support

        public string PS_Task { get; set; }
        public string PS_Outcomes { get; set; }
        public string PS_Indicators { get; set; }

        //Agreement >> Self and Profession Development

        public string SP_Dev_Task { get; set; }
        public string SP_Dev_Dev_Outcomes { get; set; }
        public string SP_Dev_Dev_Indicators { get; set; }

        //Agreement >> Challenging Point

        public string CL_Point { get; set; }
        public string CL_Point_Text { get; set; }
        public string Problem_State { get; set; }

        public string Method_To_Acheiivment { get; set; }
        public string QT_Expect_Result { get; set; }
        public string QL_Expect_Result { get; set; }



    }

    public class Subject
    {
        public int Sbj_Id { get; set; }
        public string Sbj_Name { get; set; }
        public int Sbj_Hour { get; set; }

    }
}
