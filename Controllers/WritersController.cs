using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class WritersController : LibraryController
    {
        // GET: Writers
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter =new Models.LibraryTableAdapters.WritersTableAdapter();
            Models.Library.WritersDataTable writersTable=new Models.Library.WritersDataTable();

            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;
            ViewData["title"] = "Yazarlar";
            return View();
        }
    }
}