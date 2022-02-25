using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class WorldCupModel
    {
        [Required]
        [Display(Name = "Id")]
        public int worldcup_id { get; set; }

        [Required]
        [Display(Name = "No of teams")]
        [Range(14, 14, ErrorMessage = "Value should be 14")]
        public int no_of_teams { get; set; }


        [Required]
        [Display(Name = "Place")]
        public string place { get; set; }

        [Required]
        [Display(Name = "Type of World Cup")]
        public string format_of_wc { get; set; }
        
        [Required]
        [Display(Name = "Winner Team")]
        public string winnerteam { get; set; }
        

        [Required]
        [Display(Name = "World Cup Year")]
        [Range(1975, 2021, ErrorMessage = "Value should be between 1975 and 2021")]
        public int Worldcup_year { get; set; }

        public List<string> format { get; set; }
        
    }
}
