<<<<<<< Updated upstream
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> Stashed changes

namespace EvaluatePa.Models
{
    public class WorkLoadPart3
    {
<<<<<<< Updated upstream

     [Key]
     public string IdPSubject { get; set; }

     public int HourSupportSubject { get; set; }

    public int HourQsubject { get; set; }

    public int HourFocus { get; set; }

    //public string IdPA { get; set; }
=======
       [Key]
        public int IdPSupport { get; set; }

        public int HourSupportSubject { get; set; }

        public int HourQualitySubject {get;set;}

         public int HourFocus { get; set; }

      //  public ICollection<DevelopPA> DevelopPAs { get; set; }
>>>>>>> Stashed changes

    }
}
