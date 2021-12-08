using System.ComponentModel.DataAnnotations;

namespace EvaluatePa.Models
{
    public class WorkLoadPart2
    {
        [Key]
       public string IdSubject { get; set; } 

        public string Subject { get; set; }

        public int Hour { get; set; }

      //  public int IdPA { get; set; } 
      
    }
}
