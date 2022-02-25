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
    public class Int_T20Controller : Controller
    {
        private readonly IConfiguration _configuration;

        public Int_T20Controller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }




       
        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            Int_T20Model rcrd = fetch(id);
           
            return View(rcrd);
        }

        // GET: Umpires/Add/
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            Int_T20Model int_T20 = new Int_T20Model();
            if (id != null)
                int_T20.T20_id = (int)id;
            return View(int_T20);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, [Bind("T20_id,T_20_bat_rank,fifties,style,runs,hundreds,fours,sixes,average,T_20_bow_rank,best_figure,runs_conceded,wickets,fiveWick,econymy")] Int_T20Model int_T20Model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INT_T20_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", int_T20Model.T20_id);
                    cmd.Parameters.AddWithValue("T_20_bat_rank", int_T20Model.T_20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", int_T20Model.fifties);
                    cmd.Parameters.AddWithValue("style", int_T20Model.style);
                    cmd.Parameters.AddWithValue("runs", int_T20Model.runs);
                    cmd.Parameters.AddWithValue("hundreds", int_T20Model.hundreds);
                    cmd.Parameters.AddWithValue("fours", int_T20Model.fours);
                    cmd.Parameters.AddWithValue("sixes", int_T20Model.sixes);
                    cmd.Parameters.AddWithValue("average", int_T20Model.average);
                    cmd.Parameters.AddWithValue("T_20_bow_rank", int_T20Model.T_20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", int_T20Model.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", int_T20Model.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", int_T20Model.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", int_T20Model.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", int_T20Model.econymy);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", int_T20Model) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Int_T20Model int_T20 = new Int_T20Model();
            if (id > 0)
            {
                int_T20 = fetch(id);
            }
            return View(int_T20);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("T20_id,T_20_bat_rank,fifties,style,runs,hundreds,fours,sixes,average,T_20_bow_rank,best_figure,runs_conceded,wickets,fiveWick,econymy")] Int_T20Model int_T20Model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INT_T20_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", int_T20Model.T20_id);
                    cmd.Parameters.AddWithValue("T_20_bat_rank", int_T20Model.T_20_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", int_T20Model.fifties);
                    cmd.Parameters.AddWithValue("style", int_T20Model.style);
                    cmd.Parameters.AddWithValue("runs", int_T20Model.runs);
                    cmd.Parameters.AddWithValue("hundreds", int_T20Model.hundreds);
                    cmd.Parameters.AddWithValue("fours", int_T20Model.fours);
                    cmd.Parameters.AddWithValue("sixes", int_T20Model.sixes);
                    cmd.Parameters.AddWithValue("average", int_T20Model.average);
                    cmd.Parameters.AddWithValue("T_20_bow_rank", int_T20Model.T_20_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", int_T20Model.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", int_T20Model.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", int_T20Model.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", int_T20Model.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", int_T20Model.econymy);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_T20_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", int_T20Model) });
        }




        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Int_T20Model int_T20 = fetch(id);

            return View(int_T20);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INT_T20_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_T20_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public Int_T20Model fetch(int? id)
        {
            Int_T20Model int_T20 = new Int_T20Model();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("INT_T20_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    int_T20.T20_id = Convert.ToInt32(id);
                    int_T20.T_20_bat_rank= Convert.ToInt32(tbl.Rows[0]["T_20_bat_rank"].ToString());
                    int_T20.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    int_T20.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    int_T20.style = tbl.Rows[0]["style"].ToString();
                    int_T20.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    int_T20.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    int_T20.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    int_T20.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    int_T20.T_20_bow_rank = Convert.ToInt32(tbl.Rows[0]["T_20_bow_rank"].ToString());
                    int_T20.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    int_T20.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    int_T20.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    int_T20.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    int_T20.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                }
                return int_T20;
            }

        }
    }
}
