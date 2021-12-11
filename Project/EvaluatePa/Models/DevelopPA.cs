
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EvaluatePa.Models
{
    public class DevelopPA 
    {

        // DateTime date = DateTime.Now.Date;
        [Key]
        public int IdPA { get; set; }
        

        public  string Name { get; set; }
      
        public string Position { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Cdate { get; set; }

        public string Place { get; set; }

        public string BelongTo { get; set; }

        public decimal GetSalary { get; set; }

        public decimal RateSalary { get; set; }

        public string  TypeClassroom1 {get; set;}

        public string TypeClassroom2 { get; set; }

        public string TypeClassroom3 { get; set; }

        public string TypeClassroom4 { get; set; }

        public string TypeClassroom5 { get; set; }

      /*  public int IdCha { get; set; }
        public Challenges challengess { get; set; }

        public int TdSuject { get; set; }
        public WorkLoadPart2 WorkLoadPart2s { get; set; }

        public int TdWork { get; set; }
        public WorkLoadPart1 WorkLoadPart1s { get; set; }


        public int TdPSupport { get; set; }
       public  WorkLoadPart3 WorkLoadPart3s { get; set; }

        public int IdProfess { get; set; }
        public ProfessionDevelopment ProfessionDevelopments { get; set; }

        public int IdLearn { get; set; }
        public ManageLearn ManageLearns { get; set; }

        public int IdPro { get; set; }
       public  PromoteDevelop PromoteDevelops { get; set; } */
    }
}
