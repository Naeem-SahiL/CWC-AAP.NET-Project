using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class RegionT20Model
    {
        [Required]
        [Display(Name = "Id")]
        public int region_id { get; set; }

        [Display(Name = "T20 Batting Rank")]
        public int T_20_bat_rank { get; set; }

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
        public Nullable<float> average { get; set; }

        [Display(Name = "Bowling Rank")]
        public int T_20_bow_rank { get; set; }

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

        [Display(Name = "Region Team id")]
        public int team_id { get; set; }
        public List<RegionTeamsModel> Teams { get; set; }
    }
}
