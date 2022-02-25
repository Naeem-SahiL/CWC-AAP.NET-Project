using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class RefreesViewModel
    {
        [Required]
        [Display(Name = "id")]
        public int refree_id { get; set; }
        [Required]
        [Display(Name = "Refree Name")]
        public string refree_name { get; set; }
        [Display(Name = "Non of Matches Umpired")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> refered_matches { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
    }
}
