using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWebProjesii.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        //veri tabanımızı oluşturduk!
        // GET: Home
        public ActionResult Index()
        {

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(X => X.Hizmetıd);
                return View();

        }


        public ActionResult SliderPartial()
        {
            //sliderları en son eklediğimizden başa doğru getirecek!
            return View(db.Slider.ToList().OrderByDescending(x => x.SliderId));

        }

        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToString());
        }


        public ActionResult Hakkimizda()
        {

            var hakkimizda = db.Hakkimizda.SingleOrDefault();
            return View(hakkimizda);
        }
        public ActionResult Hizmetlerimiz()
        {
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.Hizmetıd));
        }


        public ActionResult Iletisim()
        {
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {

            //mail gönderma hatalı -> sor hatayı öğren!
            if (adsoyad != null && email != null && konu != null && mesaj != null)
            {
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.EnableSsl = true;
            WebMail.UserName = "kurumsawebkurumsalweb@gmail.com";
            WebMail.Password = "Kurumsalweb123";
            WebMail.SmtpPort = 587;
            WebMail.Send("kurumsawebkurumsalweb@gmail.com", konu, email + "-" + mesaj);
            ViewBag.Uyari = "Mesajınız başarıyla gönderildi!";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu tekrar deneyiniz!";
            }

            return View();
                
        }

        public ActionResult Blog(int Sayfa = 1)
        {
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.Blogıd).ToPagedList(Sayfa, 5));

        }
        public ActionResult BlogDetay(int id)
        {
            var b = db.Blog.Include("Kategori").Where(x => x.Blogıd == id).SingleOrDefault();
            return View(b);
        }

        public  JsonResult YorumYap(string adsoyad, string eposta, string icerik, int Blogıd)
        {

            if(icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Içerik = icerik, Blogıd = Blogıd, Onay=false });
                db.SaveChanges();
                Response.Redirect("/Home/BlogDetay/" + Blogıd);

                
            }
                return Json(false, JsonRequestBehavior.AllowGet);
            //Json verilerinin alınıp gönderilmesine izin veriyoruz!
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault() ?? new Iletisim();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.Blogıd);
            return PartialView();
        }






    }
}