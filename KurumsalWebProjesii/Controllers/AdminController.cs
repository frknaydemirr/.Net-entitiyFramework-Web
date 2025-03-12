using KurumsalWebProjesii.Models;
using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost] //login de form post olacak
        public ActionResult Login(Admin admin) //admin modeli alır parametre;
        {
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();
            if ( login !=null && login.Eposta == admin.Eposta && login.Sifre==admin.Sifre)
            {
                //oturum değişkeni oluşturma:
                Session["adminid"] = login.Adminıd;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index","Admin");
            }
            ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış!";
            return View(admin);

        }
    }
    
}