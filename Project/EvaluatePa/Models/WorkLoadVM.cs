using System.Collections.Generic;

namespace EvaluatePa.Models
{
    public class WorkLoadVM
    {
        public int AountWorkLoad { get; set; }

        public List<WorkLoadPart2> Workloadpart2s { get; set; }

        public int HourSupportSubject { get; set; }

       public int HourQsubject { get; set; }

        public int HourFocus { get; set; }


    }
}
