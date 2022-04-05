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
        public int pedestrian_involved { get; set; }
        public int bicyclist_involved { get; set; }
        public int motorcycle_involved { get; set; }
        public int improper_restraint { get; set; }
        public int unrestrained { get; set; }
        public int dui { get; set; }
        public int intersection_related { get; set; }
        public int wild_animal_related { get; set; }
        public int overturn_rollover { get; set; }
        public int commercial_motor_veh_involved { get; set; }
        public int teenage_driver_involved { get; set; }
        public int older_driver_involved { get; set; }
        public int night_dark_condition { get; set; }
        public int single_vehicle { get; set; }
        public int distracted_driving { get; set; }
        public int drowsy_driving { get; set; }
        public int roadway_departure { get; set; }
        public int crash_month { get; set; }
        public int crash_year { get; set; }
        public int crash_day { get; set; }
        public int crash_hour { get; set; }
        public int route_15 { get; set; }
        public int route_89 { get; set; }
        public int route_Other { get; set; }
        public int main_road_name_I_15 { get; set; }
        public int city_OutsideCityLimits { get; set; }
        public int city_Other { get; set; }
        public int city_SaltLakeCity { get; set; }
        public int city_WestValleyCity { get; set; }
        public int county_name_Davis { get; set; }
        public int county_name_Other { get; set; }
        public int county_name_SaltLake { get; set; }
        public int county_name_Utah { get; set; }
        public int county_name_Weber { get; set; }

        public Tensor<int> AsTensor()
        {
            int[] data = new int[]
            {
                pedestrian_involved, bicyclist_involved, motorcycle_involved, improper_restraint, unrestrained, dui, intersection_related, wild_animal_related, overturn_rollover, commercial_motor_veh_involved, teenage_driver_involved, older_driver_involved, night_dark_condition, single_vehicle, distracted_driving, drowsy_driving, roadway_departure, crash_month, crash_year, crash_day, crash_hour, route_15, route_89, route_Other, main_road_name_I_15, city_OutsideCityLimits, city_Other, city_SaltLakeCity, city_WestValleyCity, county_name_Davis, county_name_Other, county_name_SaltLake, county_name_Utah, county_name_Weber
            };

            int[] dimensions = new int[] { 1, 8 };

            return new DenseTensor<int>(data, dimensions);
        }
    }
}
