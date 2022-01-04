using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluatePa.Models
{
    public class CalendarInfo
    {
        public int Event_Id { get; set; }
         public string Summary { get; set; }
            public string Description { get; set; }
            public string BDate { get; set; }
            public string EDate { get; set; }
            public string Location_Id { get; set; }
            public string RRULE { get; set; }
            public string RR_FREQ { get; set; }
            public string RR_UNTIL { get; set; }
            public string RR_INTERVAL { get; set; }
            public string RR_COUNT { get; set; }
            public string RR_BYDAY { get; set; }
            public string RR_BYWEEKNO { get; set; }
            public string RR_BYMONT { get; set; }
            public string RR_BYMONTHDAY { get; set; }
            public string RR_BYYEARDAY { get; set; }
            public string RR_BYSETPOS { get; set; }
            public string RR_WKST { get; set; }
            public string User_Id { get; set; }

            public string Subject_Id { get; set; }

            public string SubSchool_Id { get; set; }
            public string School_Id { get; set; }
         public string School_Name { get; set; }
         public string Province { get; set; }
         public string Location_Name { get; set; }
         public string Subject_Name { get; set; }
         public string Subject_Level { get; set; }

        public string CDate { get; set; }
        public string OpenDate { get; set; }
        public string CloseDate { get; set; }
        public string DDate { get; set; }
        public int Parent_Event_Id { get; set; }

        public string Event_Status_Name { get; set; }
        public string Event_Type_Name { get; set; }
        public string Event_SubType_Name { get; set; }
        public int Unit_Id { get; set; }
        public string UnitName { get; set; }

        //public CalendarInfo createNew() { 
        
        //}


    }
}
