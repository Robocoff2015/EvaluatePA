using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluatePa.Controllers
{
       public class Identity
    {
        //object School;
        //Object Lecturer;
        //object Subject;
        //object School_Room;
        //object School_Class;
        //object Event;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class School : Identity
    {
        public string CreatedDate { get; set; }
        public int OwnerId { get; set; }

    }
    //public class Lecturer : Identity
    //{

    //}
    public class Subject : Identity
    {
        public string SubjectCode { get; set; }
        public int School_Id { get; set; }

    }
    public class School_Class : Identity
    {
        public int School_Id { get; set; }
    }
    public class School_Room : Identity
    {
        public int School_Id { get; set; }
    }
    
}
