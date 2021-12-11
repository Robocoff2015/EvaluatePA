using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluatePa.Models
{
    public class ManageLearn
    {
      //  [Key]
        public int IdLearn { get; set; }

       public string Task { get; set; }

        public string Outcome { get; set; }

        public string Indicator { get; set; }

      //  public ICollection<DevelopPA> DevelopPAs { get; set; }
        
    }
}
