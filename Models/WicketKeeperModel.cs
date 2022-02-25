using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class WicketKeeperModel
    {
        [Required]
        [Display(Name = "Id")]
        public int wk_id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string wk_name { get; set; }

        [Display(Name = "Rank")]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> wk_rank { get; set; }

        [Required]
        [Display(Name = "No. of Catches")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> no_of_catches { get; set; }

        [Required]
        [Display(Name = "No. of Stumps")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> no_of_stumps { get; set; }
    }
}
