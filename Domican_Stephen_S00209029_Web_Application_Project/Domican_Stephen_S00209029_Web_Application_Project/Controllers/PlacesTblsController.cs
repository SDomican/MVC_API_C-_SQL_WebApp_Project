using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Domican_Stephen_S00209029_Web_Application_Project.Models;

namespace Domican_Stephen_S00209029_Web_Application_Project.Controllers
{
    public class PlacesTblsController : Controller
    {
        private PeopleAndPlacesDBEntities db = new PeopleAndPlacesDBEntities();

        // GET: PlacesTbls
        public ActionResult Index()
        {
            return View(db.PlacesTbl.ToList());
        }

        // GET: PlacesTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "400 error");
            }

            if (id <= 0)
            {
                throw new HttpException(400, "400 error");
            }

            PlacesTbl placesTbl = db.PlacesTbl.Find(id);

            if (placesTbl == null)
            {
                throw new HttpException(404, "404 error");
            }

            //Code that requests info from our API is here

            //API's info page says it needs a string, so we create one here.
            string theVaueToDisplay;

            //Create a HTTPClient object to connect to the API. Class lets you send/receive HTTP requests/responses.
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56942/api/");

            var httpResponseMessage = client.GetAsync("GetPlaceInfo/" + placesTbl.Location);

            //Needs to wait for responseMessage thread to work
            httpResponseMessage.Wait();

            var responseMessageFromApi = httpResponseMessage.Result;

            if (responseMessageFromApi.IsSuccessStatusCode)
            {
                var taskObjectRepresentingString = responseMessageFromApi.Content.ReadAsAsync<string>();
                taskObjectRepresentingString.Wait();

                theVaueToDisplay = taskObjectRepresentingString.Result;

                //viewbag lets you share info from controller in the view
                ViewBag.InfoFromAPI = theVaueToDisplay;
            }
            else
            {
                ViewBag.InfoFromAPI = "Error";
                ModelState.AddModelError(string.Empty, "No API available");
            }

            return View(placesTbl);
        }

        // GET: PlacesTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlacesTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlacesID,PlaceName,Location,Notes,PlaceType")] PlacesTbl placesTbl, HttpPostedFileBase PlaceImageFileInput)
        {
            if (ModelState.IsValid)
            {

                if (PlaceImageFileInput != null)
                {
                    placesTbl.Image = new byte[PlaceImageFileInput.ContentLength];
                    PlaceImageFileInput.InputStream.Read(placesTbl.Image, 0, PlaceImageFileInput.ContentLength);
                }


                db.PlacesTbl.Add(placesTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placesTbl);
        }

        // GET: PlacesTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Place not found");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id <= 0)
            {
                throw new HttpException(400, "400 error");
            }

            PlacesTbl placesTbl = db.PlacesTbl.Find(id);
            if (placesTbl == null)
            {
                throw new HttpException(400, "400 error");
                //return HttpNotFound();
            }
            return View(placesTbl);
        }

        // POST: PlacesTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlacesID,PlaceName,Location,Notes,PlaceType")] PlacesTbl placesTbl, HttpPostedFileBase PlaceImageFileInput)
        {
            if (ModelState.IsValid)
            {
                if (PlaceImageFileInput != null)
                {
                    placesTbl.Image = new byte[PlaceImageFileInput.ContentLength];
                    PlaceImageFileInput.InputStream.Read(placesTbl.Image, 0, PlaceImageFileInput.ContentLength);
                }

                db.Entry(placesTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placesTbl);
        }

        // GET: PlacesTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "404 error");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (id <= 0)
            {
                throw new HttpException(400, "400 error");
            }

            PlacesTbl placesTbl = db.PlacesTbl.Find(id);
            if (placesTbl == null)
            {
                throw new HttpException(404, "404 error");
            }
            return View(placesTbl);
        }

        // POST: PlacesTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlacesTbl placesTbl = db.PlacesTbl.Find(id);
            db.PlacesTbl.Remove(placesTbl);
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
