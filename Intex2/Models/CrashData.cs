using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    // THIS IS USED FOR THE ML MODEL
    public class CrashData
    {
        public float pedestrian_involved { get; set; }
        public float bicyclist_involved { get; set; }
        public float motorcycle_involved { get; set; }
        public float improper_restraint { get; set; }
        public float unrestrained { get; set; }
        public float dui { get; set; }
        public float intersection_related { get; set; }
        public float wild_animal_related { get; set; }
        public float overturn_rollover { get; set; }
        public float commercial_motor_veh_involved { get; set; }
        public float teenage_driver_involved { get; set; }
        public float older_driver_involved { get; set; }
        public float night_dark_condition { get; set; }
        public float single_vehicle { get; set; }
        public float distracted_driving { get; set; }
        public float drowsy_driving { get; set; }
        public float roadway_departure { get; set; }
        public float crash_month { get; set; }
        public float crash_year { get; set; }
        public float crash_day { get; set; }
        public float crash_hour { get; set; }
        public float route_15 { get; set; }
        public float route_89 { get; set; }
        public float route_Other { get; set; }
        public float main_road_name_I_15 { get; set; }
        public float city_OutsideCityLimits { get; set; }
        public float city_Other { get; set; }
        public float city_SaltLakeCity { get; set; }
        public float city_WestValleyCity { get; set; }
        public float county_name_Davis { get; set; }
        public float county_name_Other { get; set; }
        public float county_name_SaltLake { get; set; }
        public float county_name_Utah { get; set; }
        public float county_name_Weber { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                pedestrian_involved, bicyclist_involved, motorcycle_involved, improper_restraint, unrestrained, dui, intersection_related, wild_animal_related, overturn_rollover, commercial_motor_veh_involved, teenage_driver_involved, older_driver_involved, night_dark_condition, single_vehicle, distracted_driving, drowsy_driving, roadway_departure, crash_month, crash_year, crash_day, crash_hour, route_15, route_89, route_Other, main_road_name_I_15, city_OutsideCityLimits, city_Other, city_SaltLakeCity, city_WestValleyCity, county_name_Davis, county_name_Other, county_name_SaltLake, county_name_Utah, county_name_Weber
            };

            int[] dimensions = new int[] { 1, 34 };

            return new DenseTensor<float>(data, dimensions);
        }
    }
}

//public Int64 pedestrian_involved { get; set; }
//public Int64 bicyclist_involved { get; set; }
//public Int64 motorcycle_involved { get; set; }
//public Int64 improper_restraint { get; set; }
//public Int64 unrestrained { get; set; }
//public Int64 dui { get; set; }
//public Int64 intersection_related { get; set; }
//public Int64 wild_animal_related { get; set; }
//public Int64 overturn_rollover { get; set; }
//public Int64 commercial_motor_veh_involved { get; set; }
//public Int64 teenage_driver_involved { get; set; }
//public Int64 older_driver_involved { get; set; }
//public Int64 night_dark_condition { get; set; }
//public Int64 single_vehicle { get; set; }
//public Int64 distracted_driving { get; set; }
//public Int64 drowsy_driving { get; set; }
//public Int64 roadway_departure { get; set; }
//public Int64 crash_month { get; set; }
//public Int64 crash_year { get; set; }
//public Int64 crash_day { get; set; }
//public Int64 crash_hour { get; set; }
//public Int64 route_15 { get; set; }
//public Int64 route_89 { get; set; }
//public Int64 route_Other { get; set; }
//public Int64 main_road_name_I_15 { get; set; }
//public Int64 city_OutsideCityLimits { get; set; }
//public Int64 city_Other { get; set; }
//public Int64 city_SaltLakeCity { get; set; }
//public Int64 city_WestValleyCity { get; set; }
//public Int64 county_name_Davis { get; set; }
//public Int64 county_name_Other { get; set; }
//public Int64 county_name_SaltLake { get; set; }
//public Int64 county_name_Utah { get; set; }
//public Int64 county_name_Weber { get; set; }

