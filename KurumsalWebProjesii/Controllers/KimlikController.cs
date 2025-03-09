using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesii.Controllers
{
    public class KimlikController : Controller
    {

        //veritabanımızın örneğini aldık!
        //Bu kısım, Kimlik verilerini yöneten Controller sınıfıdır.
        KurumsalDBContext db = new KurumsalDBContext();

        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
            //Kimlik tablosundaki tüm verileri çekip Index sayfasına gönderiyor.
        }


        // GET: Kimlik/Edit/5
        //add view diyip veri tabanı işlemini gerçekleştirmek için uyguladık!!!
        public ActionResult Edit(int id)
        {
            //entity framework
            var kimlik = db.Kimlik.Where(x => x.Kimlikıd == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Where(x => x.Kimlikıd == id).SingleOrDefault();

                //daha önce kaydettiğimiz bir dosya var mı onu kontrol edeceğiz:
                if (LogoURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.LogoURL));
                    }
                    //yoksa resim yükleme işlemini yap:
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName);

                    string Logoname = imginfo.FullName + imginfo.Extension ;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Kimlik/" + Logoname);
                    k.LogoURL = "/Uploads/Kimlik/" + Logoname;


                }
                k.Title = kimlik.Title;
                k.KeyWords = kimlik.KeyWords;
                k.Description = kimlik.Description;
                k.Unvan = kimlik.Unvan;
                int v = db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(kimlik);
        }
    }
}
