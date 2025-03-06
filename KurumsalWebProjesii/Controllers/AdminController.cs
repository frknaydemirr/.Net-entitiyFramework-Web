using KurumsalWebProjesii.Models;
using KurumsalWebProjesii.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWebProjesii.Controllers
{
    public class AdminController : Controller
    {



        //veri tabanına erişmek için: -> DİĞER YOL (VERİ TABANINDAKŞİ TABLOLARIN CLASS KODLARI YOKKEN)
        // KurumsalDBEntities db = new KurumsalDBEntities();

        // GET: Admin

        KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            var sorgu = db.Kategori.ToList();

            return View(sorgu);
        }
    }
}