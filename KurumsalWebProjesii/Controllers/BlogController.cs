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
    public class BlogController : Controller
    {
        // GET: Blog
        private KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {

              
            return View(db.Blog.ToList().OrderByDescending(x=>x.Id));    
            // blog kayıtlarını liste şekline getir (veritabanı) ->en son yazdığımız kayıda göre sıralar

        }


        //blog ekleme eventi
        public ActionResult Create()
        {
            //kategorilerimiz almamız lazım -> viewBag,viewData -> veri taşıma işlemleri 
            ViewBag.Kategoriıd = new SelectList(db.Kategori,"Kategoriıd","KategoriAd");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        //resim yükleme;
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);
                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);
                blog.ResimURL = "/Uploads/Blog/" + blogimgname;
            }
            if (string.IsNullOrEmpty(blog.Icerik))
            {
                blog.Icerik = "Bu Blog içeriği henüz eklenmedi!";
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //id ye göre düzenleme yapacağız
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            //dışardan gelen id ile blog id kontorolünü sağlar;
            var b = db.Blog.Where(x => x.Id == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ;//Veri taşıma işlemi:
            ViewBag.KategoriId = new SelectList(db.Kategori, "Kategoriıd", "KategoriAd", b.Kategoriıd);
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Blog blog,HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.Id == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL));
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string blogimgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Blog/" + blogimgname);
                    b.ResimURL = "/Uploads/Blog/" + blogimgname;
                }
                b.Baslık = blog.Baslık;
                b.Icerik = string.IsNullOrWhiteSpace(blog.Icerik) ? "Bu blogun içeriği henüz eklenmemiştir." : blog.Icerik;
                b.Kategoriıd = blog.Kategoriıd;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }




        public ActionResult Delete(int id)
        {
            var b = db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL));
            }
            db.Blog.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}