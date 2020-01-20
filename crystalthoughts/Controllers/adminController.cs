using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using crystalthoughts.Helpers;
using crystalthoughts.Models;

namespace crystalthoughts.Controllers
{
    public class adminController : Controller
    {
        DataSet ds = new DataSet();
        DLayer dl = new DLayer();
        Property p = new Property();
        EncryptDecrypt enc = new EncryptDecrypt();
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult commentlist()
        {

            p.onTable = "Fetch_Comment_Data";
            ds = dl.FetchMaster(p);
            List<Account_Data> commentlist = new List<Account_Data>();
            try
            {
                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    Account_Data pa = new Account_Data();
                    pa.comment_id = i["comment_id"].ToString();
                    pa.user_id = i["user_id"].ToString();
                    pa.comment = i["comment"].ToString();
                    pa.Date = Convert.ToDateTime(i["Date"]).ToString("dd-MM-yyy");
                    pa.videoid = "https://www.youtube.com/embed/" + i["videoid"].ToString();
                    pa.Status = i["Status"].ToString();
                    pa.Name = i["Name"].ToString();
                    commentlist.Add(pa);
                }
                ViewBag.commentlist = commentlist;
            }
            catch (Exception ex)
            { }
            return View();
        }
        public ActionResult enquiry()
        {
            p.onTable = "Fetch_Contactus_Data";
            ds = dl.FetchMaster(p);
            List<Contectusdata> contactuslist = new List<Contectusdata>();
            try
            {
                foreach (DataRow i in ds.Tables[0].Rows)
                {
                    Contectusdata cs = new Contectusdata();
                    cs.Contectid = i["Contectid"].ToString();
                    cs.Name = i["Name"].ToString();
                    cs.Email = i["Email"].ToString();
                    cs.Subject = i["Subject"].ToString();
                    cs.Message = i["Message"].ToString();
                    cs.ContectDate = Convert.ToDateTime(i["ContectDate"]).ToString("dd-MM-yyy");
                    contactuslist.Add(cs);
                }
                ViewBag.contactuslist = contactuslist;
            }
            catch (Exception ex)
            { }
            return View();
        }
        public ActionResult activecomment(string id)
        {
            try
            {
                p.Condition1 = id;
                p.onTable = "update_Comment_Data";
                ds = dl.FetchMaster(p);
                TempData["MSG"] = "Comment Activated!";
            }
            catch (Exception ex)
            { }
            return Redirect("/admin/commentlist");
        }
        public ActionResult deactivecomment(string id)
        {
            try
            {
                p.Condition1 = id;
                p.onTable = "update_Comment_Data_Deactive";
                ds = dl.FetchMaster(p);
            }
            catch (Exception ex)
            { }
            return Redirect("/admin/commentlist");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            HttpCookie loginCookie = Request.Cookies["crystalthoughts"];
            if (loginCookie != null)
            {
                loginCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(loginCookie);
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return Redirect("/");
        }
        public ActionResult aboutus()
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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult aboutus(aboutus abs, List<HttpPostedFileBase> f1)
        {
            string fileLocation = "";
            string ItemUploadFolderPath = "~/DataFile/";
            foreach (HttpPostedFileBase file in f1)
            {
                try
                {
                    if (file.ContentLength == 0)
                        continue;

                    if (file.ContentLength > 0)
                    {
                        fileLocation = HelperFunctions.renameUploadFile(file, ItemUploadFolderPath);
                        if (fileLocation == "")
                        {
                            fileLocation = "";
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                if (fileLocation == "" || fileLocation == null)
                {
                    abs.image = abs.image;
                }
                else
                {
                    abs.image = fileLocation;
                }
            }
            try
            {
                if (dl.Insertaboutus(abs) > 0)
                {
                    TempData["MSG"] = "Data Saved!";
                }
            }
            catch (Exception ex)
            {
                return Redirect("/admin");
            }
            return Redirect("/admin/aboutus");
        }
        public ActionResult blog(string id)
        {
            p.Condition1 = id;
            p.onTable = "Fetch_Blog_Data";
            ds = dl.FetchMaster(p);
            var info = new crystalthoughts.Models.blogdata();
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
                }
            }
            catch (Exception ex)
            { }
            return View(info);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult blog(blogdata bg,List<HttpPostedFileBase> f1)
        {
           
            string fileLocation = "";
            string ItemUploadFolderPath = "~/DataFile/";
            foreach (HttpPostedFileBase file in f1)
            {
                try
                {
                    if (file.ContentLength == 0)
                        continue;

                    if (file.ContentLength > 0)
                    {
                        fileLocation = HelperFunctions.renameUploadFile(file, ItemUploadFolderPath);
                        if (fileLocation == "")
                        {
                            fileLocation = "";
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                if (fileLocation == "" || fileLocation == null)
                {
                    bg.image = bg.image;
                }
                else
                {
                    bg.image = fileLocation;
                }
            }
            try
            {

                if(dl.Insertblog(bg)>0)
                {
                    TempData["Msg"] = "Blog Saved!";
                }
            }
            catch (Exception ex)
            {
                return Redirect("/admin/blog");
            }
            return Redirect("/admin/blog");
        }
        public ActionResult bloglist()
        {
            p.onTable = "Fetch_Blog_Data";
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

        public ActionResult blogcommentlist()
        {
            p.onTable = "Fetch_Comment_Data";
            ds = dl.FetchMaster(p);
            List<Account_Data> commentlist = new List<Account_Data>();
            try
            {
                foreach (DataRow i in ds.Tables[2].Rows)
                {
                    Account_Data pa = new Account_Data();
                    pa.comment_id = i["comment_id"].ToString();
                    pa.user_id = i["user_id"].ToString();
                    pa.comment = i["comment"].ToString();
                    pa.Date = Convert.ToDateTime(i["Date"]).ToString("dd-MM-yyy");                  
                    pa.Status = i["Status"].ToString();
                    pa.Name = i["Name"].ToString();
                    commentlist.Add(pa);
                }
                ViewBag.commentlist = commentlist;
            }
            catch (Exception ex)
            { }
            return View();
        }


        public ActionResult activeblogcomment(string id)
        {
            try
            {
                p.Condition1 = id;
                p.onTable = "update_blogComment_Data";
                ds = dl.FetchMaster(p);
                TempData["MSG"] = "Comment Activated!";
            }
            catch (Exception ex)
            { }
            return Redirect("/admin/blogcommentlist");
        }
        public ActionResult deactiveblogcomment(string id)
        {
            try
            {
                p.Condition1 = id;
                p.onTable = "update_blogComment_Data_Deactive";
                ds = dl.FetchMaster(p);
            }
            catch (Exception ex)
            { }
            return Redirect("/admin/blogcommentlist");
        }
    }
}