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
using RestSharp;
using System.Data.SqlClient;
using Facebook;
using Newtonsoft.Json;

namespace EzPartyEzLife.Controllers
{
    public class partiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: parties
        public ActionResult Index()
        {
           // var found = db.Users.Where(r => r.UserName == User.Identity.Name).ToArray();
            return View(db.parties.ToList());
        }

        // GET: parties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            party party = db.parties.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // GET: parties/Create
        public ActionResult CreateParty()
        {
            return View();
        }


        // POST: parties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateParty(supParty superP)
        {
            
            SqlConnection sqlConnection1;
            DataSet resultSet;
            gettheTableData(out sqlConnection1, out resultSet);

            //resultSet.Tables[0].Rows[0].ItemArray.ToArray()[11]
            // Data is accessible through the DataReader object here.


            sqlConnection1.Close();
            //var found = db.Users.Where(r => r.UserName == User.Identity.Name).ToArray();
            var client = new FacebookClient(resultSet.Tables[0].Rows[0].ItemArray.ToArray()[12].ToString());
            dynamic me = client.Get("me");
            dynamic perm = client.Get(resultSet.Tables[0].Rows[0].ItemArray.ToArray()[13].ToString() + "/permissions");
            dynamic EventList = client.Get("940972469274337/attending");// replace with param
            dynamic Events = client.Get("940972469274337");

            // make the party insert all values 
            party curparty = new party { details = Events[0], name = Events[3], AdminID = Events[4][1], eventID = superP.eventID, totalcost = superP.total };
            db.parties.Add(curparty);
            Console.WriteLine(EventList);
            string aboutMe = me.about;

            decimal perPerson = (curparty.totalcost / EventList.Count);
           // get all the people to pay yo
            for (int i = 0; i < EventList.Count ; i++)
            {

                Datum d1 = new Datum(EventList[0][i][0], EventList[0][i][1], EventList[0][i][2]);
                db.userPaids.Add(new userPaid { FBID = d1.rsvp_status, ammount = perPerson, partyID = curparty.id, party = curparty, haspaid = false }); // don't fucking ask about rsvp
            }

            //db.parties.Add();
             db.SaveChanges();
           return RedirectToAction("Index");
           

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

        public class Datum
        {
            public Datum(string id, string name, string rsvp)
            {
                this.id = id;
                this.name = name;
                this.rsvp_status = rsvp;
            }
            public string id { get; set; }
            public string name { get; set; }
            public string rsvp_status { get; set; }
        }



        // GET: parties/Create
        //public ActionResult CreateParty()
        //{
        //    return View();
        //}

        // POST: parties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.







        // GET: parties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            party party = db.parties.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // POST: parties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,eventID,name,details,totalcost,AdminID")] party party)
        {
            if (ModelState.IsValid)
            {
                db.Entry(party).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(party);
        }

        // GET: parties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            party party = db.parties.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // POST: parties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            party party = db.parties.Find(id);
            db.parties.Remove(party);
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
