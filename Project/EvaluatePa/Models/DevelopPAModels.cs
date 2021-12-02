using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluatePa.Models
{
    public class DevelopPAModels
    {
       // [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int  IdPA { get; set; }

        //[Required]
        //[StringLength(50)]

        //[DataType(DataType.Text)]

        //[Display(Name ="Name")]

        public string Name { get; set; }

        //[StringLength(50)]
        //[DataType(DataType.Text)]
        //[Display(Name = "Position")]

        [StringLength(50, MinimumLength = 3)]
        public string Position { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public string Cdate{ get; set; }

        // [DataType(DataType.Text)]
        //[Display(Name = "Place")]

        [StringLength(50, MinimumLength = 3)]
        public string Place { get; set; }

       [StringLength(50, MinimumLength = 3)]
        public string BelongTo { get; set; }

        public int GetSalary { get; set; }

     
        public int RateSalary { get; set; }

     
       // [DataType(DataType.Text)]
       // [Display(Name = "TypeClaasroom")]

        [StringLength(50, MinimumLength = 3)]
        public string TypeClassroom { get; set; }

    }
}
