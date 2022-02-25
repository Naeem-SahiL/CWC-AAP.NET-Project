using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class ScorecardModel
    {
        [Required]
        [Display(Name = "Id")]
        public int sc_id { get; set; }


        [Display(Name = "Team 1 Scores")]
        public Nullable<int> first_team_runsscored { get; set; }

        [Display(Name = "Team 1 ballfaced")]
        public Nullable<int> first_team_ballsfaced { get; set; }

        [Display(Name = "Team 1 sixers")]
        public Nullable<int> first_team_sixers { get; set; }

        [Display(Name = "Team 1 fours")]
        public Nullable<int> first_team_fours { get; set; }

        [Display(Name = "Team 1 Maidens")]
        public Nullable<int> first_team_maidens { get; set; }

        [Display(Name = "Team 1 Total Outs")]
        public Nullable<int> first_team_totalout { get; set; }

        [Display(Name = "Team 1 Wickets Taken")]
        public Nullable<int> first_team_wktstaken { get; set; }

        [Display(Name = "Team 1 balls bowled")]
        public Nullable<int> first_team_ballsbowled { get; set; }


        [Display(Name = "Team 2 Scores")]
        public Nullable<int> second_team_runsscored { get; set; }

        [Display(Name = "Team 2 ballfaced")]
        public Nullable<int> second_team_ballsfaced { get; set; }

        [Display(Name = "Team 2 sixers")]
        public Nullable<int> second_team_sixers { get; set; }

        [Display(Name = "Team 2 fours")] 
        public Nullable<int> second_team_fours { get; set; }

        [Display(Name = "Team 2 Maidens")]
        public Nullable<int> second_team_maidens { get; set; }

        [Display(Name = "Team 2 Total Outs")]
        public Nullable<int> second_team_totalout { get; set; }

        [Display(Name = "Team 2 Wickets Taken")]
        public Nullable<int> second_team_wktstaken { get; set; }

        [Display(Name = "Team 2 balls bowled")]
        public Nullable<int> second_team_ballsbowled { get; set; }

    }
}
