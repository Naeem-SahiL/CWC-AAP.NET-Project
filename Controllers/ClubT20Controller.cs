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
    public class ClubT20Controller : Controller
    {
        private readonly IConfiguration _configuration;

        public ClubT20Controller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            ClubT20Model rcrd = fetch(id);

            return View(rcrd);
        }


        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             ClubT20Model clubT20 = new ClubT20Model();
            fill_list(clubT20);
            return View(clubT20);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, ClubT20Model clubT20)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_T20_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubT20.club_T_20_id);
                    cmd.Parameters.AddWithValue("T20_bat_rank", clubT20.T20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", clubT20.fifties);
                    cmd.Parameters.AddWithValue("style", clubT20.style);
                    cmd.Parameters.AddWithValue("runs", clubT20.runs);
                    cmd.Parameters.AddWithValue("hundreds", clubT20.hundreds);
                    cmd.Parameters.AddWithValue("fours", clubT20.fours);
                    cmd.Parameters.AddWithValue("sixes", clubT20.sixes);
                    cmd.Parameters.AddWithValue("average", clubT20.average);
                    cmd.Parameters.AddWithValue("T20_bow_rank", clubT20.T20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", clubT20.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", clubT20.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", clubT20.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", clubT20.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", clubT20.econymy);
                    cmd.Parameters.AddWithValue("team_id", clubT20.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(clubT20);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", clubT20) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            ClubT20Model clubT20 = new ClubT20Model();
            if (id > 0)
            {
                clubT20 = fetch(id);
            }
            fill_list(clubT20);
            return View(clubT20);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  ClubT20Model clubT20)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_T20_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubT20.club_T_20_id);
                    cmd.Parameters.AddWithValue("T20_bat_rank", clubT20.T20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", clubT20.fifties);
                    cmd.Parameters.AddWithValue("style", clubT20.style);
                    cmd.Parameters.AddWithValue("runs", clubT20.runs);
                    cmd.Parameters.AddWithValue("hundreds", clubT20.hundreds);
                    cmd.Parameters.AddWithValue("fours", clubT20.fours);
                    cmd.Parameters.AddWithValue("sixes", clubT20.sixes);
                    cmd.Parameters.AddWithValue("average", clubT20.average);
                    cmd.Parameters.AddWithValue("T20_bow_rank", clubT20.T20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", clubT20.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", clubT20.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", clubT20.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", clubT20.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", clubT20.econymy);
                    cmd.Parameters.AddWithValue("team_id", clubT20.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(clubT20);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", clubT20) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            ClubT20Model clubT20 = fetch(id);

            return View(clubT20);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CLUB_T20_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public ClubT20Model fetch(int? id)
        {
            ClubT20Model clubT20 = new ClubT20Model();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("CLUB_T20_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    clubT20.club_T_20_id= Convert.ToInt32(id);
                    clubT20.T20_bat_rank = Convert.ToInt32(tbl.Rows[0]["T20_bat_rank"].ToString());
                    clubT20.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    clubT20.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    clubT20.style = tbl.Rows[0]["style"].ToString();
                    clubT20.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    clubT20.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    clubT20.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    clubT20.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    clubT20.T20_bow_rank = Convert.ToInt32(tbl.Rows[0]["T20_bow_rank"].ToString());
                    clubT20.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    clubT20.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    clubT20.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    clubT20.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    clubT20.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    clubT20.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return clubT20;
            }

        }



        [NonAction]

        public void fill_list(ClubT20Model clubT20)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select team_id,team_name from clubteams", con);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                List<ClubTeamsModel> l1 = new List<ClubTeamsModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ClubTeamsModel clubTeams = new ClubTeamsModel();
                    clubTeams.team_id = Convert.ToInt32(tbl.Rows[i]["team_id"].ToString());
                    clubTeams.team_name = tbl.Rows[i]["team_name"].ToString();
                    l1.Add(clubTeams);
                }
                clubT20.Teams = l1;
                con.Close();
            }

        }
    }
}
