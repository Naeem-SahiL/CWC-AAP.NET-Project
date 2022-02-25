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
    public class DomesticT20Controller : Controller
    {
        private readonly IConfiguration _configuration;

        public DomesticT20Controller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            DomesticT20Model rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             DomesticT20Model domesticT20 = new DomesticT20Model();
            fill_list(domesticT20);
            return View(domesticT20);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, DomesticT20Model domesticT20)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_T20_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticT20.domestic_id);
                    cmd.Parameters.AddWithValue("T_20_bat_rank", domesticT20.T_20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", domesticT20.fifties);
                    cmd.Parameters.AddWithValue("style", domesticT20.style);
                    cmd.Parameters.AddWithValue("runs", domesticT20.runs);
                    cmd.Parameters.AddWithValue("hundreds", domesticT20.hundreds);
                    cmd.Parameters.AddWithValue("fours", domesticT20.fours);
                    cmd.Parameters.AddWithValue("sixes", domesticT20.sixes);
                    cmd.Parameters.AddWithValue("average", domesticT20.average);
                    cmd.Parameters.AddWithValue("T_20_bow_rank", domesticT20.T_20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", domesticT20.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", domesticT20.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", domesticT20.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", domesticT20.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", domesticT20.econymy);
                    cmd.Parameters.AddWithValue("team_id", domesticT20.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticT20);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", domesticT20) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            DomesticT20Model domesticT20 = new DomesticT20Model();
            if (id > 0)
            {
                domesticT20 = fetch(id);
            }
            fill_list(domesticT20);
            return View(domesticT20);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  DomesticT20Model domesticT20)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_T20_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticT20.domestic_id);
                    cmd.Parameters.AddWithValue("T_20_bat_rank", domesticT20.T_20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", domesticT20.fifties);
                    cmd.Parameters.AddWithValue("style", domesticT20.style);
                    cmd.Parameters.AddWithValue("runs", domesticT20.runs);
                    cmd.Parameters.AddWithValue("hundreds", domesticT20.hundreds);
                    cmd.Parameters.AddWithValue("fours", domesticT20.fours);
                    cmd.Parameters.AddWithValue("sixes", domesticT20.sixes);
                    cmd.Parameters.AddWithValue("average", domesticT20.average);
                    cmd.Parameters.AddWithValue("T_20_bow_rank", domesticT20.T_20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", domesticT20.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", domesticT20.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", domesticT20.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", domesticT20.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", domesticT20.econymy);
                    cmd.Parameters.AddWithValue("team_id", domesticT20.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticT20);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", domesticT20) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            DomesticT20Model domesticT20 = fetch(id);

            return View(domesticT20);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DOMESTIC_T20_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public DomesticT20Model fetch(int? id)
        {
            DomesticT20Model domesticT20 = new DomesticT20Model();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("DOMESTIC_T20_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    domesticT20.domestic_id= Convert.ToInt32(id);
                    domesticT20.T_20_bat_rank = Convert.ToInt32(tbl.Rows[0]["T_20_bat_rank"].ToString());
                    domesticT20.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    domesticT20.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    domesticT20.style = tbl.Rows[0]["style"].ToString();
                    domesticT20.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    domesticT20.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    domesticT20.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    domesticT20.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    domesticT20.T_20_bow_rank = Convert.ToInt32(tbl.Rows[0]["T_20_bow_rank"].ToString());
                    domesticT20.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    domesticT20.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    domesticT20.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    domesticT20.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    domesticT20.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    domesticT20.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return domesticT20;
            }

        }



        [NonAction]

        public void fill_list(DomesticT20Model domesticT20)
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
                domesticT20.Teams = l1;
                con.Close();
            }

        }
    }
}
