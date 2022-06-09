using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BooksController : LibraryController
    {
        // GET: Books
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter=new Models.LibraryTableAdapters.BooksTableAdapter();
            Models.Library.BooksDataTable booksTable=new Models.Library.BooksDataTable();

            booksAdapter.Fill(booksTable);
            ViewData["books"] = booksTable.Rows;
            ViewData["title"] = "Kitaplar";
            return View();
        }
    }
}