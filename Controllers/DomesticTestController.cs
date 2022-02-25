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
    public class DomesticTestController : Controller
    {
        private readonly IConfiguration _configuration;

        public DomesticTestController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            DomesticTestModel rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             DomesticTestModel domesticTest = new DomesticTestModel();
            fill_list(domesticTest);
            return View(domesticTest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, DomesticTestModel domesticTest)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_TEST_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticTest.domestic_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", domesticTest.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", domesticTest.fifties);
                    cmd.Parameters.AddWithValue("style", domesticTest.style);
                    cmd.Parameters.AddWithValue("runs", domesticTest.runs);
                    cmd.Parameters.AddWithValue("hundreds", domesticTest.hundreds);
                    cmd.Parameters.AddWithValue("fours", domesticTest.fours);
                    cmd.Parameters.AddWithValue("sixes", domesticTest.sixes);
                    cmd.Parameters.AddWithValue("average", domesticTest.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", domesticTest.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", domesticTest.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", domesticTest.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", domesticTest.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", domesticTest.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", domesticTest.econymy);
                    cmd.Parameters.AddWithValue("team_id", domesticTest.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticTest);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", domesticTest) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            DomesticTestModel domesticTest = new DomesticTestModel();
            if (id > 0)
            {
                domesticTest = fetch(id);
            }
            fill_list(domesticTest);
            return View(domesticTest);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  DomesticTestModel domesticTest)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_TEST_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticTest.domestic_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", domesticTest.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", domesticTest.fifties);
                    cmd.Parameters.AddWithValue("style", domesticTest.style);
                    cmd.Parameters.AddWithValue("runs", domesticTest.runs);
                    cmd.Parameters.AddWithValue("hundreds", domesticTest.hundreds);
                    cmd.Parameters.AddWithValue("fours", domesticTest.fours);
                    cmd.Parameters.AddWithValue("sixes", domesticTest.sixes);
                    cmd.Parameters.AddWithValue("average", domesticTest.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", domesticTest.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", domesticTest.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", domesticTest.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", domesticTest.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", domesticTest.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", domesticTest.econymy);
                    cmd.Parameters.AddWithValue("team_id", domesticTest.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticTest);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", domesticTest) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            DomesticTestModel domesticTest = fetch(id);

            return View(domesticTest);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DOMESTIC_TEST_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public DomesticTestModel fetch(int? id)
        {
            DomesticTestModel domesticTest = new DomesticTestModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("DOMESTIC_TEST_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    domesticTest.domestic_id= Convert.ToInt32(id);
                    domesticTest.test_bat_rank = Convert.ToInt32(tbl.Rows[0]["test_bat_rank"].ToString());
                    domesticTest.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    domesticTest.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    domesticTest.style = tbl.Rows[0]["style"].ToString();
                    domesticTest.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    domesticTest.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    domesticTest.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    domesticTest.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    domesticTest.test_bow_rank = Convert.ToInt32(tbl.Rows[0]["test_bow_rank"].ToString());
                    domesticTest.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    domesticTest.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    domesticTest.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    domesticTest.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    domesticTest.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    domesticTest.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return domesticTest;
            }

        }



        [NonAction]

        public void fill_list(DomesticTestModel domesticTest)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select team_id,team_name from domesticteams", con);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                List<DomesticTeamsModel> l1 = new List<DomesticTeamsModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    DomesticTeamsModel domesticTeam = new DomesticTeamsModel();
                    domesticTeam.team_id = Convert.ToInt32(tbl.Rows[i]["team_id"].ToString());
                    domesticTeam.team_name = tbl.Rows[i]["team_name"].ToString();
                    l1.Add(domesticTeam);
                }
                domesticTest.Teams = l1;
                con.Close();
            }

        }
    }
}
