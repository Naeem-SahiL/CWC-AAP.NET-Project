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
    public class WicketKeeperController : Controller
    {
        private readonly IConfiguration _configuration;

        public WicketKeeperController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }




       
        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from WICKET_KEEPER_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }



       
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             WicketKeeperModel wicketKeeper = new WicketKeeperModel();
            return View(wicketKeeper);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id,  WicketKeeperModel wicketKeeperViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("WICKET_K_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", wicketKeeperViewModel.wk_id);
                    cmd.Parameters.AddWithValue("name", wicketKeeperViewModel.wk_name);
                    cmd.Parameters.AddWithValue("catches", wicketKeeperViewModel.no_of_catches);
                    cmd.Parameters.AddWithValue("stumps", wicketKeeperViewModel.no_of_stumps);
                    cmd.Parameters.AddWithValue("rank", wicketKeeperViewModel.wk_rank);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from WICKET_KEEPER_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", wicketKeeperViewModel) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            WicketKeeperModel wicketKeeper = new WicketKeeperModel();
            if (id > 0)
            {
                wicketKeeper = fetch(id);
            }
            return View(wicketKeeper);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("wk_id,wk_name,wk_rank,no_of_catches,no_of_stumps")] WicketKeeperModel wicketKeeperViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("WICKET_K_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", wicketKeeperViewModel.wk_id);
                    cmd.Parameters.AddWithValue("name", wicketKeeperViewModel.wk_name);
                    cmd.Parameters.AddWithValue("catches", wicketKeeperViewModel.no_of_catches);
                    cmd.Parameters.AddWithValue("stumps", wicketKeeperViewModel.no_of_stumps);
                    cmd.Parameters.AddWithValue("rank", wicketKeeperViewModel.wk_rank);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from WICKET_KEEPER_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", wicketKeeperViewModel) });
        }










        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            WicketKeeperModel wicketKeeper = fetch(id);

            return View(wicketKeeper);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("WICKET_K_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from WICKET_KEEPER_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public WicketKeeperModel fetch(int? id)
        {
            WicketKeeperModel wicketKeeper = new WicketKeeperModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("WICKET_K_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    wicketKeeper.wk_id = Convert.ToInt32(id);
                    wicketKeeper.wk_name = tbl.Rows[0]["wk_name"].ToString();
                    wicketKeeper.wk_rank = Convert.ToInt32(tbl.Rows[0]["wk_rank"].ToString());
                    wicketKeeper.no_of_stumps = Convert.ToInt32(tbl.Rows[0]["no_of_stumps"].ToString());
                    wicketKeeper.no_of_catches = Convert.ToInt32(tbl.Rows[0]["no_of_catches"].ToString());
                }
                return wicketKeeper;
            }

        }
    }
}
