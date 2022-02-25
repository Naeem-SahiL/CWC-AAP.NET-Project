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
    public class PlayedForDomesticController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public PlayedForDomesticController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void _view_helper(List<Played_for_domesticModel> played_For_DomesticsList)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PLAYEDFORDOMESTIC_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    Played_for_domesticModel played_For_DomesticModel = new Played_for_domesticModel();
                    played_For_DomesticModel.player_id = Convert.ToInt32(tbl.Rows[i]["player_id"].ToString());
                    played_For_DomesticModel.player_name = tbl.Rows[i]["player_name"].ToString();
                    played_For_DomesticModel.domestic_nmae = tbl.Rows[i]["domestic_nmae"].ToString();
                    played_For_DomesticModel.domestic_id = Convert.ToInt32(tbl.Rows[i]["domestic_id"].ToString());
                    try {played_For_DomesticModel.domestic_ODI = Convert.ToInt32(tbl.Rows[i]["domestic_ODI"].ToString()); }
                    catch (Exception) { }

                    try { played_For_DomesticModel.domestic_test_id = Convert.ToInt32(tbl.Rows[i]["domestic_test_id"].ToString()); }
                    catch (Exception) { }

                    try { played_For_DomesticModel.domestic_T_20_id = Convert.ToInt32(tbl.Rows[i]["domestic_T_20_id"].ToString());}
                    catch (Exception) { }
                    
                    played_For_DomesticsList.Add(played_For_DomesticModel);
                }
            }
        }
        public IActionResult Index()
        {
           
            List<Played_for_domesticModel> played_For_DomesticsList = new List<Played_for_domesticModel>();
            _view_helper(played_For_DomesticsList);
            return View(played_For_DomesticsList);
        }






        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            Played_for_domesticModel played_For_DomesticModel = new Played_for_domesticModel();
            fill_list(played_For_DomesticModel);
            return View(played_For_DomesticModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, Played_for_domesticModel played_For_DomesticModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("PLAYED_FOR_DOMESTIC_ADD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("player_id", played_For_DomesticModel.player_id);
                        cmd.Parameters.AddWithValue("domestic_id", played_For_DomesticModel.domestic_id);
                        cmd.Parameters.AddWithValue("domestic_ODI", played_For_DomesticModel.domestic_ODI);
                        cmd.Parameters.AddWithValue("domestic_test_id", played_For_DomesticModel.domestic_test_id);
                        cmd.Parameters.AddWithValue("domestic_T_20_id", played_For_DomesticModel.domestic_T_20_id);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

                List<Played_for_domesticModel> played_For_DomesticModellist = new List<Played_for_domesticModel>();
                _view_helper(played_For_DomesticModellist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_DomesticModellist) });
            }
            fill_list(played_For_DomesticModel);

            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", played_For_DomesticModel) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Played_for_domesticModel played_For_DomesticModel = new Played_for_domesticModel();
            if (id > 0)
            {
                played_For_DomesticModel = fetch(id);
                fill_list(played_For_DomesticModel);

            }
            return View(played_For_DomesticModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Played_for_domesticModel played_For_DomesticModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYED_FOR_DOMESTIC_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("player_id", played_For_DomesticModel.player_id);
                    cmd.Parameters.AddWithValue("domestic_id", played_For_DomesticModel.domestic_id);
                    cmd.Parameters.AddWithValue("domestic_ODI", played_For_DomesticModel.domestic_ODI);
                    cmd.Parameters.AddWithValue("domestic_test_id", played_For_DomesticModel.domestic_test_id);
                    cmd.Parameters.AddWithValue("domestic_T_20_id", played_For_DomesticModel.domestic_T_20_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                
                List<Played_for_domesticModel> played_For_DomesticModellist = new List<Played_for_domesticModel>();
                _view_helper(played_For_DomesticModellist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_DomesticModellist) });
            }
            fill_list(played_For_DomesticModel);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", played_For_DomesticModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Played_for_domesticModel played_For_DomesticModel = fetch(id);

            return View(played_For_DomesticModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PLAYED_FOR_DOMESTIC_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            List<Played_for_domesticModel> played_For_DomesticModellist = new List<Played_for_domesticModel>();
            _view_helper(played_For_DomesticModellist);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_DomesticModellist) });
        }




        [NonAction]
        public Played_for_domesticModel fetch(int? id)
        {
            Played_for_domesticModel played_For_DomesticModel = new Played_for_domesticModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("PLAYED_FOR_DOMESTIC_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    played_For_DomesticModel.player_id = Convert.ToInt32(tbl.Rows[0]["player_id"].ToString());
                    played_For_DomesticModel.player_name = tbl.Rows[0]["player_name"].ToString();
                    played_For_DomesticModel.domestic_nmae = tbl.Rows[0]["domestic_nmae"].ToString();
                    played_For_DomesticModel.domestic_id = Convert.ToInt32(tbl.Rows[0]["domestic_id"].ToString());
                    try { played_For_DomesticModel.domestic_ODI = Convert.ToInt32(tbl.Rows[0]["domestic_ODI"].ToString()); }
                    catch (Exception) { }

                    try { played_For_DomesticModel.domestic_test_id = Convert.ToInt32(tbl.Rows[0]["domestic_test_id"].ToString()); }
                    catch (Exception) { }

                    try { played_For_DomesticModel.domestic_T_20_id = Convert.ToInt32(tbl.Rows[0]["domestic_T_20_id"].ToString()); }
                    catch (Exception) { }
                }
                return played_For_DomesticModel;
            }

        }


        [NonAction]

        public void fill_list(Played_for_domesticModel played_For_DomesticModel)
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
                        played_For_DomesticModel.players = players;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select domestic_id,domestic_nmae from domestic", con))
                {
                    using (SqlDataAdapter adptr = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        List<DomesticModel> domestics= new List<DomesticModel>();
                        adptr.Fill(tbl);
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            DomesticModel domestic= new DomesticModel();
                            domestic.domestic_id= Convert.ToInt32(tbl.Rows[i]["domestic_id"].ToString());
                            domestic.domestic_nmae= tbl.Rows[i]["domestic_nmae"].ToString();
                            domestics.Add(domestic);
                        }
                        played_For_DomesticModel.domestics= domestics;
                    }
                }
            }

        }
    }
}
