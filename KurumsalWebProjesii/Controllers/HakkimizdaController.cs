using KurumsalWebProjesii.Models.DataContext;
using KurumsalWebProjesii.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//controllers altından controllers ekleyip veri tabanına bağlantıyı sağladık!
namespace KurumsalWebProjesii.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        public ActionResult Index()
        {
            //veri tabanımızdaki tablomuzdan verilerimizi alıyoruz (çektik)!
            var h = db.Hakkimizda.ToList();
            return View(h); //çekilen veriler index cshtml e gönderiliyor... ->read okuma işlemi
            //indexe tıklayarak  -> add view la   verileri görüntülemek için HTML sayfası 
        }

        //update işlemi için:

        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizda.Where(x => x.Hakkimizdaıd == id).FirstOrDefault();
            return View(h);
        }

        //http post action u için metot:
        [HttpPost]
        [ValidateAntiForgeryToken] //güvenli bir şekilde veri gönderilmesini sağlarız!
        [ValidateInput(false)]
        public ActionResult Edit (int id,Hakkimizda h)
        {
            if (ModelState.IsValid)
            {            
            var hakkimizda = db.Hakkimizda.Where(x => x.Hakkimizdaıd == id).SingleOrDefault();

                hakkimizda.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
                //güncelleme işlemini sağlayalım ve veri tabanına işlemimizi kaydedelim!
            }

            return View(h);
        }
    }
}