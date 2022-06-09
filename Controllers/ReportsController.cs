using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class ReportsController : LibraryController
    {
        // GET: Reports
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult ProcessPayment(string startDate,string endDate)
        {
            DateTime reportStart = DateTime.ParseExact(startDate, "dd-MM-yyyy", null);
            DateTime reportEnd = DateTime.ParseExact(endDate, "dd-MM-yyyy", null);
            Models.LibraryTableAdapters.PaymentTableAdapter paymentAdapter = new Models.LibraryTableAdapters.PaymentTableAdapter();
            Models.Library.PaymentDataTable paymentTable = new Models.Library.PaymentDataTable();
            paymentAdapter.Fill(paymentTable, reportStart, reportEnd);
            ViewData["payment"] = paymentTable.Rows;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
 
            return View();
        }
        public ActionResult Receive()
        {
         
            Models.LibraryTableAdapters.ReceiveTableAdapter receiveAdapter = new Models.LibraryTableAdapters.ReceiveTableAdapter();
            Models.Library.ReceiveDataTable receiveTable = new Models.Library.ReceiveDataTable();
            receiveAdapter.Fill(receiveTable);
            ViewData["receiveRow"] = receiveTable.Rows;
            return View();
        }
        public ActionResult BookControl()
        {
            Models.LibraryTableAdapters.BooksReportsTableAdapter booksReportsAdapter = new Models.LibraryTableAdapters.BooksReportsTableAdapter();
            Models.Library.BooksReportsDataTable booksReportsData = new Models.Library.BooksReportsDataTable();
            booksReportsAdapter.Fill(booksReportsData);
            ViewData["bookReports"]=booksReportsData.Rows;
            return View();

        }
        public ActionResult BookLoad(long id)
        {
            Models.LibraryTableAdapters.ReceiveTableAdapter receiveAdapter = new Models.LibraryTableAdapters.ReceiveTableAdapter();
            Models.Library.ReceiveDataTable receiveTable = new Models.Library.ReceiveDataTable();
            receiveAdapter.FillById(receiveTable, id);
            ViewData["bookLoad"]=receiveTable.Rows;

            return PartialView();
        }
        public ActionResult BookLate()
        {
            Models.LibraryTableAdapters.ReceiveTableAdapter receiveAdapter = new Models.LibraryTableAdapters.ReceiveTableAdapter();
            Models.Library.ReceiveDataTable receiveTable = new Models.Library.ReceiveDataTable();
            receiveAdapter.Fill(receiveTable);
            ViewData["receiveRow"] = receiveTable.Rows;
            return View();
        }
        public ActionResult MovesReport()
        {
            

            return View();
        }
        public ActionResult ProcessMoves(string startDate, string endDate)
        {

            if(startDate == "")
            {
                startDate = "01-01-1900";
            }
            if (endDate == "")
            {
                endDate = "01-01-2250";

            }
            DateTime reportStart = DateTime.ParseExact(startDate, "dd-MM-yyyy", null);
                DateTime reportEnd = DateTime.ParseExact(endDate, "dd-MM-yyyy", null);
                Models.LibraryTableAdapters.MovesTableAdapter movesAdapter = new Models.LibraryTableAdapters.MovesTableAdapter();
                Models.Library.MovesDataTable movesTable = new Models.Library.MovesDataTable();

                movesAdapter.Fill(movesTable, reportStart, reportEnd);
                ViewData["moves"] = movesTable.Rows;
            if(startDate== "01-01-1900")
            {
                startDate = "";
            }
            if(endDate== "01-01-2250")
            {
                endDate = "";
            }
                ViewData["startDate"] = startDate;
                ViewData["endDate"] = endDate;
           
            return PartialView();
        }
       
    }
}