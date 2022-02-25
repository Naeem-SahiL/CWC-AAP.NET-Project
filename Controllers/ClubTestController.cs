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
    public class ClubTestController : Controller
    {
        private readonly IConfiguration _configuration;

        public ClubTestController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            ClubTestModel rcrd = fetch(id);

            return View(rcrd);
        }


        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             ClubTestModel clubTest = new ClubTestModel();
            fill_list(clubTest);
            return View(clubTest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, ClubTestModel clubTest)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_TEST_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubTest.club_test_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", clubTest.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", clubTest.fifties);
                    cmd.Parameters.AddWithValue("style", clubTest.style);
                    cmd.Parameters.AddWithValue("runs", clubTest.runs);
                    cmd.Parameters.AddWithValue("hundreds", clubTest.hundreds);
                    cmd.Parameters.AddWithValue("fours", clubTest.fours);
                    cmd.Parameters.AddWithValue("sixes", clubTest.sixes);
                    cmd.Parameters.AddWithValue("average", clubTest.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", clubTest.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", clubTest.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", clubTest.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", clubTest.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", clubTest.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", clubTest.econymy);
                    cmd.Parameters.AddWithValue("team_id", clubTest.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(clubTest);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", clubTest) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            ClubTestModel clubTest = new ClubTestModel();
            if (id > 0)
            {
                clubTest = fetch(id);
            }
            fill_list(clubTest);
            return View(clubTest);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  ClubTestModel clubTest)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_TEST_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubTest.club_test_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", clubTest.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", clubTest.fifties);
                    cmd.Parameters.AddWithValue("style", clubTest.style);
                    cmd.Parameters.AddWithValue("runs", clubTest.runs);
                    cmd.Parameters.AddWithValue("hundreds", clubTest.hundreds);
                    cmd.Parameters.AddWithValue("fours", clubTest.fours);
                    cmd.Parameters.AddWithValue("sixes", clubTest.sixes);
                    cmd.Parameters.AddWithValue("average", clubTest.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", clubTest.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", clubTest.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", clubTest.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", clubTest.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", clubTest.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", clubTest.econymy);
                    cmd.Parameters.AddWithValue("team_id", clubTest.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(clubTest);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", clubTest) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            ClubTestModel clubTest = fetch(id);

            return View(clubTest);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CLUB_TEST_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public ClubTestModel fetch(int? id)
        {
            ClubTestModel clubTest = new ClubTestModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("CLUB_TEST_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    clubTest.club_test_id= Convert.ToInt32(id);
                    clubTest.test_bat_rank = Convert.ToInt32(tbl.Rows[0]["test_bat_rank"].ToString());
                    clubTest.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    clubTest.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    clubTest.style = tbl.Rows[0]["style"].ToString();
                    clubTest.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    clubTest.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    clubTest.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    clubTest.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    clubTest.test_bow_rank = Convert.ToInt32(tbl.Rows[0]["test_bow_rank"].ToString());
                    clubTest.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    clubTest.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    clubTest.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    clubTest.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    clubTest.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    clubTest.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return clubTest;
            }

        }



        [NonAction]

        public void fill_list(ClubTestModel clubTest)
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
                clubTest.Teams = l1;
                con.Close();
            }

        }
    }
}
