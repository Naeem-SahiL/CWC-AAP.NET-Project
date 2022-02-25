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
    public class PlayerCWController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public PlayerCWController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void _view_helper(List<PlayerCWModel> playerCWs)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PLAYER_CW_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    PlayerCWModel playerCW = new PlayerCWModel();
                    playerCW.p_id = Convert.ToInt32(tbl.Rows[i]["p_id"].ToString());
                    playerCW.p_name = tbl.Rows[i]["player_name"].ToString();

                    if (tbl.Rows[i]["bat_rank"].ToString() != "")
                        playerCW.bat_rank = Convert.ToInt32(tbl.Rows[i]["bat_rank"].ToString());
                    if (tbl.Rows[i]["style"].ToString() != "")
                        playerCW.style = tbl.Rows[i]["style"].ToString();
                    if (tbl.Rows[i]["fours"].ToString() != "")
                        playerCW.fours = Convert.ToInt32(tbl.Rows[i]["fours"].ToString());
                    if (tbl.Rows[i]["sixes"].ToString() != "")
                        playerCW.sixes = Convert.ToInt32(tbl.Rows[i]["sixes"].ToString());
                    if (tbl.Rows[i]["runs"].ToString() != "")
                        playerCW.runs = Convert.ToInt32(tbl.Rows[i]["runs"].ToString());
                    if (tbl.Rows[i]["fifties"].ToString() != "")
                        playerCW.fifties = Convert.ToInt32(tbl.Rows[i]["fifties"].ToString());
                    if (tbl.Rows[i]["hundreds"].ToString() != "")
                        playerCW.hundreds = Convert.ToInt32(tbl.Rows[i]["hundreds"].ToString());
                    if (tbl.Rows[i]["average"].ToString() != "")
                        playerCW.average = float.Parse(tbl.Rows[i]["average"].ToString());

                    if (tbl.Rows[i]["bow_rank"].ToString() != "")
                        playerCW.bow_rank = Convert.ToInt32(tbl.Rows[i]["bow_rank"].ToString());
                    if (tbl.Rows[i]["best_figure"].ToString() != "")
                        playerCW.best_figure = tbl.Rows[i]["best_figure"].ToString();
                    if (tbl.Rows[i]["runs_conceded"].ToString() != "")
                        playerCW.runs_conceded = Convert.ToInt32(tbl.Rows[i]["runs_conceded"].ToString());
                    if (tbl.Rows[i]["wickets"].ToString() != "")
                        playerCW.wickets = Convert.ToInt32(tbl.Rows[i]["wickets"].ToString());
                    if (tbl.Rows[i]["fiveWick"].ToString() != "")
                        playerCW.fiveWick = Convert.ToInt32(tbl.Rows[i]["fiveWick"].ToString());
                    if (tbl.Rows[i]["econymy"].ToString() != "")
                        playerCW.econymy = float.Parse(tbl.Rows[i]["econymy"].ToString());
                    playerCWs.Add(playerCW);
                }
            }
        }
        public IActionResult Index()
        {
           
            List<PlayerCWModel> playerCWs = new List<PlayerCWModel>();
            _view_helper(playerCWs);
            return View(playerCWs);
        }




        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            PlayerCWModel playerCW = new PlayerCWModel();
            fill_list(playerCW);
            return View(playerCW);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, PlayerCWModel playerCW)
        {
           

            if (ModelState.IsValid)
            {
                if (playerCW.bat_boolen == true && playerCW.bow_boolen == true)
                {
                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("PLAYER_CW_ADD", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_id", playerCW.p_id);
                    

                        cmd.Parameters.AddWithValue("bat_rank", playerCW.bat_rank);
                        cmd.Parameters.AddWithValue("style", playerCW.style);
                        cmd.Parameters.AddWithValue("runs", playerCW.runs);
                        cmd.Parameters.AddWithValue("fours", playerCW.fours);
                        cmd.Parameters.AddWithValue("sixes", playerCW.sixes);
                        cmd.Parameters.AddWithValue("fifties", playerCW.fifties);
                        cmd.Parameters.AddWithValue("hundreds", playerCW.hundreds);
                        cmd.Parameters.AddWithValue("average", playerCW.average);

                        cmd.Parameters.AddWithValue("bow_rank", playerCW.bow_rank);
                        cmd.Parameters.AddWithValue("runs_conceded", playerCW.runs_conceded);
                        cmd.Parameters.AddWithValue("best_figure", playerCW.best_figure);
                        cmd.Parameters.AddWithValue("wickets", playerCW.wickets);
                        cmd.Parameters.AddWithValue("fiveWick", playerCW.fiveWick);
                        cmd.Parameters.AddWithValue("econymy", playerCW.econymy);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else if (playerCW.bat_boolen == true && playerCW.bow_boolen == false)
                {
                     using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("CW_Batsman_ADD", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_id", playerCW.p_id);


                        cmd.Parameters.AddWithValue("bat_rank", playerCW.bat_rank);
                        cmd.Parameters.AddWithValue("style", playerCW.style);
                        cmd.Parameters.AddWithValue("runs", playerCW.runs);
                        cmd.Parameters.AddWithValue("fours", playerCW.fours);
                        cmd.Parameters.AddWithValue("sixes", playerCW.sixes);
                        cmd.Parameters.AddWithValue("fifties", playerCW.fifties);
                        cmd.Parameters.AddWithValue("hundreds", playerCW.hundreds);
                        cmd.Parameters.AddWithValue("average", playerCW.average);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                else if (playerCW.bat_boolen == false && playerCW.bow_boolen == true)
                {
                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("CW_Bowler_ADD", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_id", playerCW.p_id);

                        cmd.Parameters.AddWithValue("bow_rank", playerCW.bow_rank);
                        cmd.Parameters.AddWithValue("runs_conceded", playerCW.runs_conceded);
                        cmd.Parameters.AddWithValue("best_figure", playerCW.best_figure);
                        cmd.Parameters.AddWithValue("wickets", playerCW.wickets);
                        cmd.Parameters.AddWithValue("fiveWick", playerCW.fiveWick);
                        cmd.Parameters.AddWithValue("econymy", playerCW.econymy);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                

                List<PlayerCWModel> playerlist = new List<PlayerCWModel>();
                _view_helper(playerlist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", playerlist) });
            }
            fill_list(playerCW);

            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", playerCW) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            PlayerCWModel playerCW = new PlayerCWModel();
            if (id > 0)
            {
                playerCW = fetch(id);
                fill_list(playerCW);

            }
            return View(playerCW);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PlayerCWModel playerCW)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYER_CW_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id", playerCW.p_id);
                    cmd.Parameters.AddWithValue("bbb", playerCW.B_B_B);

                    cmd.Parameters.AddWithValue("bat_rank", playerCW.bat_rank);
                    cmd.Parameters.AddWithValue("style", playerCW.style);
                    cmd.Parameters.AddWithValue("runs", playerCW.runs);
                    cmd.Parameters.AddWithValue("fours", playerCW.fours);
                    cmd.Parameters.AddWithValue("sixes", playerCW.sixes);
                    cmd.Parameters.AddWithValue("fifties", playerCW.fifties);
                    cmd.Parameters.AddWithValue("hundreds", playerCW.hundreds);
                    cmd.Parameters.AddWithValue("average", playerCW.average);

                    cmd.Parameters.AddWithValue("bow_rank", playerCW.bow_rank);
                    cmd.Parameters.AddWithValue("runs_conceded", playerCW.runs_conceded);
                    cmd.Parameters.AddWithValue("best_figure", playerCW.best_figure);
                    cmd.Parameters.AddWithValue("wickets", playerCW.wickets);
                    cmd.Parameters.AddWithValue("fiveWick", playerCW.fiveWick);
                    cmd.Parameters.AddWithValue("econymy", playerCW.econymy);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                
                List<PlayerCWModel> playerlist = new List<PlayerCWModel>();
                _view_helper(playerlist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", playerlist) });
            }
            fill_list(playerCW);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", playerCW) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            PlayerCWModel playerCW = fetch(id);

            return View(playerCW);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PLAYER_CW_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            List<PlayerCWModel> playerlist = new List<PlayerCWModel>();
            _view_helper(playerlist);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", playerlist) });
        }






        [NonAction]
        public PlayerCWModel fetch(int? id)
        {
            PlayerCWModel playerCW = new PlayerCWModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("PLAYER_CW_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    playerCW.p_id = Convert.ToInt32(tbl.Rows[0]["p_id"].ToString());
                    playerCW.p_name = tbl.Rows[0]["player_name"].ToString();

                    if(tbl.Rows[0]["bat_rank"].ToString() != "")
                        playerCW.bat_rank = Convert.ToInt32(tbl.Rows[0]["bat_rank"].ToString());
                    if (tbl.Rows[0]["style"].ToString() != "")
                        playerCW.style = tbl.Rows[0]["style"].ToString();
                    if (tbl.Rows[0]["fours"].ToString() != "")
                        playerCW.fours = Convert.ToInt32(tbl.Rows[0]["fours"].ToString());
                    if (tbl.Rows[0]["sixes"].ToString() != "")
                        playerCW.sixes = Convert.ToInt32(tbl.Rows[0]["sixes"].ToString());
                    if (tbl.Rows[0]["runs"].ToString() != "")
                        playerCW.runs = Convert.ToInt32(tbl.Rows[0]["runs"].ToString());
                    if (tbl.Rows[0]["fifties"].ToString() != "")
                        playerCW.fifties = Convert.ToInt32(tbl.Rows[0]["fifties"].ToString());
                    if (tbl.Rows[0]["hundreds"].ToString() != "")
                        playerCW.hundreds = Convert.ToInt32(tbl.Rows[0]["hundreds"].ToString());
                    if (tbl.Rows[0]["average"].ToString() != "")
                        playerCW.average = float.Parse(tbl.Rows[0]["average"].ToString());

                    if (tbl.Rows[0]["bow_rank"].ToString() != "")
                        playerCW.bow_rank = Convert.ToInt32(tbl.Rows[0]["bow_rank"].ToString());
                    if (tbl.Rows[0]["best_figure"].ToString() != "")
                        playerCW.best_figure = tbl.Rows[0]["best_figure"].ToString();
                    if (tbl.Rows[0]["runs_conceded"].ToString() != "")
                        playerCW.runs_conceded = Convert.ToInt32(tbl.Rows[0]["runs_conceded"].ToString());
                    if (tbl.Rows[0]["wickets"].ToString() != "")
                        playerCW.wickets = Convert.ToInt32(tbl.Rows[0]["wickets"].ToString());
                    if (tbl.Rows[0]["fiveWick"].ToString() != "")
                        playerCW.fiveWick = Convert.ToInt32(tbl.Rows[0]["fiveWick"].ToString());
                    if (tbl.Rows[0]["econymy"].ToString() != "")
                        playerCW.econymy = float.Parse(tbl.Rows[0]["econymy"].ToString());
                }
                return playerCW;
            }

        }


        [NonAction]

        public void fill_list(PlayerCWModel playerCW)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select p_id,first_name+' '+last_name as 'p_name' from player", con);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                List<PlayerModel> l1 = new List<PlayerModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    PlayerModel player= new PlayerModel();
                    player.p_id= Convert.ToInt32(tbl.Rows[i]["p_id"].ToString());
                    player.first_name = tbl.Rows[i]["p_name"].ToString();
                    l1.Add(player);
                }
                playerCW.players = l1;

                con.Close();
            }

        }
    }
}
