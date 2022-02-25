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
    public class RegionTeamsController : Controller
    {

        public IConfiguration _configuration { get; private set; }
        public RegionTeamsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEAMS_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }


        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            RegionTeamsModel regionTeams = new RegionTeamsModel();
            fill_list(regionTeams);
            return View(regionTeams);
        }

        //team_id,team_name,country_name,team_rank,no_of_bowlers,no_of_batsmans,no_of_wins,no_of_loses,no_of_draws,cap_id,wicketkeeper_id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, RegionTeamsModel regionTeams)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_TEAMS_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionTeams.team_id);
                    cmd.Parameters.AddWithValue("team_name", regionTeams.team_name);
                    cmd.Parameters.AddWithValue("country_name", regionTeams.country_name);
                    cmd.Parameters.AddWithValue("team_rank", regionTeams.team_rank);
                    cmd.Parameters.AddWithValue("no_of_bowlers", regionTeams.no_of_bowlers);
                    cmd.Parameters.AddWithValue("no_of_batsmans", regionTeams.no_of_batsmans);
                    cmd.Parameters.AddWithValue("no_of_wins", regionTeams.no_of_wins);
                    cmd.Parameters.AddWithValue("no_of_loses", regionTeams.no_of_loses);
                    cmd.Parameters.AddWithValue("no_of_draws", regionTeams.no_of_draws);
                    cmd.Parameters.AddWithValue("cap_id", regionTeams.cap_id);
                    cmd.Parameters.AddWithValue("wicketkeeper_id", regionTeams.wk_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEAMS_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionTeams);
            
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", regionTeams) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            RegionTeamsModel regionTeams = new RegionTeamsModel();
            if (id > 0)
            {
                regionTeams = fetch(id);
                fill_list(regionTeams);
                
            }
            return View(regionTeams);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RegionTeamsModel regionTeams)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_TEAMS_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionTeams.team_id);
                    cmd.Parameters.AddWithValue("team_name", regionTeams.team_name);
                    cmd.Parameters.AddWithValue("country_name", regionTeams.country_name);
                    cmd.Parameters.AddWithValue("team_rank", regionTeams.team_rank);
                    cmd.Parameters.AddWithValue("no_of_bowlers", regionTeams.no_of_bowlers);
                    cmd.Parameters.AddWithValue("no_of_batsmans", regionTeams.no_of_batsmans);
                    cmd.Parameters.AddWithValue("no_of_wins", regionTeams.no_of_wins);
                    cmd.Parameters.AddWithValue("no_of_loses", regionTeams.no_of_loses);
                    cmd.Parameters.AddWithValue("no_of_draws", regionTeams.no_of_draws);
                    cmd.Parameters.AddWithValue("cap_id", regionTeams.cap_id);
                    cmd.Parameters.AddWithValue("wicketkeeper_id", regionTeams.wk_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEAMS_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionTeams);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", regionTeams) });
        }










        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            RegionTeamsModel regionTeams = fetch(id);

            return View(regionTeams);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("REGION_TEAMS_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEAMS_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public RegionTeamsModel fetch(int? id)
        {
            RegionTeamsModel regionTeams = new RegionTeamsModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("REGION_TEAMS_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    regionTeams.team_id = Convert.ToInt32(id);
                    regionTeams.team_name = tbl.Rows[0]["team_name"].ToString();
                    regionTeams.country_name = tbl.Rows[0]["country_name"].ToString();
                    regionTeams.team_rank = Convert.ToInt32(tbl.Rows[0]["team_rank"].ToString());
                    if (tbl.Rows[0]["no_of_bowlers"].ToString() != "")
                        regionTeams.no_of_bowlers = Convert.ToInt32(tbl.Rows[0]["no_of_bowlers"].ToString());
                    if (tbl.Rows[0]["no_of_batsmans"].ToString() != "")
                        regionTeams.no_of_batsmans = Convert.ToInt32(tbl.Rows[0]["no_of_batsmans"].ToString());
                    if (tbl.Rows[0]["no_of_wins"].ToString() != "")
                        regionTeams.no_of_wins = Convert.ToInt32(tbl.Rows[0]["no_of_wins"].ToString());
                    if (tbl.Rows[0]["no_of_loses"].ToString() != "")
                        regionTeams.no_of_loses = Convert.ToInt32(tbl.Rows[0]["no_of_loses"].ToString());
                    if (tbl.Rows[0]["no_of_draws"].ToString() != "")
                        regionTeams.no_of_draws = Convert.ToInt32(tbl.Rows[0]["no_of_draws"].ToString());

                    if (tbl.Rows[0]["cap_id"].ToString() != "")
                        regionTeams.cap_id = Convert.ToInt32(tbl.Rows[0]["cap_id"].ToString());
                    if (tbl.Rows[0]["wicketkeeper_id"].ToString() != "")
                        regionTeams.wk_id = Convert.ToInt32(tbl.Rows[0]["wicketkeeper_id"].ToString());
                }
                return regionTeams;
            }


            

        }


        [NonAction]

        public void fill_list(RegionTeamsModel regionTeams)
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
                regionTeams.Captains = l1;

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
                regionTeams.Wicket_keepers = l2;
                con.Close();
            }
            
        }
    }
}
