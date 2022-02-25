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
    public class RegionODIController : Controller
    {
        private readonly IConfiguration _configuration;

        public RegionODIController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            RegionODIModel rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             RegionODIModel regionODI = new RegionODIModel();
            fill_list(regionODI);
            return View(regionODI);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, RegionODIModel regionODI)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_ODI_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionODI.region_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", regionODI.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", regionODI.fifties);
                    cmd.Parameters.AddWithValue("style", regionODI.style);
                    cmd.Parameters.AddWithValue("runs", regionODI.runs);
                    cmd.Parameters.AddWithValue("hundreds", regionODI.hundreds);
                    cmd.Parameters.AddWithValue("fours", regionODI.fours);
                    cmd.Parameters.AddWithValue("sixes", regionODI.sixes);
                    cmd.Parameters.AddWithValue("average", regionODI.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", regionODI.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", regionODI.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", regionODI.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", regionODI.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", regionODI.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", regionODI.econymy);
                    cmd.Parameters.AddWithValue("team_id", regionODI.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionODI);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", regionODI) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            RegionODIModel regionODI = new RegionODIModel();
            if (id > 0)
            {
                regionODI = fetch(id);
            }
            fill_list(regionODI);
            return View(regionODI);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  RegionODIModel regionODI)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_ODI_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionODI.region_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", regionODI.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", regionODI.fifties);
                    cmd.Parameters.AddWithValue("style", regionODI.style);
                    cmd.Parameters.AddWithValue("runs", regionODI.runs);
                    cmd.Parameters.AddWithValue("hundreds", regionODI.hundreds);
                    cmd.Parameters.AddWithValue("fours", regionODI.fours);
                    cmd.Parameters.AddWithValue("sixes", regionODI.sixes);
                    cmd.Parameters.AddWithValue("average", regionODI.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", regionODI.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", regionODI.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", regionODI.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", regionODI.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", regionODI.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", regionODI.econymy);
                    cmd.Parameters.AddWithValue("team_id", regionODI.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionODI);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", regionODI) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            RegionODIModel regionODI = fetch(id);

            return View(regionODI);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("REGION_ODI_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public RegionODIModel fetch(int? id)
        {
            RegionODIModel regionODI = new RegionODIModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("REGION_ODI_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    regionODI.region_id= Convert.ToInt32(id);
                    regionODI.ODI_bat_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bat_rank"].ToString());
                    regionODI.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    regionODI.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    regionODI.style = tbl.Rows[0]["style"].ToString();
                    regionODI.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    regionODI.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    regionODI.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    regionODI.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    regionODI.ODI_bow_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bow_rank"].ToString());
                    regionODI.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    regionODI.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    regionODI.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    regionODI.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    regionODI.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    regionODI.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return regionODI;
            }

        }



        [NonAction]

        public void fill_list(RegionODIModel regionODI)
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
                regionODI.Teams = l1;
                con.Close();
            }

        }
    }
}
