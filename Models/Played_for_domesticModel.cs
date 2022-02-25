using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class Played_for_domesticModel
    {
        [Required]
        [Display(Name = "Palyer Id")]
        public int player_id { get; set; }


   
        [Display(Name = "Palyer Name")]
        public string player_name { get; set; }

        [Required]
        [Display(Name = "Domestic Id")]
        public int domestic_id { get; set; }

      
        [Display(Name = "Domestic Name")]
        public string domestic_nmae { get; set; }


        [Display(Name = "Domestic T20 Id")]
        public Nullable<int> domestic_T_20_id { get; set; }


        [Display(Name = "Domestic ODI Id")]
        public Nullable<int> domestic_ODI { get; set; }

        [Display(Name = "Domestic Test Id")]
        public Nullable<int> domestic_test_id { get; set; }

        public List<PlayerModel> players{ get; set; }
        public List<DomesticModel> domestics{ get; set; }
        

    }
}
