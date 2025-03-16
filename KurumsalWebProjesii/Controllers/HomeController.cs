using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault() ?? new Iletisim();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.Blogıd);
            return PartialView();
        }





    }
}