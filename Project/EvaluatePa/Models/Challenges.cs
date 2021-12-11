using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;


namespace EvaluatePa.Models
{
    public class Challenges
    {
        [Key]
        public string IdCha { get; set; }
     
        public string NameChallen { get; set; } 

        public string Operate { get; set; } 

        public string Quantitative { get; set; } 

        public string Qualitative { get; set; } 

     //   public ICollection<DevelopPA> DevelopPAs { get; set; }

    }
}
