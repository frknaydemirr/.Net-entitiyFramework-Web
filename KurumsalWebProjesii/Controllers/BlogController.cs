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

              
            return View(db.Blog.ToList().OrderByDescending(x=>x.Blogıd));    
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
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}