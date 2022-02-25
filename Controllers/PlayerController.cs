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
    public class PlayerController : Controller
    {
        public IConfiguration _configuration { get; private set; }
        public PlayerController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void _view_helper(List<PlayerModel> playerlist)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PLAYER_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    PlayerModel player = new PlayerModel();
                    player.p_id = Convert.ToInt32(tbl.Rows[i]["p_id"].ToString());
                    player.first_name = tbl.Rows[i]["first_name"].ToString();
                    player.last_name = tbl.Rows[i]["last_name"].ToString();
                    player.country = tbl.Rows[i]["country"].ToString();
                    if (tbl.Rows[i]["height"].ToString() != "")
                        player.height = float.Parse(tbl.Rows[i]["height"].ToString());
                    if (tbl.Rows[i]["dob"].ToString() != "")
                        player.dob = DateTime.Parse(tbl.Rows[i]["dob"].ToString());
                    if (tbl.Rows[i]["debut_date"].ToString() != "")
                        player.debut_date = DateTime.Parse(tbl.Rows[i]["debut_date"].ToString());
                    if (tbl.Rows[i]["T_20s"].ToString() != "")
                        player.T_20s = Convert.ToInt32(tbl.Rows[i]["T_20s"].ToString());
                    if (tbl.Rows[i]["ODIS"].ToString() != "")
                        player.ODIS = Convert.ToInt32(tbl.Rows[i]["ODIS"].ToString());
                    if (tbl.Rows[i]["tests"].ToString() != "")
                        player.tests = Convert.ToInt32(tbl.Rows[i]["tests"].ToString());
                    player.team_id = Convert.ToInt32(tbl.Rows[i]["team_id"].ToString());
                    playerlist.Add(player);
                }
            }
        }
        public IActionResult Index()
        {
           
            List<PlayerModel> playerlist = new List<PlayerModel>();
            _view_helper(playerlist);
            return View(playerlist);
        }






        [NoDirectAccess]
        public IActionResult Add(int? id)
        {
            PlayerModel player = new PlayerModel();
            fill_list(player);
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id, PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYER_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", player.p_id);
                    cmd.Parameters.AddWithValue("first_name", player.first_name);
                    cmd.Parameters.AddWithValue("last_name", player.last_name);
                    cmd.Parameters.AddWithValue("country", player.country);
                    cmd.Parameters.AddWithValue("height", player.height);
                    cmd.Parameters.AddWithValue("dob", player.dob);
                    cmd.Parameters.AddWithValue("debut_date", player.debut_date);
                    cmd.Parameters.AddWithValue("T_20s", player.T_20s);
                    cmd.Parameters.AddWithValue("ODIS", player.ODIS);
                    cmd.Parameters.AddWithValue("tests", player.tests);
                    cmd.Parameters.AddWithValue("team_id", player.team_id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                List<PlayerModel> playerlist = new List<PlayerModel>();
                _view_helper(playerlist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", playerlist) });
            }
            fill_list(player);

            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Add", player) });
        }




        [NoDirectAccess]

        public IActionResult Edit(int? id)
        {
            PlayerModel player = new PlayerModel();
            if (id > 0)
            {
                player = fetch(id);
                fill_list(player);

            }
            return View(player);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PLAYER_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", player.p_id);
                    cmd.Parameters.AddWithValue("first_name", player.first_name);
                    cmd.Parameters.AddWithValue("last_name", player.last_name);
                    cmd.Parameters.AddWithValue("country", player.country);
                    cmd.Parameters.AddWithValue("height", player.height);
                    cmd.Parameters.AddWithValue("dob", player.dob);
                    cmd.Parameters.AddWithValue("debut_date", player.debut_date);
                    cmd.Parameters.AddWithValue("T_20s", player.T_20s);
                    cmd.Parameters.AddWithValue("ODIS", player.ODIS);
                    cmd.Parameters.AddWithValue("tests", player.tests);
                    cmd.Parameters.AddWithValue("team_id", player.team_id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                
                List<PlayerModel> playerlist = new List<PlayerModel>();
                _view_helper(playerlist);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", playerlist) });
            }
            fill_list(player);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", player) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            PlayerModel player = fetch(id);

            return View(player);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PLAYER_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            List<PlayerModel> playerlist = new List<PlayerModel>();
            _view_helper(playerlist);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", playerlist) });
        }






        [NonAction]
        public PlayerModel fetch(int? id)
        {
            PlayerModel player = new PlayerModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("PLAYER_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    player.p_id = Convert.ToInt32(tbl.Rows[0]["p_id"].ToString());
                    player.first_name = tbl.Rows[0]["first_name"].ToString();
                    player.last_name = tbl.Rows[0]["last_name"].ToString();
                    player.country = tbl.Rows[0]["country"].ToString();
                    if (tbl.Rows[0]["height"].ToString() != "")
                        player.height = float.Parse(tbl.Rows[0]["height"].ToString());
                    if (tbl.Rows[0]["dob"].ToString() != "")
                        player.dob = DateTime.Parse(tbl.Rows[0]["dob"].ToString());
                    if (tbl.Rows[0]["debut_date"].ToString() != "")
                        player.debut_date = DateTime.Parse(tbl.Rows[0]["debut_date"].ToString());
                    if (tbl.Rows[0]["T_20s"].ToString() != "")
                        player.T_20s = Convert.ToInt32(tbl.Rows[0]["T_20s"].ToString());
                    if (tbl.Rows[0]["ODIS"].ToString() != "")
                        player.ODIS = Convert.ToInt32(tbl.Rows[0]["ODIS"].ToString());
                    if (tbl.Rows[0]["tests"].ToString() != "")
                        player.tests = Convert.ToInt32(tbl.Rows[0]["tests"].ToString());
                    player.team_id = Convert.ToInt32(tbl.Rows[0]["team_id"].ToString());
                }
                return player;
            }

        }


        [NonAction]

        public void fill_list(PlayerModel player)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select team_id,team_name from team", con);
                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                List<TeamModel> l1 = new List<TeamModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    TeamModel team= new TeamModel();
                    team.team_id= Convert.ToInt32(tbl.Rows[i]["team_id"].ToString());
                    team.team_name = tbl.Rows[i]["team_name"].ToString();
                    l1.Add(team);
                }
                player.Teams = l1;

                con.Close();
            }

        }
    }
}
