using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrencıNotMvc.Models.Entity_Framework;
using OgrencıNotMvc.Models;

namespace OgrencıNotMvc.Controllers
{
    public class SinavlarController : Controller
    {
        // GET: Sinavlar
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var notlar = db.TBLNOTLAR.ToList();
            return View(notlar);
        }
        [HttpGet]
        public ActionResult SinavEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SinavEkle(TBLNOTLAR p)
        {
            db.TBLNOTLAR.Add(p);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult SinavGetir(int id)
        {
            var sinav = db.TBLNOTLAR.Find(id);

            return View("SinavGetir", sinav);
        }
        [HttpPost]
        public ActionResult SinavGetir(Class1 model, TBLNOTLAR p, int SINAV1=0, int SINAV2=0, int SINAV3=0, int PROJE=0)
        {
            if(model.islem=="HESAPLA")
            {
                int ortalama = (SINAV1+SINAV2+SINAV3+PROJE) / 4;
                ViewBag.ort = ortalama;
            }
            if (model.islem=="NOTGUNCELLE")
            {
                var snv = db.TBLNOTLAR.Find(p.NOTID);
                snv.SINAV1 = p.SINAV1;
                snv.SINAV2 = p.SINAV2;
                snv.SINAV3 = p.SINAV3;
                snv.PROJE = p.PROJE;
                snv.ORTALAMA = p.ORTALAMA;
                db.SaveChanges();
                return RedirectToAction("Index", "Sinavlar");

            }
            return View();
        }
    }
}