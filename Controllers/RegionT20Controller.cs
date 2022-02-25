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
    public class RegionT20Controller : Controller
    {
        private readonly IConfiguration _configuration;

        public RegionT20Controller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            RegionT20Model rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             RegionT20Model regionT20 = new RegionT20Model();
            fill_list(regionT20);
            return View(regionT20);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, RegionT20Model regionT20)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_T20_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionT20.region_id);
                    cmd.Parameters.AddWithValue("T_20_bat_rank", regionT20.T_20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", regionT20.fifties);
                    cmd.Parameters.AddWithValue("style", regionT20.style);
                    cmd.Parameters.AddWithValue("runs", regionT20.runs);
                    cmd.Parameters.AddWithValue("hundreds", regionT20.hundreds);
                    cmd.Parameters.AddWithValue("fours", regionT20.fours);
                    cmd.Parameters.AddWithValue("sixes", regionT20.sixes);
                    cmd.Parameters.AddWithValue("average", regionT20.average);
                    cmd.Parameters.AddWithValue("T_20_bow_rank", regionT20.T_20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", regionT20.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", regionT20.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", regionT20.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", regionT20.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", regionT20.econymy);
                    cmd.Parameters.AddWithValue("team_id", regionT20.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionT20);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", regionT20) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            RegionT20Model regionT20 = new RegionT20Model();
            if (id > 0)
            {
                regionT20 = fetch(id);
            }
            fill_list(regionT20);
            return View(regionT20);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  RegionT20Model regionT20)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_T20_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionT20.region_id);
                    cmd.Parameters.AddWithValue("T_20_bat_rank", regionT20.T_20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", regionT20.fifties);
                    cmd.Parameters.AddWithValue("style", regionT20.style);
                    cmd.Parameters.AddWithValue("runs", regionT20.runs);
                    cmd.Parameters.AddWithValue("hundreds", regionT20.hundreds);
                    cmd.Parameters.AddWithValue("fours", regionT20.fours);
                    cmd.Parameters.AddWithValue("sixes", regionT20.sixes);
                    cmd.Parameters.AddWithValue("average", regionT20.average);
                    cmd.Parameters.AddWithValue("T_20_bow_rank", regionT20.T_20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", regionT20.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", regionT20.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", regionT20.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", regionT20.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", regionT20.econymy);
                    cmd.Parameters.AddWithValue("team_id", regionT20.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionT20);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", regionT20) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            RegionT20Model regionT20 = fetch(id);

            return View(regionT20);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("REGION_T20_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public RegionT20Model fetch(int? id)
        {
            RegionT20Model regionT20 = new RegionT20Model();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("REGION_T20_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    regionT20.region_id= Convert.ToInt32(id);
                    regionT20.T_20_bat_rank = Convert.ToInt32(tbl.Rows[0]["T_20_bat_rank"].ToString());
                    regionT20.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    regionT20.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    regionT20.style = tbl.Rows[0]["style"].ToString();
                    regionT20.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    regionT20.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    regionT20.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    regionT20.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    regionT20.T_20_bow_rank = Convert.ToInt32(tbl.Rows[0]["T_20_bow_rank"].ToString());
                    regionT20.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    regionT20.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    regionT20.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    regionT20.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    regionT20.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    regionT20.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return regionT20;
            }

        }



        [NonAction]

        public void fill_list(RegionT20Model regionT20)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select team_id,team_name from regionteams", con);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                List<RegionTeamsModel> l1 = new List<RegionTeamsModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    RegionTeamsModel Teams = new RegionTeamsModel();
                    Teams.team_id = Convert.ToInt32(tbl.Rows[i]["team_id"].ToString());
                    Teams.team_name = tbl.Rows[i]["team_name"].ToString();
                    l1.Add(Teams);
                }
                regionT20.Teams = l1;
                con.Close();
            }

        }
    }
}
