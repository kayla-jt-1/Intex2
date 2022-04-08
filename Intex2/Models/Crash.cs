using System;
using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }

        // this is a string in the database so it must be a string here
        [Required (ErrorMessage ="Please enter a datetime")]
        public string CRASH_DATETIME { get; set; }

        [Required(ErrorMessage = "Please enter a valid route")]
        [Range(0, 1000000000)]
        public string ROUTE { get; set; }

        [Required(ErrorMessage = "Please enter a valid milepoint")]
        public double MILEPOINT { get; set; }

        [Required(ErrorMessage = "Please enter a valid latitude")]
        [Range(0, 1000000000)]
        public double LAT_UTM_Y { get; set; }

        [Required(ErrorMessage = "Please enter a valid longitude")]
        [Range(0, 1000000000)]
        public double LONG_UTM_X { get; set; }

        [Required(ErrorMessage = "Please enter a valid address")]
        public string MAIN_ROAD_NAME { get; set; }

        [Required(ErrorMessage = "Please enter a valid city")]
        public string CITY { get; set; }

        [Required(ErrorMessage = "Please enter a valid county")]
        public string COUNTY_NAME { get; set; }

        [Required(ErrorMessage = "Please select a crash severity")]
        public int CRASH_SEVERITY_ID { get; set; }

        // these values are either "true" or "false"
        public string WORK_ZONE_RELATED { get; set; }
        public string PEDESTRIAN_INVOLVED { get; set; }
        public string BICYCLIST_INVOLVED { get; set; }
        public string MOTORCYCLE_INVOLVED { get; set; }
        public string IMPROPER_RESTRAINT { get; set; }
        public string UNRESTRAINED { get; set; }
        public string DUI { get; set; }
        public string INTERSECTION_RELATED { get; set; }
        public string WILD_ANIMAL_RELATED { get; set; }
        public string DOMESTIC_ANIMAL_RELATED { get; set; }
        public string OVERTURN_ROLLOVER { get; set; }
        public string COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public string TEENAGE_DRIVER_INVOLVED { get; set; }
        public string OLDER_DRIVER_INVOLVED { get; set; }
        public string NIGHT_DARK_CONDITION { get; set; }
        public string SINGLE_VEHICLE { get; set; }
        public string DISTRACTED_DRIVING { get; set; }
        public string DROWSY_DRIVING { get; set; }
        public string ROADWAY_DEPARTURE { get; set; }
    }
}
