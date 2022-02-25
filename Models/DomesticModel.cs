using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using core.Models;

namespace core.Models
{
    public class DomesticModel
    {
        
        [Required]
        [Display(Name = "Id")]
        public int domestic_id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string domestic_nmae { get; set; }

        
        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }

    }
}
