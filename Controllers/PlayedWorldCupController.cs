using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace core.Controllers
{
    public class PlayedWorldCupController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public PlayedWorldCupController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void _view_helper(List<Played_for_WorldCupModel> played_For_WorldCups)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PLAYED_WORLDCUP_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    Played_for_WorldCupModel played_For_WorldCup = new Played_for_WorldCupModel();
                    played_For_WorldCup.player_id = Convert.ToInt32(tbl.Rows[i]["player_id"].ToString());
                    played_For_WorldCup.worldcup_id = Convert.ToInt32(tbl.Rows[i]["worldcup_id"].ToString());
                    played_For_WorldCup.pw_id = Convert.ToInt32(tbl.Rows[i]["pw_id"].ToString());
                    played_For_WorldCup.player_name = tbl.Rows[i]["player_name"].ToString();
                    played_For_WorldCup.worldcup_name = tbl.Rows[i]["worldcup_name"].ToString();

                    played_For_WorldCups.Add(played_For_WorldCup);
                }
            }
        }
        public IActionResult Index()
        {

            List<Played_for_WorldCupModel> played_For_WorldCups = new List<Played_for_WorldCupModel>();
            _view_helper(played_For_WorldCups);
            return View(played_For_WorldCups);
        }


        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            Played_for_WorldCupModel played_For_WorldCup = new Played_for_WorldCupModel();
            fill_list(played_For_WorldCup);
            return View(played_For_WorldCup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, Played_for_WorldCupModel played_For_WorldCup)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("PLAYED_FOR_WORLDCUP_ADD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("player_id", played_For_WorldCup.player_id);
                        cmd.Parameters.AddWithValue("worldcup_id", played_For_WorldCup.worldcup_id);
                        cmd.Parameters.AddWithValue("pw_id", played_For_WorldCup.pw_id);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

                List<Played_for_WorldCupModel> played_For_WorldCuplist = new List<Played_for_WorldCupModel>();
                _view_helper(played_For_WorldCuplist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_WorldCuplist) });
            }
            fill_list(played_For_WorldCup);

            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", played_For_WorldCup) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Played_for_WorldCupModel played_For_WorldCup = new Played_for_WorldCupModel();
            if (id > 0)
            {
                played_For_WorldCup = fetch(id);
                fill_list(played_For_WorldCup);

            }
            return View(played_For_WorldCup);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Played_for_WorldCupModel played_For_WorldCup)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYED_FOR_WORLDCUP_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("player_id", played_For_WorldCup.player_id);
                    cmd.Parameters.AddWithValue("worldcup_id", played_For_WorldCup.worldcup_id);
                    cmd.Parameters.AddWithValue("pw_id", played_For_WorldCup.pw_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                List<Played_for_WorldCupModel> played_For_WorldCuplist = new List<Played_for_WorldCupModel>();
                _view_helper(played_For_WorldCuplist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_WorldCuplist) });
            }
            fill_list(played_For_WorldCup);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", played_For_WorldCup) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Played_for_WorldCupModel played_For_WorldCup = fetch(id);

            return View(played_For_WorldCup);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PLAYED_FOR_WORLDCUP_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            List<Played_for_WorldCupModel> played_For_WorldCuplist = new List<Played_for_WorldCupModel>();
            _view_helper(played_For_WorldCuplist);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_WorldCuplist) });
        }




        [NonAction]
        public Played_for_WorldCupModel fetch(int? id)
        {
            Played_for_WorldCupModel played_For_WorldCup = new Played_for_WorldCupModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("PLAYED_FOR_WORLDCUP_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    played_For_WorldCup.player_id = Convert.ToInt32(tbl.Rows[0]["player_id"].ToString());
                    played_For_WorldCup.worldcup_id = Convert.ToInt32(tbl.Rows[0]["worldcup_id"].ToString());
                    played_For_WorldCup.pw_id = Convert.ToInt32(tbl.Rows[0]["pw_id"].ToString());
                }
                return played_For_WorldCup;
            }

        }


        [NonAction]

        public void fill_list(Played_for_WorldCupModel played_For_WorldCup)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select p_id,first_name+' '+last_name as 'p_name' from player", con))
                {
                    using (SqlDataAdapter adptr = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        List<PlayerModel> players = new List<PlayerModel>();
                        adptr.Fill(tbl);
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            PlayerModel player = new PlayerModel();
                            player.p_id = Convert.ToInt32(tbl.Rows[i]["p_id"].ToString());
                            player.first_name = tbl.Rows[i]["p_name"].ToString();
                            players.Add(player);
                        }
                        played_For_WorldCup.Players = players;
                    }
                }
                
                using (SqlCommand cmd = new SqlCommand("select worldcup_id, cast(Worldcup_year as varchar(4)) +' ' + place + ' ' + format_of_wc as 'worldcup_name' from worldcup", con))
                {
                    using (SqlDataAdapter adptr = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        List<WorldCupModel> wcs = new List<WorldCupModel>();
                        adptr.Fill(tbl);
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            WorldCupModel worldCup = new WorldCupModel();
                            worldCup.worldcup_id = Convert.ToInt32(tbl.Rows[i]["worldcup_id"].ToString());
                            worldCup.format_of_wc = tbl.Rows[i]["worldcup_name"].ToString();
                            wcs.Add(worldCup);
                        }
                        played_For_WorldCup.WorldCups = wcs;
                    }
                }
            }

        }
    }
}
