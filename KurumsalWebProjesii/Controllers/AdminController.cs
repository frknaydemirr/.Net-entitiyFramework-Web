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
        public ActionResult Login(Admin admin,string sifre) //admin modeli alır parametre;
        {
            //şifreyi md5 e çevşrme olayını sor -> güncelleme yaptıktan sonra giriş yap diyince girmiyor!
            var md5pas = Crypto.Hash(sifre, "MD5");
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).FirstOrDefault();

            if (login != null && login.Eposta == admin.Eposta && login.Sifre==admin.Sifre  /*&& login.Sifre==md5pas*/)
            {
                //oturum değişkeni oluşturma:
                Session["adminid"] = login.Adminıd;
                Session["eposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;
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


        //şifre unutma:
        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            var mail = db.Admin.Where(x => x.Eposta == eposta).SingleOrDefault();
            if (mail!=null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                Admin admin = new Admin();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsawebkurumsalweb@gmail.com";
                WebMail.Password = "Kurumsalweb123";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta,"Admin Panel Giriş Şifreniz","Şifreniz : " + yenisifre);
                ViewBag.Uyari = "Mesajınız başarıyla gönderildi!";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu tekrar deneyiniz!";
            }

            return View();
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


        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.Adminıd == id).SingleOrDefault();
            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(int id,Admin admin,string sifre , string eposta)
        {

            if (ModelState.IsValid){

                var a = db.Admin.Where(x => x.Adminıd == id).SingleOrDefault();
                a.Sifre = Crypto.Hash("sifre", "MD5");
                a.Eposta = admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }

            return View(admin);
        }
        public ActionResult Delete(int id)
        {
            var a = db.Admin.Where(x => x.Adminıd == id).SingleOrDefault();
            if (a != null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View();
        }

    }

    
}