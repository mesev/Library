using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace Library.Controllers
{
    public class UsersController : LibraryController
    {
        // GET: Users
        public ActionResult Index()
        {
           

            Models.LibraryTableAdapters.UserListTableAdapter userListAdapter = new Models.LibraryTableAdapters.UserListTableAdapter();
            Models.Library.UserListDataTable userListTable = new Models.Library.UserListDataTable();

            userListAdapter.Fill(userListTable);
            ViewData["userList"] = userListTable.Rows;
            ViewData["title"] = "Kullanıcılar";
            return View();
        }


    }
}