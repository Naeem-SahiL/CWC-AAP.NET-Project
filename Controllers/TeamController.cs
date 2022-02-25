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
    public class TeamController : Controller
    {

        public IConfiguration _configuration { get; private set; }
        public TeamController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from TEAM_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }


        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            TeamModel team = new TeamModel();
            fill_list(team);
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, TeamModel team)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("TEAM_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", team.team_id);
                    cmd.Parameters.AddWithValue("team_name", team.team_name);
                    cmd.Parameters.AddWithValue("country_name", team.country_name);
                    cmd.Parameters.AddWithValue("team_rank", team.team_rank);
                    cmd.Parameters.AddWithValue("no_of_bowlers", team.no_of_bowlers);
                    cmd.Parameters.AddWithValue("no_of_batsmans", team.no_of_batsmans);
                    cmd.Parameters.AddWithValue("no_of_wins", team.no_of_wins);
                    cmd.Parameters.AddWithValue("no_of_loses", team.no_of_loses);
                    cmd.Parameters.AddWithValue("no_of_draws", team.no_of_draws);
                    cmd.Parameters.AddWithValue("cap_id", team.cap_id);
                    cmd.Parameters.AddWithValue("wicketkeeper_id", team.wk_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from TEAM_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(team);
            
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", team) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            TeamModel team = new TeamModel();
            if (id > 0)
            {
                team = fetch(id);
                fill_list(team);
                
            }
            return View(team);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TeamModel team)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("TEAM_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", team.team_id);
                    cmd.Parameters.AddWithValue("team_name", team.team_name);
                    cmd.Parameters.AddWithValue("country_name", team.country_name);
                    cmd.Parameters.AddWithValue("team_rank", team.team_rank);
                    cmd.Parameters.AddWithValue("no_of_bowlers", team.no_of_bowlers);
                    cmd.Parameters.AddWithValue("no_of_batsmans", team.no_of_batsmans);
                    cmd.Parameters.AddWithValue("no_of_wins", team.no_of_wins);
                    cmd.Parameters.AddWithValue("no_of_loses", team.no_of_loses);
                    cmd.Parameters.AddWithValue("no_of_draws", team.no_of_draws);
                    cmd.Parameters.AddWithValue("cap_id", team.cap_id);
                    cmd.Parameters.AddWithValue("wicketkeeper_id", team.wk_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from TEAM_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(team);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", team) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            TeamModel team = fetch(id);

            return View(team);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("TEAM_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from TEAM_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public TeamModel fetch(int? id)
        {
            TeamModel team = new TeamModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("TEAM_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    team.team_id = Convert.ToInt32(id);
                    team.team_name = tbl.Rows[0]["team_name"].ToString();
                    team.country_name = tbl.Rows[0]["country_name"].ToString();
                    team.team_rank = Convert.ToInt32(tbl.Rows[0]["team_rank"].ToString());
                    team.no_of_bowlers = Convert.ToInt32(tbl.Rows[0]["no_of_bowlers"].ToString());
                    team.no_of_batsmans = Convert.ToInt32(tbl.Rows[0]["no_of_batsmans"].ToString());
                    team.no_of_wins = Convert.ToInt32(tbl.Rows[0]["no_of_wins"].ToString());
                    team.no_of_loses = Convert.ToInt32(tbl.Rows[0]["no_of_loses"].ToString());
                    team.no_of_draws = Convert.ToInt32(tbl.Rows[0]["no_of_draws"].ToString());
                    team.cap_id = Convert.ToInt32(tbl.Rows[0]["cap_id"].ToString());
                    team.wk_id = Convert.ToInt32(tbl.Rows[0]["wicketkeeper_id"].ToString());
                }
                return team;
            }

        }


        [NonAction]

        public void fill_list(TeamModel team)
        {
           
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select cap_id,cap_name from captain", con);
                SqlCommand cmd1 = new SqlCommand("select wk_id,wk_name from wicketkeeper", con);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                List<CaptainModel> l1 = new List<CaptainModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    CaptainModel cap = new CaptainModel();
                    cap.cap_id = Convert.ToInt32(tbl.Rows[i]["cap_id"].ToString());
                    cap.cap_name = tbl.Rows[i]["cap_name"].ToString();
                    l1.Add(cap);
                }
                team.Captains = l1;

                List<WicketKeeperModel> l2 = new List<WicketKeeperModel>();
                tbl.Rows.Clear();
                SqlDataAdapter adptr1 = new SqlDataAdapter(cmd1);
                adptr1.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    WicketKeeperModel wicketKeeper = new WicketKeeperModel();

                    wicketKeeper.wk_id = Convert.ToInt32(tbl.Rows[i]["wk_id"].ToString());
                    wicketKeeper.wk_name = tbl.Rows[i]["wk_name"].ToString();
                    l2.Add(wicketKeeper);
                }
                team.Wicket_keepers = l2;
                con.Close();
            }
            
        }
    }
}
