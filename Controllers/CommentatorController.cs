using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace core.Controllers
{
    public class CommentatorController : Controller
    {
        private readonly IConfiguration _configuration;

        public CommentatorController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from COMMENTATOR_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }



        // GET: Umpires/Add/
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            CommentatorModel commentator = new CommentatorModel();
            return View(commentator);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, [Bind("commentator_id,name,commentator_matches,country")] CommentatorModel commentatorModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("COMMENTATOR_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", commentatorModel.commentator_id);
                    cmd.Parameters.AddWithValue("name", commentatorModel.name);
                    cmd.Parameters.AddWithValue("no_of_matches", commentatorModel.commentator_matches);
                    cmd.Parameters.AddWithValue("cntry", commentatorModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from COMMENTATOR_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", commentatorModel) });
        }






        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            CommentatorModel commentator = new CommentatorModel();
            if (id > 0)
            {
                commentator = fetch(id);
            }
            return View(commentator);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("commentator_id,name,commentator_matches,country")] CommentatorModel commentatorModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("COMMENTATOR_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", commentatorModel.commentator_id);
                    cmd.Parameters.AddWithValue("name", commentatorModel.name);
                    cmd.Parameters.AddWithValue("no_of_matches", commentatorModel.commentator_matches);
                    cmd.Parameters.AddWithValue("cntry", commentatorModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from COMMENTATOR_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", commentatorModel) });
        }










        // GET: Refrees/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            CommentatorModel commentator = fetch(id);

            return View(commentator);
        }

        // POST: Refrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("COMMENTATOR_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from COMMENTATOR_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public CommentatorModel fetch(int? id)
        {
            CommentatorModel commentator = new CommentatorModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("COMMENTATOR_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    commentator.commentator_id = Convert.ToInt32(id);
                    commentator.name = tbl.Rows[0]["name"].ToString();
                    commentator.commentator_matches = Convert.ToInt32(tbl.Rows[0]["commentator_matches"].ToString());
                    commentator.country = tbl.Rows[0]["country"].ToString();
                }
                return commentator;
            }

        }


    }
}
