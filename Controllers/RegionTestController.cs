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
    public class RegionTestController : Controller
    {
        private readonly IConfiguration _configuration;

        public RegionTestController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            RegionTestModel rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             RegionTestModel regionTEST = new RegionTestModel();
            fill_list(regionTEST);
            return View(regionTEST);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, RegionTestModel regionTEST)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_TEST_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionTEST.region_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", regionTEST.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", regionTEST.fifties);
                    cmd.Parameters.AddWithValue("style", regionTEST.style);
                    cmd.Parameters.AddWithValue("runs", regionTEST.runs);
                    cmd.Parameters.AddWithValue("hundreds", regionTEST.hundreds);
                    cmd.Parameters.AddWithValue("fours", regionTEST.fours);
                    cmd.Parameters.AddWithValue("sixes", regionTEST.sixes);
                    cmd.Parameters.AddWithValue("average", regionTEST.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", regionTEST.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", regionTEST.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", regionTEST.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", regionTEST.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", regionTEST.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", regionTEST.econymy);
                    cmd.Parameters.AddWithValue("team_id", regionTEST.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionTEST);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", regionTEST) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            RegionTestModel regionTEST = new RegionTestModel();
            if (id > 0)
            {
                regionTEST = fetch(id);
            }
            fill_list(regionTEST);
            return View(regionTEST);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  RegionTestModel regionTEST)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REGION_TEST_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", regionTEST.region_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", regionTEST.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", regionTEST.fifties);
                    cmd.Parameters.AddWithValue("style", regionTEST.style);
                    cmd.Parameters.AddWithValue("runs", regionTEST.runs);
                    cmd.Parameters.AddWithValue("hundreds", regionTEST.hundreds);
                    cmd.Parameters.AddWithValue("fours", regionTEST.fours);
                    cmd.Parameters.AddWithValue("sixes", regionTEST.sixes);
                    cmd.Parameters.AddWithValue("average", regionTEST.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", regionTEST.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", regionTEST.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", regionTEST.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", regionTEST.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", regionTEST.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", regionTEST.econymy);
                    cmd.Parameters.AddWithValue("team_id", regionTEST.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(regionTEST);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", regionTEST) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            RegionTestModel regionTEST = fetch(id);

            return View(regionTEST);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("REGION_TEST_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from REGION_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public RegionTestModel fetch(int? id)
        {
            RegionTestModel regionTEST = new RegionTestModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("REGION_TEST_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    regionTEST.region_id= Convert.ToInt32(id);
                    regionTEST.test_bat_rank = Convert.ToInt32(tbl.Rows[0]["test_bat_rank"].ToString());
                    regionTEST.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    regionTEST.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    regionTEST.style = tbl.Rows[0]["style"].ToString();
                    regionTEST.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    regionTEST.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    regionTEST.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    regionTEST.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    regionTEST.test_bow_rank = Convert.ToInt32(tbl.Rows[0]["test_bow_rank"].ToString());
                    regionTEST.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    regionTEST.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    regionTEST.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    regionTEST.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    regionTEST.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    regionTEST.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return regionTEST;
            }

        }



        [NonAction]

        public void fill_list(RegionTestModel regionTEST)
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
                regionTEST.Teams = l1;
                con.Close();
            }

        }
    }
}
