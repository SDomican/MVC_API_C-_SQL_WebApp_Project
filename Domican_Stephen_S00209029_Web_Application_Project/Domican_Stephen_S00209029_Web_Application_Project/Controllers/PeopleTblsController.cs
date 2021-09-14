using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Domican_Stephen_S00209029_Web_Application_Project.Models;

namespace Domican_Stephen_S00209029_Web_Application_Project.Controllers
{
    public class PeopleTblsController : Controller
    {
        private PeopleAndPlacesDBEntities db = new PeopleAndPlacesDBEntities();

        // GET: PeopleTbls/Names
        public ActionResult Names()
        {
           

            //API's info page says it needs a string, so we create one here.
            string theVaueToDisplay;
           
           //Create a HTTPClient object to connect to the API. Class lets you send/receive HTTP requests/responses.
           HttpClient client = new HttpClient();
           client.BaseAddress = new Uri("http://localhost:56942/api/");
            
          var httpResponseMessage = client.GetAsync("GetNames");

          httpResponseMessage.Wait();

          var responseMessageFromApi = httpResponseMessage.Result;

          if (responseMessageFromApi.IsSuccessStatusCode)
          {
              var taskObjectRepresentingString = responseMessageFromApi.Content.ReadAsAsync<string>();
              taskObjectRepresentingString.Wait();

              theVaueToDisplay = taskObjectRepresentingString.Result;

              //viewbag lets you share info from controller in the view
              ViewBag.InfoFromNameAPI = theVaueToDisplay;
            }
            else
            {
                ViewBag.InfoFromNameAPI = "Error";
                ModelState.AddModelError(string.Empty, "No API available");
            }

            return View();
        }

        // GET: PeopleTbls
        public ActionResult Index()
        {
            return View(db.PeopleTbl.ToList());
        }

        // GET: PeopleTbls/Details/5
        [HandleError]
        public ActionResult Details(int? id)
        {

            if (id <= 0)
            {
                //New code that throws an exception of our choosing.
                throw new Exception("Id exception error");
            }


            if (id == null)
            {
                throw new HttpException(400, "400 error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            PeopleTbl peopleTbl = db.PeopleTbl.Find(id);
            
            if (peopleTbl == null)
            {
                throw new HttpException(404, "404 error");
                //return HttpNotFound();
            }
            return View(peopleTbl);
        }

        // GET: PeopleTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PeopleTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,FirstName,LastName,Profession,Bio")] PeopleTbl peopleTbl, HttpPostedFileBase PersonImageFileInput)
        {
            if (ModelState.IsValid)
            {
                if (PersonImageFileInput != null)
                {
                    peopleTbl.Image = new byte[PersonImageFileInput.ContentLength];
                    PersonImageFileInput.InputStream.Read(peopleTbl.Image, 0, PersonImageFileInput.ContentLength);
                }

                db.PeopleTbl.Add(peopleTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(peopleTbl);
        }

        // GET: PeopleTbls/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                throw new HttpException(400, "400 error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id <= 0)
            {
                throw new HttpException(400, "400 error");
            }

            PeopleTbl peopleTbl = db.PeopleTbl.Find(id);
            if (peopleTbl == null)
            {
                throw new HttpException(404, "404 error");
                //return HttpNotFound();
            }


            return View(peopleTbl);
        }

        // POST: PeopleTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,FirstName,LastName,Profession,Bio")] PeopleTbl peopleTbl, HttpPostedFileBase PersonImageFileInput)
        {
            if (ModelState.IsValid)
            {
                if (PersonImageFileInput != null)
                {
                    peopleTbl.Image = new byte[PersonImageFileInput.ContentLength];
                    PersonImageFileInput.InputStream.Read(peopleTbl.Image, 0, PersonImageFileInput.ContentLength);
                }

                db.Entry(peopleTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(peopleTbl);
        }

        // GET: PeopleTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                throw new HttpException(400, "400 error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (id <= 0)
            {
                throw new HttpException(400, "400 error");
            }

            PeopleTbl peopleTbl = db.PeopleTbl.Find(id);
            if (peopleTbl == null)
            {
                throw new HttpException(404, "404 error");
                //return HttpNotFound();
            }
            return View(peopleTbl);
        }

        // POST: PeopleTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PeopleTbl peopleTbl = db.PeopleTbl.Find(id);
            db.PeopleTbl.Remove(peopleTbl);
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
