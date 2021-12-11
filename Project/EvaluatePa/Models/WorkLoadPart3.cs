using System.ComponentModel.DataAnnotations;

namespace EvaluatePa.Models
{
    public class WorkLoadPart3
    {

     [Key]
     public string IdPSubject { get; set; }

     public int HourSupportSubject { get; set; }

    public int HourQsubject { get; set; }

    public int HourFocus { get; set; }

    //public string IdPA { get; set; }

    }
}
