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
    public class StadiumController : Controller
    {

        private readonly IConfiguration _configuration;

        public StadiumController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from STADIUM_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }
        public IActionResult Stadium(int id)
        {
            SadiumModel sadium = fetch(id);
            return View(sadium);
        }

        [NoDirectAccess]
        public IActionResult Add()
        {
            SadiumModel sadiumModel = new SadiumModel();
            return View(sadiumModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, SadiumModel sadiumModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("STADIUM_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", sadiumModel.stad_id);
                    cmd.Parameters.AddWithValue("stad_name", sadiumModel.stad_name);
                    cmd.Parameters.AddWithValue("country", sadiumModel.country);
                    cmd.Parameters.AddWithValue("place", sadiumModel.place);
                    cmd.Parameters.AddWithValue("capacity", sadiumModel.capacity);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from STADIUM_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", sadiumModel) });
        }


        [NoDirectAccess]
        public IActionResult Edit(int? id)
        {

            SadiumModel sadium = new SadiumModel();
            if (id > 0)
            {
                sadium = fetch(id);
            }
            return View(sadium);
        }

        [HttpPost]
        public IActionResult Edit(int id, SadiumModel sadiumModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("STADIUM_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("stad_id", sadiumModel.stad_id);
                    cmd.Parameters.AddWithValue("stad_name", sadiumModel.stad_name);
                    cmd.Parameters.AddWithValue("country", sadiumModel.country);
                    cmd.Parameters.AddWithValue("place", sadiumModel.place);
                    cmd.Parameters.AddWithValue("capacity", sadiumModel.capacity);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from STADIUM_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", sadiumModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            SadiumModel sadium = fetch(id);

            return View(sadium);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("STADIUM_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from STADIUM_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public SadiumModel fetch(int? id)
        {
            SadiumModel sadium = new SadiumModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("STADIUM_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    sadium.stad_id = Convert.ToInt32(id);
                    sadium.stad_name = tbl.Rows[0]["stad_name"].ToString();
                    sadium.country = tbl.Rows[0]["country"].ToString();
                    sadium.place = tbl.Rows[0]["place"].ToString();
                    sadium.capacity = Convert.ToInt32(tbl.Rows[0]["capacity"].ToString());
                }
                return sadium;
            }

        }
    }
}
