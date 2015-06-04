using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjesi.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult SonBesMakale()
        {
            
            MvcProjesiContext db = new MvcProjesiContext();

            
            List<Makale> makaleListe = db.Makales.OrderByDescending(i => i.Tarih).Take(5).ToList();

           
            return PartialView(makaleListe);
        }
        
        public ActionResult SonBesYorum()
        {
            MvcProjesiContext db = new MvcProjesiContext();

            
            List<Yorum> yorumListe = db.Yorums.OrderByDescending(i => i.Tarih).Take(5).ToList();

           
            return PartialView(yorumListe);
        }
        
        public ActionResult EnCokOnEtiket()
        {
            MvcProjesiContext db = new MvcProjesiContext();

           
            List<Etiket> etiketListe = (from i in db.Etikets orderby i.Makales.Count() descending select i).Take(10).ToList();

            
            return PartialView(etiketListe);
        }
        public ActionResult TumMakaleler()
        {
            MvcProjesiContext db = new MvcProjesiContext();

            
            List<Makale> makaleListe = (from i in db.Makales orderby i.Tarih descending select i).ToList();
            return View(makaleListe);
        }
        public ActionResult TumYorumlar()
        {
            MvcProjesiContext db = new MvcProjesiContext();
            List<Yorum> yorumListe = (from i in db.Yorums orderby i.Tarih descending select i).ToList();
            return View(yorumListe);
        }
        public ActionResult EtiketinMakaleleri(int etiketId)
        {
            MvcProjesiContext db = new MvcProjesiContext();
            var geciciListe = (from i in db.Etikets where i.EtiketId == etiketId select i.Makales).ToList();

            
            return View(geciciListe[0]);
        }
        public ActionResult MakaleDetay(int makaleId)
        {
            MvcProjesiContext db = new MvcProjesiContext();

            
            Makale makale = (from i in db.Makales where i.MakaleId == makaleId select i).SingleOrDefault();
            return View(makale);
        }
        public ActionResult YorumMakalesi(int yorumId)
        {
            MvcProjesiContext db = new MvcProjesiContext();

            
            Makale makale = (from i in db.Yorums where i.YorumId == yorumId select i.Makale).SingleOrDefault();
            return View(makale);
        }
        
    }
}