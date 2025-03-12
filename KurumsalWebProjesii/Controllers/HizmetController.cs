using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesii.Controllers
{
    public class HizmetController : Controller
    {
        // GET: Hizmet

        private KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)] //text area doğrulamasını -> false geç;
        //create actionu:
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            //verileri tabloya atma;
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Hizmet/" + logoname);

                    hizmet.ResimURL = "/Uploads/Hizmet/" + logoname;
                }

                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }
        //düzenleme için metot;
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenicek Hizmet Bulunamadı!";
            }
            var hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }


            return View(hizmet);
        }

        //edit in post action metodu:

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Hizmet hizmet, HttpPostedFileBase ResimURL)
        {

            if (ModelState.IsValid)
            {
                var h = db.Hizmet.Where(x => x.Hizmetıd == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string hizmetname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Hizmet/" + hizmetname);
                    h.ResimURL = "/Uploads/Hizmet/" + hizmetname;
                }

                h.Baslık = hizmet.Baslık;
                h.Acıklama = hizmet.Acıklama;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        //primary key e göre silme gerçekleşir!
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var h = db.Hizmet.Find(id); //url den gelen id yi bul; ->primary keye göre hizmeti bulur!
            if (h == null)
            {
                return HttpNotFound();
            }
            db.Hizmet.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");
  
        }

    }
}