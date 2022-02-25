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
    public class ClubODIController : Controller
    {
        private readonly IConfiguration _configuration;

        public ClubODIController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            ClubODIModel rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             ClubODIModel clubODI = new ClubODIModel();
            fill_list(clubODI);
            return View(clubODI);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, ClubODIModel clubODI)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_ODI_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubODI.club_ODI_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", clubODI.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", clubODI.fifties);
                    cmd.Parameters.AddWithValue("style", clubODI.style);
                    cmd.Parameters.AddWithValue("runs", clubODI.runs);
                    cmd.Parameters.AddWithValue("hundreds", clubODI.hundreds);
                    cmd.Parameters.AddWithValue("fours", clubODI.fours);
                    cmd.Parameters.AddWithValue("sixes", clubODI.sixes);
                    cmd.Parameters.AddWithValue("average", clubODI.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", clubODI.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", clubODI.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", clubODI.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", clubODI.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", clubODI.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", clubODI.econymy);
                    cmd.Parameters.AddWithValue("team_id", clubODI.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(clubODI);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", clubODI) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            ClubODIModel clubODI = new ClubODIModel();
            if (id > 0)
            {
                clubODI = fetch(id);
            }
            fill_list(clubODI);
            return View(clubODI);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  ClubODIModel clubODI)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_ODI_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubODI.club_ODI_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", clubODI.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", clubODI.fifties);
                    cmd.Parameters.AddWithValue("style", clubODI.style);
                    cmd.Parameters.AddWithValue("runs", clubODI.runs);
                    cmd.Parameters.AddWithValue("hundreds", clubODI.hundreds);
                    cmd.Parameters.AddWithValue("fours", clubODI.fours);
                    cmd.Parameters.AddWithValue("sixes", clubODI.sixes);
                    cmd.Parameters.AddWithValue("average", clubODI.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", clubODI.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", clubODI.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", clubODI.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", clubODI.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", clubODI.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", clubODI.econymy);
                    cmd.Parameters.AddWithValue("team_id", clubODI.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(clubODI);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", clubODI) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            ClubODIModel clubODI = fetch(id);

            return View(clubODI);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CLUB_ODI_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public ClubODIModel fetch(int? id)
        {
            ClubODIModel clubODI = new ClubODIModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("CLUB_ODI_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    clubODI.club_ODI_id= Convert.ToInt32(id);
                    clubODI.ODI_bat_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bat_rank"].ToString());
                    clubODI.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    clubODI.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    clubODI.style = tbl.Rows[0]["style"].ToString();
                    clubODI.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    clubODI.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    clubODI.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    clubODI.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    clubODI.ODI_bow_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bow_rank"].ToString());
                    clubODI.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    clubODI.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    clubODI.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    clubODI.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    clubODI.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    clubODI.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return clubODI;
            }

        }



        [NonAction]

        public void fill_list(ClubODIModel clubODI)
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
                clubODI.Teams = l1;
                con.Close();
            }

        }
    }
}
