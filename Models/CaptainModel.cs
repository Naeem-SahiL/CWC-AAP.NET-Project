using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class CaptainModel
    {
        [Required]
        [Display(Name = "Id")]
        public int cap_id { get; set; }
        [Required]
        [Display(Name = "Captain Name")]
        public string cap_name { get; set; }

        [Display(Name = "Years of Captancy")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> years_of_captaincy { get; set; }
     
        [Display(Name = "Wins")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> total_wins { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        [Display(Name = "Losses")]
        public Nullable<int> total_loses { get; set; }


    }
}
