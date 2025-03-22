using KurumsalWebProjesii.Models;
using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesii.Controllers
{
    public class AdminController : Controller
    {



        //veri tabanına erişmek için: -> DİĞER YOL (VERİ TABANINDAKŞİ TABLOLARIN CLASS KODLARI YOKKEN)
        // KurumsalDBEntities db = new KurumsalDBEntities();

        // GET: Admin

        KurumsalDBContext db = new KurumsalDBContext();

        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            var sorgu = db.Kategori.ToList();
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.HizmetSay = db.Hizmet.Count();
            return View(sorgu);
        }

        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost] //login de form post olacak
        public ActionResult Login(Admin admin) //admin modeli alır parametre;
        {
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();
            if (login != null && login.Eposta == admin.Eposta && login.Sifre==admin.Sifre)
            {
                //oturum değişkeni oluşturma:
                Session["adminid"] = login.Adminıd;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index","Admin");
            }
            ViewBag.Uyari = "Kullanıcı adı yada şifre yanlış!";
            return View(admin);

        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon(); //sessionları düşürme kısmı:
            return RedirectToAction("Login/Admin");
            
        }

        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin,string sifre, string eposta) //yeni admin oluşturma-> [PostAction]
        {

            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }


    }
    
}