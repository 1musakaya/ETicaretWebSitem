using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETicaretWebSitem.Models;

namespace ETicaretWebSitem.Controllers
{
    public class UrunlerController : Controller
    {
        private ETicaretEntities db = new ETicaretEntities();

        // GET: Urunlers
        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kategoriler);
            return View(urunler.ToList());
        }

        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAD");
            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urunler urunler, HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();

                if (urunResim != null)
                {
                    string dosya = Path.Combine(Server.MapPath("~/Resim/"), urunler.UrunID + ".jpg");
                    urunResim.SaveAs(dosya);
                }

                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAD", urunler.KategoriID);
            return View(urunler);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Urunler urunler, HttpPostedFileBase urunResim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                if (urunResim != null)
                {
                    string dosya = Path.Combine(Server.MapPath("~/Resim/"), urunler.UrunID + ".jpg");
                    urunResim.SaveAs(dosya);
                }
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
            string dosya = Path.Combine(Server.MapPath("~/Resim/"), id + ".jpg");

            FileInfo fi = new FileInfo(dosya);

            if (fi.Exists)
            {
                fi.Delete();
            }

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
