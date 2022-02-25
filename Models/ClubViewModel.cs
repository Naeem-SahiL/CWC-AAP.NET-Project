using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class ClubViewModel
    {
        [Required]
        [Display(Name = "id")]
        public int club_id { get; set; }

        [Required]
        [Display(Name = "Club Name")]
        public string club_name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
        
    }
}
