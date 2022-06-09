using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class RentalsController : LibraryController
    {

        public ActionResult Index(string error=null)
        {
            if (error != null)
            {
                if (error != "")
                {
                    ViewData["error"] = error;
                }
            }
            return View();
        }
        public void ProcessRental(long bookId, long memberId)
        {
            MembersController membersController = new MembersController();
            bool? gender = null;
            Models.LibraryTableAdapters.QueriesTableAdapter queriesAdapter = new Models.LibraryTableAdapters.QueriesTableAdapter();
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter = new Models.LibraryTableAdapters.BooksTableAdapter();
            Models.LibraryTableAdapters.RentalsTableAdapter rentalsAdapter = new Models.LibraryTableAdapters.RentalsTableAdapter();
            Models.LibraryTableAdapters.MovesTableAdapter movesAdapter = new Models.LibraryTableAdapters.MovesTableAdapter();
            Models.Library.MovesDataTable movesTable = new Models.Library.MovesDataTable();

            string error = "";
            if (membersController.MemberExists(memberId) == false)
            {
                if (Request.Params["gender"] != null)
                {
                    gender = Convert.ToBoolean(Request.Params["gender"]);
                }
                membersController.ProcessNewMember(memberId, Request.Params["memberName"], Request.Params["memberSurname"], Convert.ToInt64(Request.Params["memberPhone"]), Request.Params["memberEMail"], Request.Params["memberAddress"], gender);
            }
            if (rentalsAdapter.CheckRent(memberId, bookId).Value == 0)
            {
                queriesAdapter.AddRental(memberId, bookId, DateTime.Today, DateTime.Today.AddDays(30));

            }
            else
            {
                error = "Bu kitap zaten ödünç verilmiştir.";
            }
            booksAdapter.DecreaseActiveStock(bookId);
            movesAdapter.AddMoves(memberId, bookId, DateTime.Today, true);
            ViewData["moves"] = movesTable.Rows;

            Response.Redirect("~/rentals?error"+error);
        }
        public ActionResult ReturnBook(string bookId)
        {
          return View();
        }
        public ActionResult CurrentRentals(long id)
        {
            Models.LibraryTableAdapters.RentalsTableAdapter rentalsAdapter = new Models.LibraryTableAdapters.RentalsTableAdapter();
            Models.Library.RentalsDataTable rentalsTable = new Models.Library.RentalsDataTable();
            rentalsAdapter.Fill(rentalsTable, id);
            ViewData["rentals"] = rentalsTable.Rows;


            return PartialView();
        }
        public void ProcessReturn(long bookId,long member)
        {
            Models.LibraryTableAdapters.RentalsTableAdapter rentalsAdapter = new Models.LibraryTableAdapters.RentalsTableAdapter();
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter = new Models.LibraryTableAdapters.BooksTableAdapter();
            Models.LibraryTableAdapters.MovesTableAdapter movesAdapter = new Models.LibraryTableAdapters.MovesTableAdapter();
            Models.Library.MovesDataTable movesTable = new Models.Library.MovesDataTable();

            long? delayFee = null;
            long? collectedBy = null;
            DateTime deadline = rentalsAdapter.DeadLine(member, bookId).Value;
            int latency = (DateTime.Today - deadline).Days;
            if(latency > 0)
            {
                delayFee = latency * 50;
                collectedBy = (long)Session["userId"];
            }
            rentalsAdapter.ReturnBook(DateTime.Today,delayFee,collectedBy, member, bookId);
            booksAdapter.IncreaseActiveStock(bookId);
            movesAdapter.AddMoves(member,bookId,DateTime.Today,false);
            ViewData["moves"] = movesTable.Rows;

            Response.Redirect("~/rentals/returnbook");
        }
    }
   
}