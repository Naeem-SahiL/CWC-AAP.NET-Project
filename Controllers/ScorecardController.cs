using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace core.Controllers
{
    public class ScorecardController : Controller
    {

        private readonly IConfiguration _configuration;

        public ScorecardController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from SCORECARD_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }
        public IActionResult ScoreCard(int id)
        {
            ScorecardModel scorecard = fetch(id);
            return View(scorecard);
        }

        [NoDirectAccess]
        public IActionResult Add()
        {
            ScorecardModel scorecard = new ScorecardModel();
            return View(scorecard);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, ScorecardModel scorecardModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SCORECARD_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", scorecardModel.sc_id);
                    cmd.Parameters.AddWithValue("t1_runs", scorecardModel.first_team_runsscored);
                    cmd.Parameters.AddWithValue("t1_balls_faced", scorecardModel.first_team_ballsfaced);
                    cmd.Parameters.AddWithValue("t1_six", scorecardModel.first_team_sixers);
                    cmd.Parameters.AddWithValue("t1_fours", scorecardModel.first_team_fours);
                    cmd.Parameters.AddWithValue("t1_maidens", scorecardModel.first_team_maidens);
                    cmd.Parameters.AddWithValue("t1_totalouts", scorecardModel.first_team_totalout);
                    cmd.Parameters.AddWithValue("t1_wktstaken", scorecardModel.first_team_wktstaken);
                    cmd.Parameters.AddWithValue("t1_ballsbowld", scorecardModel.first_team_ballsbowled);

                    cmd.Parameters.AddWithValue("t2_runs", scorecardModel.second_team_runsscored);
                    cmd.Parameters.AddWithValue("t2_balls_faced", scorecardModel.second_team_ballsfaced);
                    cmd.Parameters.AddWithValue("t2_six", scorecardModel.second_team_sixers);
                    cmd.Parameters.AddWithValue("t2_fours", scorecardModel.second_team_fours);
                    cmd.Parameters.AddWithValue("t2_maidens", scorecardModel.second_team_maidens);
                    cmd.Parameters.AddWithValue("t2_totalouts", scorecardModel.second_team_totalout);
                    cmd.Parameters.AddWithValue("t2_wktstaken", scorecardModel.second_team_wktstaken);
                    cmd.Parameters.AddWithValue("t2_ballsbowld", scorecardModel.second_team_ballsbowled);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from SCORECARD_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", scorecardModel) });
        }


        [NoDirectAccess]
        public IActionResult Edit(int? id)
        {

            ScorecardModel scorecard = new ScorecardModel();
            if (id > 0)
            {
                scorecard = fetch(id);
            }
            return View(scorecard);
        }

        [HttpPost]
        public IActionResult Edit(int id, ScorecardModel scorecardModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SCORECARD_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", scorecardModel.sc_id);
                    cmd.Parameters.AddWithValue("t1_runs", scorecardModel.first_team_runsscored);
                    cmd.Parameters.AddWithValue("t1_balls_faced", scorecardModel.first_team_ballsfaced);
                    cmd.Parameters.AddWithValue("t1_six", scorecardModel.first_team_sixers);
                    cmd.Parameters.AddWithValue("t1_fours", scorecardModel.first_team_fours);
                    cmd.Parameters.AddWithValue("t1_maidens", scorecardModel.first_team_maidens);
                    cmd.Parameters.AddWithValue("t1_totalouts", scorecardModel.first_team_totalout);
                    cmd.Parameters.AddWithValue("t1_wktstaken", scorecardModel.first_team_wktstaken);
                    cmd.Parameters.AddWithValue("t1_ballsbowld", scorecardModel.first_team_ballsbowled);

                    cmd.Parameters.AddWithValue("t2_runs", scorecardModel.second_team_runsscored);
                    cmd.Parameters.AddWithValue("t2_balls_faced", scorecardModel.second_team_ballsfaced);
                    cmd.Parameters.AddWithValue("t2_six", scorecardModel.second_team_sixers);
                    cmd.Parameters.AddWithValue("t2_fours", scorecardModel.second_team_fours);
                    cmd.Parameters.AddWithValue("t2_maidens", scorecardModel.second_team_maidens);
                    cmd.Parameters.AddWithValue("t2_totalouts", scorecardModel.second_team_totalout);
                    cmd.Parameters.AddWithValue("t2_wktstaken", scorecardModel.second_team_wktstaken);
                    cmd.Parameters.AddWithValue("t2_ballsbowld", scorecardModel.second_team_ballsbowled);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from SCORECARD_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", scorecardModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            ScorecardModel sadium = fetch(id);

            return View(sadium);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SCORECARD_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from SCORECARD_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public ScorecardModel fetch(int? id)
        {
            ScorecardModel scorecard = new ScorecardModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SCORECARD_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    scorecard.sc_id = Convert.ToInt32(id);
                    try
                    {
                        scorecard.first_team_runsscored = Convert.ToInt32(tbl.Rows[0]["first_team_runsscored"].ToString());
                        scorecard.first_team_ballsfaced = Convert.ToInt32(tbl.Rows[0]["first_team_ballsfaced"].ToString());
                        scorecard.first_team_sixers = Convert.ToInt32(tbl.Rows[0]["first_team_sixers"].ToString());
                        scorecard.first_team_fours = Convert.ToInt32(tbl.Rows[0]["first_team_fours"].ToString());
                        scorecard.first_team_maidens = Convert.ToInt32(tbl.Rows[0]["first_team_maidens"].ToString());
                        scorecard.first_team_totalout = Convert.ToInt32(tbl.Rows[0]["first_team_totalout"].ToString());
                        scorecard.first_team_wktstaken = Convert.ToInt32(tbl.Rows[0]["first_team_wktstaken"].ToString());
                        scorecard.first_team_ballsbowled = Convert.ToInt32(tbl.Rows[0]["first_team_ballsbowled"].ToString());

                        scorecard.second_team_runsscored = Convert.ToInt32(tbl.Rows[0]["second_team_runsscored"].ToString());
                        scorecard.second_team_ballsfaced = Convert.ToInt32(tbl.Rows[0]["second_team_ballsfaced"].ToString());
                        scorecard.second_team_sixers = Convert.ToInt32(tbl.Rows[0]["second_team_sixers"].ToString());
                        scorecard.second_team_fours = Convert.ToInt32(tbl.Rows[0]["second_team_fours"].ToString());
                        scorecard.second_team_maidens = Convert.ToInt32(tbl.Rows[0]["second_team_maidens"].ToString());
                        scorecard.second_team_totalout = Convert.ToInt32(tbl.Rows[0]["second_team_totalout"].ToString());
                        scorecard.second_team_wktstaken = Convert.ToInt32(tbl.Rows[0]["second_team_wktstaken"].ToString());
                        scorecard.second_team_ballsbowled = Convert.ToInt32(tbl.Rows[0]["second_team_ballsbowled"].ToString());
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }
                return scorecard;
            }

        }
    }
}
