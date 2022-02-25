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
    public class PlayedForRegionController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public PlayedForRegionController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void _view_helper(List<Played_for_regionModel> played_For_RegionsList)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PLAYEDFORREGIONS_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    Played_for_regionModel played_For_RegionModel = new Played_for_regionModel();
                    played_For_RegionModel.player_id = Convert.ToInt32(tbl.Rows[i]["player_id"].ToString());
                    played_For_RegionModel.player_name = tbl.Rows[i]["player_name"].ToString();
                    played_For_RegionModel.region_name = tbl.Rows[i]["region_name"].ToString();
                    played_For_RegionModel.region_id = Convert.ToInt32(tbl.Rows[i]["region_id"].ToString());
                    try {played_For_RegionModel.region_ODI = Convert.ToInt32(tbl.Rows[i]["region_ODI"].ToString()); }
                    catch (Exception) { }

                    try { played_For_RegionModel.region_test_id = Convert.ToInt32(tbl.Rows[i]["region_test_id"].ToString()); }
                    catch (Exception) { }

                    try { played_For_RegionModel.region_T_20_id = Convert.ToInt32(tbl.Rows[i]["region_T_20_id"].ToString());}
                    catch (Exception) { }
                    
                    played_For_RegionsList.Add(played_For_RegionModel);
                }
            }
        }
        public IActionResult Index()
        {
           
            List<Played_for_regionModel> played_For_RegionsList = new List<Played_for_regionModel>();
            _view_helper(played_For_RegionsList);
            return View(played_For_RegionsList);
        }






        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            Played_for_regionModel played_For_RegionModel = new Played_for_regionModel();
            fill_list(played_For_RegionModel);
            return View(played_For_RegionModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, Played_for_regionModel played_For_RegionModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("PLAYED_FOR_REGION_ADD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("player_id", played_For_RegionModel.player_id);
                        cmd.Parameters.AddWithValue("region_id", played_For_RegionModel.region_id);
                        cmd.Parameters.AddWithValue("region_ODI", played_For_RegionModel.region_ODI);
                        cmd.Parameters.AddWithValue("region_test_id", played_For_RegionModel.region_test_id);
                        cmd.Parameters.AddWithValue("region_T_20_id", played_For_RegionModel.region_T_20_id);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

                List<Played_for_regionModel> played_For_RegionModellist = new List<Played_for_regionModel>();
                _view_helper(played_For_RegionModellist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_RegionModellist) });
            }
            fill_list(played_For_RegionModel);

            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", played_For_RegionModel) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            Played_for_regionModel played_For_RegionModel = new Played_for_regionModel();
            if (id > 0)
            {
                played_For_RegionModel = fetch(id);
                fill_list(played_For_RegionModel);

            }
            return View(played_For_RegionModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Played_for_regionModel played_For_RegionModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYED_FOR_REGION_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("player_id", played_For_RegionModel.player_id);
                    cmd.Parameters.AddWithValue("region_id", played_For_RegionModel.region_id);
                    cmd.Parameters.AddWithValue("region_ODI", played_For_RegionModel.region_ODI);
                    cmd.Parameters.AddWithValue("region_test_id", played_For_RegionModel.region_test_id);
                    cmd.Parameters.AddWithValue("region_T_20_id", played_For_RegionModel.region_T_20_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                
                List<Played_for_regionModel> played_For_RegionModellist = new List<Played_for_regionModel>();
                _view_helper(played_For_RegionModellist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_RegionModellist) });
            }
            fill_list(played_For_RegionModel);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", played_For_RegionModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            Played_for_regionModel played_For_RegionModel = fetch(id);

            return View(played_For_RegionModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PLAYED_FOR_REGION_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            List<Played_for_regionModel> played_For_RegionModellist = new List<Played_for_regionModel>();
            _view_helper(played_For_RegionModellist);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", played_For_RegionModellist) });
        }




        [NonAction]
        public Played_for_regionModel fetch(int? id)
        {
            Played_for_regionModel played_For_RegionModel = new Played_for_regionModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("PLAYED_FOR_REGION_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    played_For_RegionModel.player_id = Convert.ToInt32(tbl.Rows[0]["player_id"].ToString());
                    played_For_RegionModel.player_name = tbl.Rows[0]["player_name"].ToString();
                    played_For_RegionModel.region_name = tbl.Rows[0]["region_name"].ToString();
                    played_For_RegionModel.region_id = Convert.ToInt32(tbl.Rows[0]["region_id"].ToString());
                    try { played_For_RegionModel.region_ODI = Convert.ToInt32(tbl.Rows[0]["region_ODI"].ToString()); }
                    catch (Exception) { }

                    try { played_For_RegionModel.region_test_id = Convert.ToInt32(tbl.Rows[0]["region_test_id"].ToString()); }
                    catch (Exception) { }

                    try { played_For_RegionModel.region_T_20_id = Convert.ToInt32(tbl.Rows[0]["region_T_20_id"].ToString()); }
                    catch (Exception) { }
                }
                return played_For_RegionModel;
            }

        }


        [NonAction]

        public void fill_list(Played_for_regionModel played_For_RegionModel)
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
                        played_For_RegionModel.players = players;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select region_id,region_name from region", con))
                {
                    using (SqlDataAdapter adptr = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        List<RegionModel> regions= new List<RegionModel>();
                        adptr.Fill(tbl);
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            RegionModel region= new RegionModel();
                            region.region_id= Convert.ToInt32(tbl.Rows[i]["region_id"].ToString());
                            region.region_name= tbl.Rows[i]["region_name"].ToString();
                            regions.Add(region);
                        }
                        played_For_RegionModel.regions= regions;
                    }
                }
            }

        }
    }
}
