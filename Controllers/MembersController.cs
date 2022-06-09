using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace Library.Controllers
{
    public class MembersController : LibraryController
    {
        // GET: Members
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            Models.Library.MembersDataTable membersTable = new Models.Library.MembersDataTable();

            membersAdapter.Fill(membersTable);
            ViewData["members"] = membersTable.Rows;
            ViewData["title"] = "Üyeler";
            return View();
           
        }
        public ActionResult Update(long id, string error = null)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            Models.Library.MembersDataTable membersTable = new Models.Library.MembersDataTable();
            Models.Library.MembersRow memberRow;
            membersAdapter.FillById(membersTable, id);
            memberRow=(Models.Library.MembersRow)membersTable.Rows[0];
            ViewData["title"] = memberRow.MemberName + " " + memberRow.MemberSurname; 
            ViewData["memberRow"] = memberRow;
            if(error != null)
            {
                ViewData["error"] = error;
            }

            return View();
        }
        public void ProcessMember(long originalId ,long memberId,string name ,string surname, long phone ,string eMail , string address, bool banned=false,bool? gender=null)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter;
            if (memberId.ToString().Length != 11)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if(name== null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            name= name.Trim();
            if (name == "")
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (name.Length > 50)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (surname == null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            surname = surname.Trim();
            if (name == "")
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (surname.Length > 50)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (phone.ToString().Length != 10)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (eMail == null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            eMail= eMail.Trim();
            if(eMail.Length > 100)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            try
            {
                membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
                membersAdapter.UpdateMembers(memberId, banned , phone, gender, name, surname,eMail,address,originalId);
                Response.Redirect("~/members");
            }
            catch
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
            }
        }
        public ActionResult MemberData(long id)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            Models.Library.MembersDataTable membersTable = new Models.Library.MembersDataTable();

            membersAdapter.FillById(membersTable, id);
            if (membersTable.Rows.Count > 0)
            {
                ViewData["memberRow"] = (Models.Library.MembersRow)membersTable.Rows[0];
            }
           

            return PartialView();
        }
        public bool MemberExists(long id)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            int memberCount = membersAdapter.MemberCount(id).Value;

            if(memberCount==0)
            { 
                return false; 
            }

            return true;
        }
        public void ProcessNewMember(long id, string memberName, string memberSurname, long memberPhone, string memberEMail,string memberAddress,bool? gender)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            membersAdapter.AddMember(id,memberName,memberSurname,memberPhone,memberEMail,gender, memberAddress);
        }
    }
}