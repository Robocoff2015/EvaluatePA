using System.ComponentModel.DataAnnotations;

namespace EvaluatePa.Models
{
    public class WorkLoadPart1
    {
      [Key]
     public int IdWork { get; set; } 

     public string AmountWorkLoad { get; set; }

    // public int IdPA { get; set; }
      
    }
}
