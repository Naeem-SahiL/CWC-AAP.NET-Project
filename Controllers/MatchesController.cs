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
    public class MatchesController : Controller
    {

        private readonly IConfiguration _configuration;

        public MatchesController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public IActionResult Index()
        {
            List<MatchesModel> l1 = new List<MatchesModel>();
            _view_helper(l1);

            return View(l1);
        }

        [NoDirectAccess]
        public IActionResult Create()
        {
            MatchesModel matches= new MatchesModel();
            fill_list(matches);
            return View(matches);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id,MatchesModel matchesModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("MATCHES_ADD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("match_id", matchesModel.match_id);
                    cmd.Parameters.AddWithValue("firstteam_name", matchesModel.firstteam_name);
                    cmd.Parameters.AddWithValue("secondteam_name", matchesModel.secondteam_name);
                    cmd.Parameters.AddWithValue("loser", matchesModel.loser);
                    cmd.Parameters.AddWithValue("winner", matchesModel.winner);
                    cmd.Parameters.AddWithValue("match_date", matchesModel.match_date);
                    cmd.Parameters.AddWithValue("referee_id", matchesModel.referee_id);
                    cmd.Parameters.AddWithValue("stad_id", matchesModel.stad_id);
                    cmd.Parameters.AddWithValue("sc_id", matchesModel.sc_id);
                    cmd.Parameters.AddWithValue("matchtime", matchesModel.matchtime);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from MATCHES_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }
                List<MatchesModel> l1 = new List<MatchesModel>();
                _view_helper(l1);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", l1) });
            }
            fill_list(matchesModel);
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Create", matchesModel) });
        }


        [NoDirectAccess]
        public IActionResult Edit(int? id)
        {

            MatchesModel matches = new MatchesModel();
            
            if (id > 0)
            {
                matches = fetch(id);
            }
            fill_list(matches);
            return View(matches);
        }

        [HttpPost]
        public IActionResult Edit(int id,  MatchesModel matchesModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("MATCHES_EDIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("match_id", matchesModel.match_id);
                    cmd.Parameters.AddWithValue("firstteam_name", matchesModel.firstteam_name);
                    cmd.Parameters.AddWithValue("secondteam_name", matchesModel.secondteam_name);
                    cmd.Parameters.AddWithValue("loser", matchesModel.loser);
                    cmd.Parameters.AddWithValue("winner", matchesModel.winner);
                    cmd.Parameters.AddWithValue("match_date", matchesModel.match_date);
                    cmd.Parameters.AddWithValue("referee_id", matchesModel.referee_id);
                    cmd.Parameters.AddWithValue("stad_id", matchesModel.stad_id);
                    cmd.Parameters.AddWithValue("sc_id", matchesModel.sc_id);
                    cmd.Parameters.AddWithValue("matchtime", matchesModel.matchtime);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                DataTable tbl = new DataTable();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from MATCHES_VIEW_ALL", con);
                    adapter.Fill(tbl);
                }
                List<MatchesModel> l1 = new List<MatchesModel>();
                _view_helper(l1);
                return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", l1) });
            }
            fill_list(matchesModel);
           
            return Json(new { isValid = false, html = helper.RenderRazorViewToString(this, "Edit", matchesModel) });
        }







        [NoDirectAccess]

        public IActionResult Delete(int? id)
        {
            MatchesModel captain = fetch(id);

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
                SqlCommand cmd = new SqlCommand("MATCHES_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }


            DataTable tbl = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from MATCHES_VIEW_ALL", con);
                adapter.Fill(tbl);
            }
            List<MatchesModel> l1 = new List<MatchesModel>();
            _view_helper(l1);
            return Json(new { isValid = true, html = helper.RenderRazorViewToString(this, "_View", l1) });
        }






        [NonAction]
        public MatchesModel fetch(int? id)
        {
            MatchesModel matches = new MatchesModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                DataTable tbl = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("MATCHES_FETCH_BY_ID", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("id", id);
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 1)
                {
                    matches.match_id = Convert.ToInt32(id);
                    try
                    {
                        matches.firstteam_name = tbl.Rows[0]["firstteam_name"].ToString();
                        matches.secondteam_name = tbl.Rows[0]["secondteam_name"].ToString();
                        matches.loser = tbl.Rows[0]["loser"].ToString();
                        matches.winner = tbl.Rows[0]["winner"].ToString();
                        matches.match_date = Convert.ToDateTime(tbl.Rows[0]["match_date"].ToString());
                        matches.matchtime = tbl.Rows[0]["matchtime"].ToString();

                        if (tbl.Rows[0]["referee_id"].ToString() != "")
                            matches.referee_id = Convert.ToInt32(tbl.Rows[0]["referee_id"].ToString());
                         
                        if (tbl.Rows[0]["stad_id"].ToString() != "")
                            matches.stad_id = Convert.ToInt32(tbl.Rows[0]["stad_id"].ToString());
                        if (tbl.Rows[0]["sc_id"].ToString() != "")
                           matches.sc_id = Convert.ToInt32(tbl.Rows[0]["sc_id"].ToString());
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                return matches;
            }

        }


        [NonAction]

        public void fill_list(MatchesModel matches)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select refree_id,refree_name from referee", con);
                SqlCommand cmd1 = new SqlCommand("select stad_id,stad_name from stadium", con);

                SqlDataAdapter adptr = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();

                 SqlDataAdapter adptr1 = new SqlDataAdapter(cmd1);
                DataTable tbl1 = new DataTable();

                List<RefreesViewModel> l1 = new List<RefreesViewModel>();
                List<SadiumModel> l2 = new List<SadiumModel>();

                adptr.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    RefreesViewModel refrees = new RefreesViewModel();
                    refrees.refree_id= Convert.ToInt32(tbl.Rows[i]["refree_id"].ToString());
                    refrees.refree_name = tbl.Rows[i]["refree_name"].ToString();
                    l1.Add(refrees);
                }
                matches.Refrees = l1;
                //=============================================
                adptr1.Fill(tbl1);

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    SadiumModel sadium = new SadiumModel();
                    sadium.stad_id= Convert.ToInt32(tbl1.Rows[i]["stad_id"].ToString());
                    sadium.stad_name = tbl1.Rows[i]["stad_name"].ToString();
                    l2.Add(sadium);
                }
                matches.Stadiums = l2;

                con.Close();
            }

        }




        public void _view_helper(List<MatchesModel> matches1)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from MATCHES_VIEW_ALL", con);
                adapter.Fill(tbl);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    MatchesModel matches = new MatchesModel();
                    matches.match_id = Convert.ToInt32(tbl.Rows[i]["match_id"].ToString());
                    try
                    {
                        matches.firstteam_name = tbl.Rows[i]["firstteam_name"].ToString();
                        matches.secondteam_name = tbl.Rows[i]["secondteam_name"].ToString();
                        matches.loser = tbl.Rows[i]["loser"].ToString();
                        matches.winner = tbl.Rows[i]["winner"].ToString();
                        matches.match_date = Convert.ToDateTime(tbl.Rows[i]["match_date"].ToString());
                        matches.matchtime = tbl.Rows[i]["matchtime"].ToString();

                        if(tbl.Rows[i]["referee_id"].ToString() != "")
                            matches.referee_id = Convert.ToInt32(tbl.Rows[i]["referee_id"].ToString());
                        if (tbl.Rows[i]["stad_id"].ToString() != "")
                            matches.stad_id = Convert.ToInt32(tbl.Rows[i]["stad_id"].ToString());
                        if (tbl.Rows[i]["sc_id"].ToString() != "")
                            matches.sc_id = Convert.ToInt32(tbl.Rows[i]["sc_id"].ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    matches1.Add(matches);
                }
            }
        }
    }
}
