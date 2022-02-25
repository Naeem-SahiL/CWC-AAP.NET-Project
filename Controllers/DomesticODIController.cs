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
    public class DomesticODIController : Controller
    {
        private readonly IConfiguration _configuration;

        public DomesticODIController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            DomesticODIModel rcrd = fetch(id);

            return View(rcrd);
        }

        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             DomesticODIModel domesticODI = new DomesticODIModel();
            fill_list(domesticODI);
            return View(domesticODI);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, DomesticODIModel domesticODI)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_ODI_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticODI.domestic_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", domesticODI.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", domesticODI.fifties);
                    cmd.Parameters.AddWithValue("style", domesticODI.style);
                    cmd.Parameters.AddWithValue("runs", domesticODI.runs);
                    cmd.Parameters.AddWithValue("hundreds", domesticODI.hundreds);
                    cmd.Parameters.AddWithValue("fours", domesticODI.fours);
                    cmd.Parameters.AddWithValue("sixes", domesticODI.sixes);
                    cmd.Parameters.AddWithValue("average", domesticODI.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", domesticODI.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", domesticODI.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", domesticODI.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", domesticODI.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", domesticODI.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", domesticODI.econymy);
                    cmd.Parameters.AddWithValue("team_id", domesticODI.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticODI);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", domesticODI) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            DomesticODIModel domesticODI = new DomesticODIModel();
            if (id > 0)
            {
                domesticODI = fetch(id);
            }
            fill_list(domesticODI);
            return View(domesticODI);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  DomesticODIModel domesticODI)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DOMESTIC_ODI_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", domesticODI.domestic_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", domesticODI.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", domesticODI.fifties);
                    cmd.Parameters.AddWithValue("style", domesticODI.style);
                    cmd.Parameters.AddWithValue("runs", domesticODI.runs);
                    cmd.Parameters.AddWithValue("hundreds", domesticODI.hundreds);
                    cmd.Parameters.AddWithValue("fours", domesticODI.fours);
                    cmd.Parameters.AddWithValue("sixes", domesticODI.sixes);
                    cmd.Parameters.AddWithValue("average", domesticODI.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", domesticODI.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", domesticODI.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", domesticODI.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", domesticODI.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", domesticODI.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", domesticODI.econymy);
                    cmd.Parameters.AddWithValue("team_id", domesticODI.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            fill_list(domesticODI);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", domesticODI) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            DomesticODIModel domesticODI = fetch(id);

            return View(domesticODI);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DOMESTIC_ODI_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from DOMESTIC_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public DomesticODIModel fetch(int? id)
        {
            DomesticODIModel domesticODI = new DomesticODIModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("DOMESTIC_ODI_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    domesticODI.domestic_id= Convert.ToInt32(id);
                    domesticODI.ODI_bat_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bat_rank"].ToString());
                    domesticODI.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    domesticODI.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    domesticODI.style = tbl.Rows[0]["style"].ToString();
                    domesticODI.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    domesticODI.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    domesticODI.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    domesticODI.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    domesticODI.ODI_bow_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bow_rank"].ToString());
                    domesticODI.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    domesticODI.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    domesticODI.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    domesticODI.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    domesticODI.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                    domesticODI.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());

                }
                return domesticODI;
            }

        }



        [NonAction]

        public void fill_list(DomesticODIModel domesticODI)
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
                domesticODI.Teams = l1;
                con.Close();
            }

        }
    }
}
