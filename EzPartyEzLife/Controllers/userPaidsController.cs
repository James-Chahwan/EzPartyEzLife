using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EzPartyEzLife.Models;
using ezpartyezLife.Models;
using System.Data.SqlClient;

namespace EzPartyEzLife.Controllers
{
    public class userPaidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: userPaids
        public ActionResult Index()
        {

            SqlConnection sqlConnection1;
            DataSet resultSet;
            gettheTableData(out sqlConnection1, out resultSet);
            sqlConnection1.Close();
            var FaceID = resultSet.Tables[0].Rows[0].ItemArray.ToArray()[13].ToString();
            var userPaids = db.userPaids.Where(u => u.FBID == FaceID);
            return View(userPaids.ToList());
        }


        private static void gettheTableData(out SqlConnection sqlConnection1, out DataSet resultSet)
        {
            var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            sqlConnection1 = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();




            cmd.CommandText = "SELECT * FROM [dbo].AspNetUsers";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            var adapter = new SqlDataAdapter(cmd);
            resultSet = new DataSet();
            adapter.Fill(resultSet);
        }
        // GET: userPaids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userPaid userPaid = db.userPaids.Find(id);
            if (userPaid == null)
            {
                return HttpNotFound();
            }
            return View(userPaid);
        }

        // GET: userPaids/Create
        public ActionResult Create()
        {
            ViewBag.partyID = new SelectList(db.parties, "id", "AdminID");
            return View();
        }

        // POST: userPaids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FBID,haspaid,ammount,partyID")] userPaid userPaid)
        {
            if (ModelState.IsValid)
            {
                db.userPaids.Add(userPaid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.partyID = new SelectList(db.parties, "id", "AdminID", userPaid.partyID);
            return View(userPaid);
        }

        // GET: userPaids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userPaid userPaid = db.userPaids.Find(id);
            if (userPaid == null)
            {
                return HttpNotFound();
            }
            ViewBag.partyID = new SelectList(db.parties, "id", "AdminID", userPaid.partyID);
            return View(userPaid);
        }

        // POST: userPaids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FBID,haspaid,ammount,partyID")] userPaid userPaid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPaid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.partyID = new SelectList(db.parties, "id", "AdminID", userPaid.partyID);
            return View(userPaid);
        }

        // GET: userPaids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userPaid userPaid = db.userPaids.Find(id);
            if (userPaid == null)
            {
                return HttpNotFound();
            }
            return View(userPaid);
        }

        // POST: userPaids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userPaid userPaid = db.userPaids.Find(id);
            db.userPaids.Remove(userPaid);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
