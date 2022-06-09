using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string keywords = null)
        {
            Models.LibraryTableAdapters.searchResultsTableAdapter searchAdapter = new Models.LibraryTableAdapters.searchResultsTableAdapter();
            Models.Library.searchResultsDataTable searchResultsTable = new Models.Library.searchResultsDataTable();
            if(keywords != null)
            {
                searchAdapter.Fill(searchResultsTable,keywords);
                ViewData["results"] = searchResultsTable.Rows;
            }
            return View();
        }

        public ActionResult Login(string error = null)
        {
            if(error!= null)
            {
                ViewData["error"]=error;
            }
            return View();
        }
        public void  CheckLogin(string userName,string password)
        {
            Models.LibraryTableAdapters.UsersTableAdapter usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();
            Models.Library.UsersDataTable usersTable = new Models.Library.UsersDataTable();
          
            string hashedStr;
            Models.Library.UsersRow userRow;

            usersAdapter.Fill(usersTable, userName);
            if(usersTable.Rows.Count!=1)
            {
                Response.Redirect("~/home/login?error=Bilinmeyen Kullanıcı");
                //bilinmeyen kullanıcı
                return;
            }
            userRow=(Models.Library.UsersRow)usersTable.Rows[0];
         
                hashedStr=Tools.CalculateSHA(userName,password);
            if (hashedStr != userRow.password.ToLower()) 
            {
                Response.Redirect("~/home/login?error=Hatalı Şifre");
                //hatalı şifre
                return;
            }
            Session["userId"] = userRow.id;  
            Response.Redirect("~/main",false);


        }
    }
}