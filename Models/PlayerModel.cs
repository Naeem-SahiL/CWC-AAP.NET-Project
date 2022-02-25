using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class PlayerModel
    {
        [Required]
        [Display(Name = "Player_Id")]
        public int p_id{ get; set; }

        [Required]
        [Display(Name = "Team_Id")]
        public int team_id { get; set; } 
        public List<TeamModel> Teams{ get; set; }

        [Required]
        [Display(Name = "First_Name")]
        public string first_name { get; set; }
        
        [Required]
        [Display(Name = "Last_Name")]
        public string last_name { get; set; }
        
        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
        
        [Required]
        [Display(Name = "Height")]
        public float height { get; set; }
        
        [Required]
        [Display(Name = "Date_of_birth")]
        public DateTime dob { get; set; }
        
         [Required]
        [Display(Name = "Debut_Date")]
        public DateTime debut_date { get; set; }


        [Display(Name = "T20_Played")]
        public Nullable<int> T_20s { get; set; }

        [Display(Name = "ODI_Played")]
        public Nullable<int> ODIS { get; set; }


        [Display(Name = "Test_Played")]
        public Nullable<int> tests { get; set; }

    }
}
