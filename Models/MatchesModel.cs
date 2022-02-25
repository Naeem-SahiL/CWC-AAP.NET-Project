using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class MatchesModel
    {
        [Required]
        [Display(Name = "Match Id")]
        public int match_id { get; set; }

        [Required]
        [Display(Name = "First Team")]
        public string firstteam_name{ get; set; }

        [Required]
        [Display(Name = "Second Team")]
        public string secondteam_name { get; set; }

     
        [Display(Name = "Loser")]
        public string loser { get; set; }

       
        [Display(Name = "Winner")]
        public string winner { get; set; }


        [Display(Name = "Match Date")]
        public DateTime match_date { get; set; }


        [Display(Name = "Match Time")]
        public string matchtime { get; set; }

        [Display(Name = "Refree Id")]
        public Nullable<int> referee_id { get; set; }

        [Display(Name = "Stadium Id")]
        public Nullable<int> stad_id { get; set; }

        [Display(Name = "Score Card Id")]
        public Nullable<int> sc_id { get; set; }


        public List<RefreesViewModel> Refrees { get; set; }
        public List<SadiumModel> Stadiums { get; set; }

    }
}
