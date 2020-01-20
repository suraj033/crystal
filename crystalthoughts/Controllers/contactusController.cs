using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3.Data;
using crystalthoughts.Models;
using System.Data; 
namespace crystalthoughts.Controllers
{
    public class contactusController : Controller
    {
        DataSet Ds = new DataSet();
        DLayer dl = new DLayer();
        Property p = new Property();
        // GET: contactus
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Contectusdata co)
        {
            try
            {
                if(dl.InsertContactus(co)>0)
                {
                    TempData["MSG"] = "Your Message Send!";
                }
            }
            catch (Exception ex)
            {}
            return Redirect("/contactus");
        }
    }
}