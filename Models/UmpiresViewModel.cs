using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
   
    public class UmpiresViewModel
    {
        [Required]
        [Display(Name ="Id")]
        public int umpire_id { get; set; }
        [Required]
        [Display(Name ="Umpire Name")]
        public string umpire_name { get; set; }
        [Display(Name = "Non of Matches Umpired")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> maches_umpired { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
    }
}
