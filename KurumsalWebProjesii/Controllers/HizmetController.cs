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
    public class HizmetController : Controller
    {
        // GET: Hizmet

       private  KurumsalDBContext db = new KurumsalDBContext();
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
        public ActionResult Create(Hizmet hizmet,HttpPostedFileBase ResimURL )
        {
            //verileri tabloya atma;
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoname = ResimURL.FileName + imgInfo.Extension;
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
    }
}