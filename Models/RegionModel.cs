using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class RegionModel
    {
        [Required]
        [Display(Name = "id")]
        public int region_id { get; set; }

        [Required]
        [Display(Name = "Region Name")]
        public string region_name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
     
        [Display(Name = "Location")]
        public string region_location { get; set; }
        
    }
}
