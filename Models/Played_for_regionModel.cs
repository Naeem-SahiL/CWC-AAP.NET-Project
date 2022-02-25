using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class Played_for_regionModel
    {
        [Required]
        [Display(Name = "Palyer Id")]
        public int player_id { get; set; }


   
        [Display(Name = "Palyer Name")]
        public string player_name { get; set; }

        [Required]
        [Display(Name = "Region Id")]
        public int region_id { get; set; }

      
        [Display(Name = "Region Name")]
        public string region_name { get; set; }


        [Display(Name = "Region T20 Id")]
        public Nullable<int> region_T_20_id { get; set; }


        [Display(Name = "Region ODI Id")]
        public Nullable<int> region_ODI { get; set; }

        [Display(Name = "Region Test Id")]
        public Nullable<int> region_test_id { get; set; }

        public List<PlayerModel> players{ get; set; }
        public List<RegionModel> regions{ get; set; }
        

    }
}
