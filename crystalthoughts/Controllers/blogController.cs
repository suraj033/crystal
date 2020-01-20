using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using crystalthoughts.Models;
using crystalthoughts.Helpers;

namespace crystalthoughts.Controllers
{
    public class blogController : Controller
    {
        DataSet ds = new DataSet();
        DLayer dl = new DLayer();
        Property p = new Property();
        EncryptDecrypt enc = new EncryptDecrypt();
        // GET: blog
        public ActionResult Index()
        {
            p.onTable = "Home_Data";
            ds = dl.FetchMaster(p);
            List<blogdata> bloglist = new List<blogdata>();
            try
            {
                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    blogdata cs = new blogdata();
                    cs.blogid = i["blogid"].ToString();
                    cs.title = i["title"].ToString();
                    cs.image = i["image"].ToString();
                    cs.videourl = i["videourl"].ToString();
                    cs.discription = i["discription"].ToString();
                    cs.status = i["status"].ToString();
                    cs.blogdate = Convert.ToDateTime(i["blogdate"]).ToString("dd-MM-yyy");
                    bloglist.Add(cs);
                }
                ViewBag.bloglist = bloglist;
            }
            catch (Exception ex)
            { }
            return View();
        }

        public ActionResult details(string id, string id1)
        {
            var info = new crystalthoughts.Models.blogdata();
            p.Condition1 = id;
            TempData["bloid"] = id;
            TempData["blgurl"] = id1;
            p.onTable = "Home_Data";
            ds = dl.FetchMaster(p);
            try
            {
                if (ds?.Tables[1].Rows.Count > 0)
                {
                    DataRow i = ds.Tables[1].Rows[0];
                    info.blogid = i["blogid"].ToString();
                    info.title = i["title"].ToString();
                    info.videourl = i["videourl"].ToString();
                    info.image = i["image"].ToString();
                    info.discription = i["discription"].ToString();
                    info.status = i["status"].ToString();
                    info.blogdate = Convert.ToDateTime(i["blogdate"]).ToString("dd-MM-yyy");
                }
            }
            catch (Exception ex)
            { }
            p.onTable = "Fetch_Comment_Data";
            ds = dl.FetchMaster(p);
            List<Account_Data> commentlist = new List<Account_Data>();
            try
            {
                foreach (DataRow i in ds.Tables[3].Rows)
                {
                    Account_Data pa = new Account_Data();
                    pa.comment = i["comment"].ToString();
                    pa.Date = Convert.ToDateTime(i["Date"]).ToString("dd-MM-yyy");
                    pa.Name = i["Name"].ToString();
                    commentlist.Add(pa);
                }
                ViewBag.commentlist = commentlist;
            }
            catch (Exception ex)
            { }
            return View(info);
        }

        [HttpPost]
        public ActionResult comment(Account_Data ac)
        {
            HttpCookie loginCookie = Request.Cookies["crystalthoughts"];
            var url = TempData["blgurl"].ToString();
            try
            {
                ac.user_id = enc.Decrypt(loginCookie["Account_id"]).ToString();
                ac.videoid = TempData["bloid"].ToString();
                ac.Status = "false";
                if (dl.Blog_Insertcomment(ac) > 0)
                {
                    TempData["MSG"] = "Thnakyou for Showing Intrest!";
                }
            }
            catch (Exception ex)
            { }
            return Redirect("/blog/details/" + ac.videoid + "/" + url);
        }
    }
}