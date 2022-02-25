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
    public class RefreesController : Controller
    {
        private readonly IConfiguration _configuration;

        public RefreesController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from RFREES_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

         public IActionResult Refree(int id)
        {
            RefreesViewModel refrees = fetch(id);
            return View(refrees);
        }



        // GET: Umpires/Add/
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
             RefreesViewModel refree = new RefreesViewModel();
            return View(refree);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, [Bind("refree_id,refree_name,refered_matches,country")] RefreesViewModel refreesViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REFREE_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", refreesViewModel.refree_id);
                    cmd.Parameters.AddWithValue("name", refreesViewModel.refree_name);
                    cmd.Parameters.AddWithValue("no_of_matches", refreesViewModel.refered_matches);
                    cmd.Parameters.AddWithValue("cntry", refreesViewModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from RFREES_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", refreesViewModel) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            RefreesViewModel refree = new RefreesViewModel();
            if (id > 0)
            {
                refree = fetch(id);
            }
            return View(refree);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("refree_id,refree_name,refered_matches,country")] RefreesViewModel refreesViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("REFREE_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", refreesViewModel.refree_id);
                    cmd.Parameters.AddWithValue("name", refreesViewModel.refree_name);
                    cmd.Parameters.AddWithValue("no_of_matches", refreesViewModel.refered_matches);
                    cmd.Parameters.AddWithValue("cntry", refreesViewModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from RFREES_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", refreesViewModel) });
        }










        // GET: Refrees/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            RefreesViewModel refree= fetch(id);

            return View(refree);
        }

        // POST: Refrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("REFREE_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from RFREES_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public RefreesViewModel fetch(int? id)
        {
            RefreesViewModel refree = new RefreesViewModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("REFREE_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    refree.refree_id = Convert.ToInt32(id);
                    refree.refree_name = tbl.Rows[0]["refree_name"].ToString();
                    refree.refered_matches = Convert.ToInt32(tbl.Rows[0]["refered_matches"].ToString());
                    refree.country = tbl.Rows[0]["country"].ToString();
                }
                return refree;
            }

        }


    }
}
