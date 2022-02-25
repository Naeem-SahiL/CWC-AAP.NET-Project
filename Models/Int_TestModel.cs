using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.Models
{
    public class Int_TestModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Test_id { get; set; }

        [Display(Name = "Test Batting Rank")]
        public Nullable<int> test_bat_rank { get; set; }

        [Display(Name = "Fifties")]
        public Nullable<int> fifties { get; set; }

        [Display(Name = "Style")]
        public string style { get; set; }

        [Display(Name = "Runs")]
        public Nullable<int> runs { get; set; }

        [Display(Name = "Centuries")]
        public Nullable<int> hundreds { get; set; }

        [Display(Name = "Fours")]
        public Nullable<int> fours { get; set; }

        [Display(Name = "Sixes")]
        public Nullable<int> sixes { get; set; }

        [Display(Name = "Average")]
        public Nullable<float> average { get; set; }

        [Display(Name = "Bowling Rank")]
        public Nullable<int> test_bow_rank { get; set; }

        [Display(Name = "Best Figure")]
        public string best_figure { get; set; }

        [Display(Name = "Run conceded")]
        public Nullable<int> runs_conceded { get; set; }

        [Display(Name = "Wickets")]
        public Nullable<int> wickets { get; set; }

        [Display(Name = "FiveWick")]
        public Nullable<int> fiveWick { get; set; }

        [Display(Name = "Economy")]
        public Nullable<float> econymy { get; set; }
    }
}
