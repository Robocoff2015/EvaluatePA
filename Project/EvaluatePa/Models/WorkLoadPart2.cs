<<<<<<< Updated upstream
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> Stashed changes

namespace EvaluatePa.Models
{
    public class WorkLoadPart2
    {
<<<<<<< Updated upstream
        [Key]
       public string IdSubject { get; set; } 

        public string Subject { get; set; }

        public int Hour { get; set; }

      //  public int IdPA { get; set; } 
      
=======
       [Key]
        public int TdSubject { get; set; }

        public string NameSubject { get; set; }

        public int Hour { get; set; }

     //   public ICollection<DevelopPA> DevelopPAs { get; set; }
>>>>>>> Stashed changes
    }
}
