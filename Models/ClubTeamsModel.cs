using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class ClubTeamsModel
    {
        [Required]
        [Display(Name = "Team Id")]
        public int team_id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string team_name { get; set; }


        [Required]
        [Display(Name = "Country")]
        public string country_name { get; set; }


        [Required]
        [Display(Name = "Rank")]
        public int team_rank { get; set; }


        [Display(Name = "Bowlers")]
        [Range(1, 10, ErrorMessage = "Value should be from 1 to 10")]
        public int no_of_bowlers { get; set; }

        [Display(Name = "Batsmen")]
        [Range(1, 11, ErrorMessage = "Value should be from 1 to 11")]
        public int no_of_batsmans { get; set; }

        [Display(Name = "Wins")]
        public int no_of_wins { get; set; }

        [Display(Name = "Lossses")]
        public int no_of_loses { get; set; }

        [Display(Name = "Draws")]
        public int no_of_draws { get; set; }

        [Display(Name = "Captain id")]

        public int cap_id { get; set; }

        [Display(Name = "Wicket keeper id")]
        public int wk_id { get; set; }

        
        public List<CaptainModel> Captains { get; set; }

        public List<WicketKeeperModel> Wicket_keepers { get; set; }
    }
}
