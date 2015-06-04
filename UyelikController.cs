using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjesi.Controllers
{
    public class UyelikController : Controller
    {
        public ActionResult YeniUyelik()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniUyelik(MvcProjesi.Data.Uye model, string textBoxDogum, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (String.IsNullOrEmpty(textBoxDogum))
            {
               
                ModelState.AddModelError("textBoxDogum", "Doğum tarihi boş geçilemez");

                
                return View();
            }
            int yil = int.Parse(textBoxDogum.Substring(6));
            if (yil > 2002)
            {
                ModelState.AddModelError("textBoxDogum", "Yaşınız 12 den küçük olamaz");
                return View();
            }
            MvcProjesi.Data.Uye uye = new MvcProjesi.Data.Uye();
            if (file != null)
            {
                
                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                uye.ResimYol = "/Images/" + file.FileName;
            }
            uye.Ad = model.Ad;
            uye.EPosta = model.EPosta;
            uye.Soyad = model.Soyad;
            uye.UyeOlmaTarih = DateTime.Now;
            uye.WebSite = model.WebSite;
            uye.Sifre = model.Sifre;
            using (MvcProjesiContext db = new MvcProjesiContext())
            {
                db.Uyes.Add(uye);
                db.SaveChanges();

                
                return RedirectToAction("UyelikBasarili");
            }
        }

        public ActionResult UyelikBasarili()
        {
            return View();
        }





        public ActionResult UyeGiris()
        {

            return View();
        }

        [HttpPost]
       public string UyeGirisi()
       {
           
           string posta = Request.Form["txtPosta"];
           string sifre = Request.Form["pswSifre"];
           if (String.IsNullOrEmpty(posta) && String.IsNullOrEmpty(sifre))
           {
               return "E-Posta adresinizi ve şifrenizi girmediniz.";
           }
           else if (String.IsNullOrEmpty(posta))
           {
               return "E-posta adresinizi girmediniz.";
           }
           else if (string.IsNullOrEmpty(sifre))
           {
               return "Şifrenizi girmediniz.";
           }
           else
           {
               using (MvcProjesiContext db = new MvcProjesiContext())
               {
                   
                   var uye = (from i in db.Uyes where i.Sifre == sifre && i.EPosta == posta select i).SingleOrDefault();
 
                   if (uye == null) return "Kullanıcı adınızı ya da şifreyi hatalı girdiniz.";
 
                   
                   Session["uyeId"] = uye.UyeId;
 
                  
                   return "Sisteme, başarıyla giriş yaptınız.<script type='text/javascript'>setTimeout(function(){window.location='/'},3000);</script>";
               }
           }
       }













    }

}