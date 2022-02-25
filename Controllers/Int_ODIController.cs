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
    public class Int_ODIController : Controller
    {
        private readonly IConfiguration _configuration;

        public Int_ODIController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }




        // GET: Umpires
        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }


        public IActionResult Player(int id)
        {
            Int_ODIModel rcrd = fetch(id);

            return View(rcrd);
        }
        // GET: Umpires/Add/
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             Int_ODIModel int_ODI = new Int_ODIModel();
            if (id != null)
                int_ODI.ODI_id = (int)id;
            return View(int_ODI);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, [Bind("ODI_id,ODI_bat_rank,fifties,style,runs,hundreds,fours,sixes,average,ODI_bow_rank,best_figure,runs_conceded,wickets,fiveWick,econymy")] Int_ODIModel int_ODIModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INT_ODI_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", int_ODIModel.ODI_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", int_ODIModel.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", int_ODIModel.fifties);
                    cmd.Parameters.AddWithValue("style", int_ODIModel.style);
                    cmd.Parameters.AddWithValue("runs", int_ODIModel.runs);
                    cmd.Parameters.AddWithValue("hundreds", int_ODIModel.hundreds);
                    cmd.Parameters.AddWithValue("fours", int_ODIModel.fours);
                    cmd.Parameters.AddWithValue("sixes", int_ODIModel.sixes);
                    cmd.Parameters.AddWithValue("average", int_ODIModel.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", int_ODIModel.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", int_ODIModel.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", int_ODIModel.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", int_ODIModel.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", int_ODIModel.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", int_ODIModel.econymy);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", int_ODIModel) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Int_ODIModel int_ODI = new Int_ODIModel();
            if (id > 0)
            {
                int_ODI = fetch(id);
            }
            return View(int_ODI);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ODI_id,ODI_bat_rank,fifties,style,runs,hundreds,fours,sixes,average,ODI_bow_rank,best_figure,runs_conceded,wickets,fiveWick,econymy")] Int_ODIModel int_ODIModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INT_ODI_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", int_ODIModel.ODI_id);
                    cmd.Parameters.AddWithValue("ODI_bat_rank", int_ODIModel.ODI_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", int_ODIModel.fifties);
                    cmd.Parameters.AddWithValue("style", int_ODIModel.style);
                    cmd.Parameters.AddWithValue("runs", int_ODIModel.runs);
                    cmd.Parameters.AddWithValue("hundreds", int_ODIModel.hundreds);
                    cmd.Parameters.AddWithValue("fours", int_ODIModel.fours);
                    cmd.Parameters.AddWithValue("sixes", int_ODIModel.sixes);
                    cmd.Parameters.AddWithValue("average", int_ODIModel.average);
                    cmd.Parameters.AddWithValue("ODI_bow_rank", int_ODIModel.ODI_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", int_ODIModel.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", int_ODIModel.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", int_ODIModel.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", int_ODIModel.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", int_ODIModel.econymy);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_ODI_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", int_ODIModel) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Int_ODIModel int_ODI = fetch(id);

            return View(int_ODI);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INT_ODI_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_ODI_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public Int_ODIModel fetch(int? id)
        {
            Int_ODIModel int_ODI = new Int_ODIModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("INT_ODI_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    int_ODI.ODI_id= Convert.ToInt32(id);
                    int_ODI.ODI_bat_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bat_rank"].ToString());
                    int_ODI.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    int_ODI.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    int_ODI.style = tbl.Rows[0]["style"].ToString();
                    int_ODI.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    int_ODI.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    int_ODI.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    int_ODI.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    int_ODI.ODI_bow_rank = Convert.ToInt32(tbl.Rows[0]["ODI_bow_rank"].ToString());
                    int_ODI.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    int_ODI.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    int_ODI.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    int_ODI.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    int_ODI.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                }
                return int_ODI;
            }

        }
    }
}
