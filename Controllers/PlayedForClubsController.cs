﻿using core.Models;
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
    public class PlayedForClubsController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public PlayedForClubsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void _view_helper(List<Played_for_clubModel> played_For_ClubsList)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PLAYEDFORCLUBS_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    Played_for_clubModel played_For_ClubModel = new Played_for_clubModel();
                    played_For_ClubModel.player_id = Convert.ToInt32(tbl.Rows[i]["player_id"].ToString());
                    played_For_ClubModel.player_name = tbl.Rows[i]["player_name"].ToString();
                    played_For_ClubModel.club_name = tbl.Rows[i]["club_name"].ToString();
                    played_For_ClubModel.club_id = Convert.ToInt32(tbl.Rows[i]["club_id"].ToString());
                    try {played_For_ClubModel.club_ODI = Convert.ToInt32(tbl.Rows[i]["club_ODI"].ToString()); }
                    catch (Exception) { }

                    try { played_For_ClubModel.club_test_id = Convert.ToInt32(tbl.Rows[i]["club_test_id"].ToString()); }
                    catch (Exception) { }

                    try { played_For_ClubModel.club_T_20_id = Convert.ToInt32(tbl.Rows[i]["club_T_20_id"].ToString());}
                    catch (Exception) { }
                    
                    played_For_ClubsList.Add(played_For_ClubModel);
                }
            }
        }
        public IActionResult Index()
        {
           
            List<Played_for_clubModel> played_For_ClubsList = new List<Played_for_clubModel>();
            _view_helper(played_For_ClubsList);
            return View(played_For_ClubsList);
        }






        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            Played_for_clubModel played_For_ClubModel = new Played_for_clubModel();
            fill_list(played_For_ClubModel);
            return View(played_For_ClubModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, Played_for_clubModel played_For_ClubModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("PLAYED_FOR_CLUBS_ADD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("player_id", played_For_ClubModel.player_id);
                        cmd.Parameters.AddWithValue("club_id", played_For_ClubModel.club_id);
                        cmd.Parameters.AddWithValue("club_ODI", played_For_ClubModel.club_ODI);
                        cmd.Parameters.AddWithValue("club_test_id", played_For_ClubModel.club_test_id);
                        cmd.Parameters.AddWithValue("club_T_20_id", played_For_ClubModel.club_T_20_id);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

                List<Played_for_clubModel> played_For_ClubModellist = new List<Played_for_clubModel>();
                _view_helper(played_For_ClubModellist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_ClubModellist) });
            }
            fill_list(played_For_ClubModel);

            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", played_For_ClubModel) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Played_for_clubModel played_For_ClubModel = new Played_for_clubModel();
            if (id > 0)
            {
                played_For_ClubModel = fetch(id);
                fill_list(played_For_ClubModel);

            }
            return View(played_For_ClubModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Played_for_clubModel played_For_ClubModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYED_FOR_CLUBS_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("player_id", played_For_ClubModel.player_id);
                    cmd.Parameters.AddWithValue("club_id", played_For_ClubModel.club_id);
                    cmd.Parameters.AddWithValue("club_ODI", played_For_ClubModel.club_ODI);
                    cmd.Parameters.AddWithValue("club_test_id", played_For_ClubModel.club_test_id);
                    cmd.Parameters.AddWithValue("club_T_20_id", played_For_ClubModel.club_T_20_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                
                List<Played_for_clubModel> played_For_ClubModellist = new List<Played_for_clubModel>();
                _view_helper(played_For_ClubModellist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_ClubModellist) });
            }
            fill_list(played_For_ClubModel);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", played_For_ClubModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Played_for_clubModel played_For_ClubModel = fetch(id);

            return View(played_For_ClubModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PLAYED_FOR_CLUBS_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            List<Played_for_clubModel> played_For_ClubModellist = new List<Played_for_clubModel>();
            _view_helper(played_For_ClubModellist);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_ClubModellist) });
        }




        [NonAction]
        public Played_for_clubModel fetch(int? id)
        {
            Played_for_clubModel played_For_ClubModel = new Played_for_clubModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("PLAYED_FOR_CLUBS_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    played_For_ClubModel.player_id = Convert.ToInt32(tbl.Rows[0]["player_id"].ToString());
                    played_For_ClubModel.player_name = tbl.Rows[0]["player_name"].ToString();
                    played_For_ClubModel.club_name = tbl.Rows[0]["club_name"].ToString();
                    played_For_ClubModel.club_id = Convert.ToInt32(tbl.Rows[0]["club_id"].ToString());
                    try { played_For_ClubModel.club_ODI = Convert.ToInt32(tbl.Rows[0]["club_ODI"].ToString()); }
                    catch (Exception) { }

                    try { played_For_ClubModel.club_test_id = Convert.ToInt32(tbl.Rows[0]["club_test_id"].ToString()); }
                    catch (Exception) { }

                    try { played_For_ClubModel.club_T_20_id = Convert.ToInt32(tbl.Rows[0]["club_T_20_id"].ToString()); }
                    catch (Exception) { }
                }
                return played_For_ClubModel;
            }

        }


        [NonAction]

        public void fill_list(Played_for_clubModel played_For_ClubModel)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select p_id,first_name+' '+last_name as 'p_name' from player", con))
                {
                    using (SqlDataAdapter adptr = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        List<PlayerModel> players= new List<PlayerModel>();
                        adptr.Fill(tbl);
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            PlayerModel player = new PlayerModel();
                            player.p_id= Convert.ToInt32(tbl.Rows[i]["p_id"].ToString());
                            player.first_name = tbl.Rows[i]["p_name"].ToString();
                            players.Add(player);
                        }
                        played_For_ClubModel.players = players;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select club_id,club_name from club", con))
                {
                    using (SqlDataAdapter adptr = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        List<ClubViewModel> clubs= new List<ClubViewModel>();
                        adptr.Fill(tbl);
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ClubViewModel club= new ClubViewModel();
                            club.club_id= Convert.ToInt32(tbl.Rows[i]["club_id"].ToString());
                            club.club_name= tbl.Rows[i]["club_name"].ToString();
                            clubs.Add(club);
                        }
                        played_For_ClubModel.clubs= clubs;
                    }
                }
            }

        }
    }
}
