using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class Played_for_clubModel
    {
        [Required]
        [Display(Name = "Palyer Id")]
        public int player_id { get; set; }


   
        [Display(Name = "Palyer Name")]
        public string player_name { get; set; }

        [Required]
        [Display(Name = "Club Id")]
        public int club_id { get; set; }

      
        [Display(Name = "Club Name")]
        public string club_name { get; set; }


        [Display(Name = "Club T20 Id")]
        public Nullable<int> club_T_20_id { get; set; }


        [Display(Name = "Club ODI Id")]
        public Nullable<int> club_ODI { get; set; }

        [Display(Name = "Club Test Id")]
        public Nullable<int> club_test_id { get; set; }

        public List<PlayerModel> players{ get; set; }
        public List<ClubViewModel> clubs{ get; set; }
        public List<ClubT20Model> club_t20s{ get; set; }
        public List<ClubODIModel> club_ODIs{ get; set; }
        public List<ClubTestModel> club_Tests{ get; set; }

    }
}
