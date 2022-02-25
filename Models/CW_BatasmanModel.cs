using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class CW_BatasmanModel
    {
        [Required]
        [Display(Name = "Id")]
        public int p_id { get; set; }

        [Display(Name = "Batting Rank")]
        public int bat_rank { get; set; }

        [Display(Name = "Fifties")]
        public int fifties { get; set; }

        [Display(Name = "Style")]
        public string style { get; set; }

        [Display(Name = "Runs")]
        public int runs { get; set; }

        [Display(Name = "Centuries")]
        public int hundreds { get; set; }

        [Display(Name = "Fours")]
        public int fours { get; set; }

        [Display(Name = "Sixes")]
        public int sixes { get; set; }

        [Display(Name = "Average")]
        public int average { get; set; }

    }
}
