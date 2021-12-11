<<<<<<< Updated upstream
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> Stashed changes

namespace EvaluatePa.Models
{
    public class WorkLoadPart1
    {
      [Key]
<<<<<<< Updated upstream
     public int IdWork { get; set; } 

     public string AmountWorkLoad { get; set; }

    // public int IdPA { get; set; }
      
=======
      public int IdWork { get; set; }

      public decimal AmountWorkLoad { get; set; }

    //  public ICollection<DevelopPA> DevelopPAs { get; set; }

>>>>>>> Stashed changes
    }
}
