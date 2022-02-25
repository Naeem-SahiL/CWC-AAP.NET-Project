using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class CW_BowlerModel
    {
        [Required]
        [Display(Name = "Id")]
        public int p_id { get; set; }


        [Display(Name = "Bowling Rank")]
        public int bow_rank { get; set; }

        [Display(Name = "Best Figure")]
        public string best_figure { get; set; }

        [Display(Name = "Run conceded")]
        public int runs_conceded { get; set; }

        [Display(Name = "Wickets")]
        public int wickets { get; set; }

        [Display(Name = "FiveWick")]
        public int fiveWick { get; set; }

        [Display(Name = "Economy")]
        public float econymy { get; set; }

    }
}
