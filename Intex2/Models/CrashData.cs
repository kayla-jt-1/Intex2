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
        public float main_road_name_Other { get; set; }
        public float work_zone_related { get; set; }
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
                main_road_name_Other, work_zone_related, pedestrian_involved, bicyclist_involved, motorcycle_involved, improper_restraint, unrestrained, dui, intersection_related, wild_animal_related, overturn_rollover, commercial_motor_veh_involved, teenage_driver_involved, older_driver_involved, night_dark_condition, single_vehicle, distracted_driving, drowsy_driving, roadway_departure, crash_month, crash_year, crash_day, crash_hour, route_15, route_89, route_Other, city_OutsideCityLimits, city_Other, city_SaltLakeCity, city_WestValleyCity, county_name_Davis, county_name_Other, county_name_SaltLake, county_name_Utah, county_name_Weber
            };

            int[] dimensions = new int[] { 1, 35 };

            return new DenseTensor<float>(data, dimensions);
        }
    }
}

