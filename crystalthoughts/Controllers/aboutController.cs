using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using crystalthoughts.Models;

namespace crystalthoughts.Controllers
{
    public class aboutController : Controller
    {
        DataSet ds = new DataSet();
        Property p = new Property();
        DLayer dl = new DLayer();
        // GET: about
        public ActionResult Index()
        {
            p.onTable = "Fetch_Aboutus_Data";
            ds = dl.FetchMaster(p);
            var info = new crystalthoughts.Models.aboutus();
            try
            {
                if (ds?.Tables[0].Rows.Count > 0)
                {
                    DataRow i = ds.Tables[0].Rows[0];
                    info.aboutid = i["aboutid"].ToString();
                    info.title = i["title"].ToString();
                    info.image = i["image"].ToString();
                    info.Description = i["Description"].ToString();
                }
            }
            catch (Exception ex)
            { }
            return View(info);
        }
    }
}