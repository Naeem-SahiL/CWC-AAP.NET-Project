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
    public class ClubController : Controller
    {

        private readonly IConfiguration _configuration;

        public ClubController(IConfiguration configuration)
        {
            this._configuration = configuration; 
        }


        public IActionResult Index()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }

        [NoDirectAccess]
        public IActionResult Create()
        {
            ClubViewModel club= new ClubViewModel();
            return View(club);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, ClubViewModel clubViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubViewModel.club_id);
                    cmd.Parameters.AddWithValue("name", clubViewModel.club_name);
                    cmd.Parameters.AddWithValue("cntry", clubViewModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Create", clubViewModel) });
        }


        [NoDirectAccess]
        public IActionResult Edit(int? id)
        {

            ClubViewModel club = new ClubViewModel();
            if (id > 0)
            {
                club = fetch(id);
            }
            return View(club);
        }

        [HttpPost]
        public IActionResult Edit(int id, ClubViewModel clubViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CLUB_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", clubViewModel.club_id);
                    cmd.Parameters.AddWithValue("name", clubViewModel.club_name);
                    cmd.Parameters.AddWithValue("cntry", clubViewModel.country);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", clubViewModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            ClubViewModel captain = fetch(id);

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
                SqlCommand cmd = new SqlCommand("CLUB_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from CLUB_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public ClubViewModel fetch(int? id)
        {
            ClubViewModel club = new ClubViewModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("CLUB_FETCH_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    club.club_id = Convert.ToInt32(id);
                    club.club_name = tbl.Rows[0]["club_name"].ToString();
                    club.country = tbl.Rows[0]["country"].ToString();
                }
                return club;
            }

        }
    }
}
