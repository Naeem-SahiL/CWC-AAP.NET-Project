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
    public class WorldCupController : Controller
    {

        private readonly IConfiguration _configuration;

        public WorldCupController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void _view_helper(List<WorldCupModel> worldCups)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from WORLDCUP_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    WorldCupModel worldCup= new WorldCupModel();
                    worldCup.worldcup_id = Convert.ToInt32(tbl.Rows[i]["worldcup_id"].ToString());
                    worldCup.no_of_teams = Convert.ToInt32(tbl.Rows[i]["no_of_teams"].ToString());
                    worldCup.Worldcup_year = Convert.ToInt32(tbl.Rows[i]["Worldcup_year"].ToString());
                    worldCup.place = tbl.Rows[i]["place"].ToString();
                    worldCup.format_of_wc = tbl.Rows[i]["format_of_wc"].ToString();
                    worldCup.winnerteam = tbl.Rows[i]["winnerteam"].ToString();
                    worldCups.Add(worldCup);
                }
            }
        }
        public IActionResult Index()
        {
            List<WorldCupModel> worldCups = new List<WorldCupModel>();
            _view_helper(worldCups);
            return View(worldCups);
        }  
        public IActionResult wc(int id)
        {
            WorldCupModel worldCup = fetch(id);

            return View(worldCup);
        }

        [NoDirectAccess]
        public IActionResult Create()
        {
            WorldCupModel worldCup= new WorldCupModel();
            

            return View(worldCup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, WorldCupModel worldCup)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("WORLDCUP_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", worldCup.worldcup_id);
                    cmd.Parameters.AddWithValue("teams", worldCup.no_of_teams);
                    cmd.Parameters.AddWithValue("place", worldCup.place);
                    cmd.Parameters.AddWithValue("year", worldCup.Worldcup_year);
                    cmd.Parameters.AddWithValue("format", worldCup.format_of_wc);
                    cmd.Parameters.AddWithValue("winner", worldCup.winnerteam);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                List<WorldCupModel> worldCups = new List<WorldCupModel>();
                _view_helper(worldCups);

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", worldCups) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Create", worldCup) });
        }


        [NoDirectAccess]
        public IActionResult Edit(int? id)
        {

            WorldCupModel worldCup = new WorldCupModel();
            if (id > 0)
            {
                worldCup = fetch(id);
            }
            return View(worldCup);
        }

        [HttpPost]
        public IActionResult Edit(int id, WorldCupModel worldCup)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("WORLDCUP_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", worldCup.worldcup_id);
                    cmd.Parameters.AddWithValue("teams", worldCup.no_of_teams);
                    cmd.Parameters.AddWithValue("place", worldCup.place);
                    cmd.Parameters.AddWithValue("year", worldCup.Worldcup_year);
                    cmd.Parameters.AddWithValue("format", worldCup.format_of_wc);
                    cmd.Parameters.AddWithValue("winner", worldCup.winnerteam);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                List<WorldCupModel> worldCups = new List<WorldCupModel>();
                _view_helper(worldCups);

                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", worldCups) });
            }
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", worldCup) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            WorldCupModel worldCup = fetch(id);

            return View(worldCup);
        }

        // POST: Umpires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("WORLDCUP_DELTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            List<WorldCupModel> worldCups = new List<WorldCupModel>();
            _view_helper(worldCups);

            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", worldCups) });
        }






        [NonAction]
        public WorldCupModel fetch(int? id)
        {
            WorldCupModel worldCup = new WorldCupModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("WORLDCUP_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    worldCup.worldcup_id = Convert.ToInt32(tbl.Rows[0]["worldcup_id"].ToString());
                    worldCup.no_of_teams = Convert.ToInt32(tbl.Rows[0]["no_of_teams"].ToString());
                    worldCup.Worldcup_year = Convert.ToInt32(tbl.Rows[0]["Worldcup_year"].ToString());
                    worldCup.place = tbl.Rows[0]["place"].ToString();
                    worldCup.format_of_wc = tbl.Rows[0]["format_of_wc"].ToString();
                    worldCup.winnerteam = tbl.Rows[0]["winnerteam"].ToString();
                }
                return worldCup;
            }

        }
    }
}
