using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core.Data;
using core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace core.Controllers
{
    public class ClubViewModelsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ClubViewModelsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: ClubViewModels
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

        // GET: ClubViewModels/Create
        public IActionResult Create()
        {
            ClubViewModel club = new ClubViewModel();
            return View(club);
        }



        // POST: ClubViewModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("club_id,club_name,country")] ClubViewModel clubViewModel)
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











        // GET: ClubViewModels/Edit/5
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

        // POST: ClubViewModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("club_id,club_name,country")] ClubViewModel clubViewModel)
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










        // GET: ClubViewModels/Delete/5
        [NoDirectAccess]
        public IActionResult Delete(int? id)
        {
            ClubViewModel club = fetch(id);

            return View(club);
        }

        // POST: ClubViewModels/Delete/5
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
