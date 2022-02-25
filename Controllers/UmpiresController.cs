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
    public class UmpiresController : Controller
    {
        private readonly IConfiguration _configuration;

        public UmpiresController(IConfiguration configuration)
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
                SqlDataAdapter adapter = new SqlDataAdapter("select * from UMPIRES_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return View(tbl);
        }



        // GET: Umpires/Add/
        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            UmpiresViewModel umpires = new UmpiresViewModel();
            return View(umpires);
        }


        //HTTp POST method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Add(int id, [Bind("umpire_id,umpire_name,maches_umpired,country")] UmpiresViewModel umpiresViewModel)
        {
            if(ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UMPIRE_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", umpiresViewModel.umpire_id);
                    cmd.Parameters.AddWithValue("name", umpiresViewModel.umpire_name);
                    cmd.Parameters.AddWithValue("no_of_matches", umpiresViewModel.maches_umpired);
                    cmd.Parameters.AddWithValue("cntry", umpiresViewModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from UMPIRES_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this,"_View",tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", umpiresViewModel) });
        }






        [NoDirectAccess]
       
        public IActionResult Edit(int? id)
        {
            UmpiresViewModel umpire = new UmpiresViewModel();
            if(id > 0)
            {
                umpire = fetch(id);
            }
            return View(umpire);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("umpire_id,umpire_name,maches_umpired,country")] UmpiresViewModel umpiresViewModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UMPIRE_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", umpiresViewModel.umpire_id);
                    cmd.Parameters.AddWithValue("name", umpiresViewModel.umpire_name);
                    cmd.Parameters.AddWithValue("no_of_matches", umpiresViewModel.maches_umpired);
                    cmd.Parameters.AddWithValue("cntry", umpiresViewModel.country);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from UMPIRES_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", umpiresViewModel) });
        }










        // GET: Umpires/Delete/5
        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            UmpiresViewModel umpire = fetch(id);
            
            return View(umpire);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE_UMPIRE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from UMPIRES_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", tbl) });
        }






        [NonAction]
        public UmpiresViewModel fetch(int ? id)
        {
            UmpiresViewModel umpire = new UmpiresViewModel();
            
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("UMPIRES_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if(tbl.Rows.Count == 1)
                {
                    umpire.umpire_id = Convert.ToInt32(id);
                    umpire.umpire_name = tbl.Rows[0]["umpir_name"].ToString();
                    umpire.maches_umpired=Convert.ToInt32( tbl.Rows[0]["matches_umpired"].ToString());
                    umpire.country = tbl.Rows[0]["country"].ToString();
                }
                return umpire; 
            }

        }
    }
}
