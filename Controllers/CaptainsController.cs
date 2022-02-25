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
    public class CaptainsController : Controller
    {

        private readonly IConfiguration _configuration;

        public CaptainsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CAPTAIN_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        [NoDirectAccess]
        public IActionResult Create()
        {
            CaptainModel captain= new CaptainModel();
            return View(captain);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("cap_id,cap_name,years_of_captaincy,total_wins,total_loses")] CaptainModel captainModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CAPTAIN_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", captainModel.cap_id);
                    cmd.Parameters.AddWithValue("name", captainModel.cap_name);
                    cmd.Parameters.AddWithValue("no_of_YRS", captainModel.years_of_captaincy);
                    cmd.Parameters.AddWithValue("wins", captainModel.total_wins);
                    cmd.Parameters.AddWithValue("los", captainModel.total_loses);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CAPTAIN_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Create", captainModel) });
        }


        [NoDirectAccess]
        public IActionResult Edit(int? id)
        {

            CaptainModel captain = new CaptainModel();
            if (id > 0)
            {
                captain = fetch(id);
            }
            return View(captain);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("cap_id,cap_name,years_of_captaincy,total_wins,total_loses")] CaptainModel captainModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CAPTAIN_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", captainModel.cap_id);
                    cmd.Parameters.AddWithValue("name", captainModel.cap_name);
                    cmd.Parameters.AddWithValue("no_of_YRS", captainModel.years_of_captaincy);
                    cmd.Parameters.AddWithValue("wins", captainModel.total_wins);
                    cmd.Parameters.AddWithValue("los", captainModel.total_loses);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CAPTAIN_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", captainModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            CaptainModel captain = fetch(id);

            return View(captain);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE_CAPTAIN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CAPTAIN_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public CaptainModel fetch(int? id)
        {
            CaptainModel cap = new CaptainModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("CAPTAIN_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    cap.cap_id = Convert.ToInt32(id);
                    cap.cap_name = tbl.Rows[0]["cap_name"].ToString();
                    cap.years_of_captaincy = Convert.ToInt32(tbl.Rows[0]["years_of_captaincy"].ToString());
                    cap.total_wins = Convert.ToInt32(tbl.Rows[0]["total_wins"].ToString());
                    cap.total_loses = Convert.ToInt32(tbl.Rows[0]["total_loses"].ToString());
                }
                return cap;
            }

        }
    }
}
