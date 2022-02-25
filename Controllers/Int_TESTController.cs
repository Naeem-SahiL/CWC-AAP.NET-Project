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
    public class Int_TESTController : Controller
    {
        private readonly IConfiguration _configuration;

        public Int_TESTController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }





        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        public IActionResult Player(int id)
        {
            Int_TestModel rcrd = fetch(id);

            return View(rcrd);
        }

        // GET: Umpires/Add/
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            Int_TestModel int_Test = new Int_TestModel();
            if (id != null)
                int_Test.Test_id = (int)id;
            return View(int_Test);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, [Bind("Test_id,test_bat_rank,fifties,style,runs,hundreds,fours,sixes,average,test_bow_rank,best_figure,runs_conceded,wickets,fiveWick,econymy")] Int_TestModel int_TestModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INT_TEST_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", int_TestModel.Test_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", int_TestModel.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", int_TestModel.fifties);
                    cmd.Parameters.AddWithValue("style", int_TestModel.style);
                    cmd.Parameters.AddWithValue("runs", int_TestModel.runs);
                    cmd.Parameters.AddWithValue("hundreds", int_TestModel.hundreds);
                    cmd.Parameters.AddWithValue("fours", int_TestModel.fours);
                    cmd.Parameters.AddWithValue("sixes", int_TestModel.sixes);
                    cmd.Parameters.AddWithValue("average", int_TestModel.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", int_TestModel.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", int_TestModel.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", int_TestModel.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", int_TestModel.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", int_TestModel.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", int_TestModel.econymy);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", int_TestModel) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Int_TestModel int_Test = new Int_TestModel();
            if (id > 0)
            {
                int_Test = fetch(id);
            }
            return View(int_Test);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Test_id,test_bat_rank,fifties,style,runs,hundreds,fours,sixes,average,test_bow_rank,best_figure,runs_conceded,wickets,fiveWick,econymy")] Int_TestModel int_TestModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INT_TEST_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", int_TestModel.Test_id);
                    cmd.Parameters.AddWithValue("test_bat_rank", int_TestModel.test_bat_rank);
                    cmd.Parameters.AddWithValue("fifties", int_TestModel.fifties);
                    cmd.Parameters.AddWithValue("style", int_TestModel.style);
                    cmd.Parameters.AddWithValue("runs", int_TestModel.runs);
                    cmd.Parameters.AddWithValue("hundreds", int_TestModel.hundreds);
                    cmd.Parameters.AddWithValue("fours", int_TestModel.fours);
                    cmd.Parameters.AddWithValue("sixes", int_TestModel.sixes);
                    cmd.Parameters.AddWithValue("average", int_TestModel.average);
                    cmd.Parameters.AddWithValue("test_bow_rank", int_TestModel.test_bow_rank);
                    cmd.Parameters.AddWithValue("best_figure", int_TestModel.best_figure);
                    cmd.Parameters.AddWithValue("runs_conceded", int_TestModel.runs_conceded);
                    cmd.Parameters.AddWithValue("wickets", int_TestModel.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", int_TestModel.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", int_TestModel.econymy);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_TEST_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", int_TestModel) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Int_TestModel int_Test = fetch(id);

            return View(int_Test);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INT_TEST_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from INT_TEST_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public Int_TestModel fetch(int? id)
        {
            Int_TestModel int_Test = new Int_TestModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("INT_TEST_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);

                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    int_Test.Test_id = Convert.ToInt32(id);
                    int_Test.test_bat_rank = Convert.ToInt32(tbl.Rows[0]["test_bat_rank"].ToString());
                    int_Test.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    int_Test.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    int_Test.style = tbl.Rows[0]["style"].ToString();
                    int_Test.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    int_Test.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    int_Test.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    int_Test.average = float.Parse(tbl.Rows[0]["average"].ToString());
                    int_Test.test_bow_rank = Convert.ToInt32(tbl.Rows[0]["test_bow_rank"].ToString());
                    int_Test.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    int_Test.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    int_Test.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    int_Test.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    int_Test.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                }
                return int_Test;
            }

        }
    }
}
