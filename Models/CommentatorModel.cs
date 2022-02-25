using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class CommentatorModel
    {
        [Required]
        [Display(Name = "Id")]
        public int commentator_id { get; set; }
        [Required]
        [Display(Name = "Commentator Name")]
        public string name { get; set; }
        [Display(Name = "Commented Matches")]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be positive")]
        public Nullable<int> commentator_matches { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
    }
}
