using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class Played_for_WorldCupModel
    {
        [Required]
        [Display(Name = "Palyer Id")]
        public int player_id { get; set; }

        [Display(Name = "Palyer Name")]
        public string player_name { get; set; }

        [Display(Name = "World Cup id")]
        public int worldcup_id { get; set; }

        [Display(Name = "World Cup Name")]
        public string worldcup_name { get; set; }
        
        [Display(Name = "Previous World Cup id")]
        public Nullable<int> pw_id { get; set; }

        public List<PlayerModel> Players { get; set; }
        public List<WorldCupModel> WorldCups{ get; set; }
      
    }
}
