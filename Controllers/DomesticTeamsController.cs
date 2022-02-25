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
    public class DomesticTeamsController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public DomesticTeamsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

       

        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEAMS_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }


        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            DomesticTeamsModel domesticTeams = new DomesticTeamsModel();
            fill_list(domesticTeams);
            return View(domesticTeams);
        }

        //team_id,team_name,country_name,team_rank,no_of_bowlers,no_of_batsmans,no_of_wins,no_of_loses,no_of_draws,cap_id,wicketkeeper_id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, DomesticTeamsModel domesticTeams)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_TEAMS_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticTeams.team_id);
                    cmd.Parameters.AddWithValue("team_name", domesticTeams.team_name);
                    cmd.Parameters.AddWithValue("country_name", domesticTeams.country_name);
                    cmd.Parameters.AddWithValue("team_rank", domesticTeams.team_rank);
                    cmd.Parameters.AddWithValue("no_of_bowlers", domesticTeams.no_of_bowlers);
                    cmd.Parameters.AddWithValue("no_of_batsmans", domesticTeams.no_of_batsmans);
                    cmd.Parameters.AddWithValue("no_of_wins", domesticTeams.no_of_wins);
                    cmd.Parameters.AddWithValue("no_of_loses", domesticTeams.no_of_loses);
                    cmd.Parameters.AddWithValue("no_of_draws", domesticTeams.no_of_draws);
                    cmd.Parameters.AddWithValue("cap_id", domesticTeams.cap_id);
                    cmd.Parameters.AddWithValue("wicketkeeper_id", domesticTeams.wk_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEAMS_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticTeams);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", domesticTeams) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            DomesticTeamsModel domesticTeams = new DomesticTeamsModel();
            if (id > 0)
            {
                domesticTeams = fetch(id);
                fill_list(domesticTeams);
            }
            return View(domesticTeams);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DomesticTeamsModel domesticTeams)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_TEAMS_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticTeams.team_id);
                    cmd.Parameters.AddWithValue("team_name", domesticTeams.team_name);
                    cmd.Parameters.AddWithValue("country_name", domesticTeams.country_name);
                    cmd.Parameters.AddWithValue("team_rank", domesticTeams.team_rank);
                    cmd.Parameters.AddWithValue("no_of_bowlers", domesticTeams.no_of_bowlers);
                    cmd.Parameters.AddWithValue("no_of_batsmans", domesticTeams.no_of_batsmans);
                    cmd.Parameters.AddWithValue("no_of_wins", domesticTeams.no_of_wins);
                    cmd.Parameters.AddWithValue("no_of_loses", domesticTeams.no_of_loses);
                    cmd.Parameters.AddWithValue("no_of_draws", domesticTeams.no_of_draws);
                    cmd.Parameters.AddWithValue("cap_id", domesticTeams.cap_id);
                    cmd.Parameters.AddWithValue("wicketkeeper_id", domesticTeams.wk_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEAMS_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticTeams);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", domesticTeams) });
        }










        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            DomesticTeamsModel domesticTeams = fetch(id);

            return View(domesticTeams);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DOMESTIC_TEAMS_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEAMS_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public DomesticTeamsModel fetch(int? id)
        {
            DomesticTeamsModel domesticTeams = new DomesticTeamsModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("DOMESTIC_TEAMS_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    domesticTeams.team_id = Convert.ToInt32(id);
                    domesticTeams.team_name= tbl.Rows[0]["team_name"].ToString();
                    domesticTeams.country_name = tbl.Rows[0]["country_name"].ToString();
                    domesticTeams.team_rank = Convert.ToInt32(tbl.Rows[0]["team_rank"].ToString());
                    if (tbl.Rows[0]["no_of_bowlers"].ToString() != "")
                        domesticTeams.no_of_bowlers = Convert.ToInt32(tbl.Rows[0]["no_of_bowlers"].ToString());
                    if (tbl.Rows[0]["no_of_batsmans"].ToString() != "")
                        domesticTeams.no_of_batsmans = Convert.ToInt32(tbl.Rows[0]["no_of_batsmans"].ToString());
                    if (tbl.Rows[0]["no_of_wins"].ToString() != "")
                        domesticTeams.no_of_wins = Convert.ToInt32(tbl.Rows[0]["no_of_wins"].ToString());
                    if (tbl.Rows[0]["no_of_loses"].ToString() != "")
                        domesticTeams.no_of_loses = Convert.ToInt32(tbl.Rows[0]["no_of_loses"].ToString());
                    if (tbl.Rows[0]["no_of_draws"].ToString() != "")
                        domesticTeams.no_of_draws = Convert.ToInt32(tbl.Rows[0]["no_of_draws"].ToString());
                    
                    if(tbl.Rows[0]["cap_id"].ToString() != "")
                        domesticTeams.cap_id = Convert.ToInt32(tbl.Rows[0]["cap_id"].ToString());
                    if (tbl.Rows[0]["wicketkeeper_id"].ToString() != "")
                        domesticTeams.wk_id = Convert.ToInt32(tbl.Rows[0]["wicketkeeper_id"].ToString());
                }
                return domesticTeams;
            }

        }


        [NonAction]

        public void fill_list(DomesticTeamsModel domestic)
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
                domestic.Captains = l1;

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
                domestic.Wicket_keepers = l2;
                con.Close();
            }

        }
    }
}
