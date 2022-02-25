using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class SadiumModel
    {
        [Required]
        [Display(Name = "Id")]
        public int stad_id { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        public string stad_name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string place { get; set; }
     
        [Display(Name = "Capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Value should greater than 0")]
        public Nullable<int> capacity { get; set; }


    }
}
