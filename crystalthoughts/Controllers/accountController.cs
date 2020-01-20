using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crystalthoughts.Models;
using System.Data;
using crystalthoughts.Helpers;
namespace crystalthoughts.Controllers
{
    public class accountController : Controller
    {
        Property p = new Property();
        DataSet ds = new DataSet();
        DLayer dl = new DLayer();
        EncryptDecrypt enc = new EncryptDecrypt();
        // GET: account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult login(string id, string id1, string id2)
        {
            TempData["mi"] = id;
            TempData["blogid"] = id1;
            TempData["blogurl"] = id2;
            return View();
        }
        [HttpPost]
        public ActionResult login(Account_Data Au)
        {
            var videoid = TempData["mi"];
            var bloid = TempData["blogid"];
            var urllink = TempData["blogurl"];
            p.Condition1 = Au.Email;
            p.Condition2 = Au.Paasword;
            p.onTable = "Fetch_Login";
            ds = dl.FetchMaster(p);
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HttpCookie loginCookie = Request.Cookies["crystalthoughts"];
                    String Type = ds.Tables[0].Rows[0]["UserType"].ToString();
                    String Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    if (Status == "True")
                    {
                        loginCookie = new HttpCookie("crystalthoughts");
                        loginCookie["Account_id"] = enc.Encrypt(ds.Tables[0].Rows[0]["Account_id"].ToString());
                        loginCookie["Name"] = enc.Encrypt(ds.Tables[0].Rows[0]["Name"].ToString());
                        loginCookie["UserType"] = enc.Encrypt(ds.Tables[0].Rows[0]["UserType"].ToString());
                        loginCookie["Status"] = enc.Encrypt(ds.Tables[0].Rows[0]["Status"].ToString());
                        Response.Cookies.Add(loginCookie);
                        if (videoid.ToString() == "blog")
                        {
                            return Redirect("/blog/details/" + bloid + "/" + urllink);
                        }
                        else
                        {
                            return Redirect("/video/details/" + videoid);
                        }
                    }
                    else
                    {
                        TempData["MSGlogin"] = "EMAIL-ID / PASSWORD IS INCORRECT PLEASE TRY AGAIN";
                        return Redirect("/Home");
                    }
                }
            }
            catch (Exception ex)
            { }
            return View();
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Account_Data u)
        {
            var videoid = TempData["mi"];
            try
            {
                u.UserType = "USER";
                u.Status = "true";
                if (dl.InsertUser_Data(u) > 0)
                {
                    TempData["MSG"] = "Account Created!";
                }
            }
            catch (Exception ex)
            { }
            return Redirect("/video/details/" + videoid);
        }

        public ActionResult adminlogin(Account_Data la)
        {
            p.Condition1 = la.Email;
            p.Condition2 = la.Paasword;
            p.onTable = "Fetch_Login";
            ds = dl.FetchMaster(p);
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HttpCookie loginCookie = Request.Cookies["crystalthoughts"];
                    String Type = ds.Tables[0].Rows[0]["UserType"].ToString();
                    String Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    if (Type == "ADMIN" && Status == "True")
                    {
                        loginCookie = new HttpCookie("crystalthoughts");
                        loginCookie["Account_id"] = enc.Encrypt(ds.Tables[0].Rows[0]["Account_id"].ToString());
                        loginCookie["Name"] = enc.Encrypt(ds.Tables[0].Rows[0]["Name"].ToString());
                        loginCookie["UserType"] = enc.Encrypt(ds.Tables[0].Rows[0]["UserType"].ToString());
                        loginCookie["Status"] = enc.Encrypt(ds.Tables[0].Rows[0]["Status"].ToString());
                        Response.Cookies.Add(loginCookie);
                        return Redirect("/admin");
                    }
                    else
                    {
                        TempData["MSGlogin"] = "EMAIL-ID / PASSWORD IS INCORRECT PLEASE TRY AGAIN";
                        return Redirect("/Home");
                    }
                }
            }
            catch (Exception ex)
            { }
            return View();
            return View();
        }

        public ActionResult signin(Account_Data sig)
        {
            p.Condition1 = sig.Email;
            p.Condition2 = sig.Paasword;
            p.onTable = "Fetch_Login";
            ds = dl.FetchMaster(p);
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HttpCookie loginCookie = Request.Cookies["crystalthoughts"];
                    String Type = ds.Tables[0].Rows[0]["UserType"].ToString();
                    String Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    if (Type == "USER" && Status == "True")
                    {
                        loginCookie = new HttpCookie("crystalthoughts");
                        loginCookie["Account_id"] = enc.Encrypt(ds.Tables[0].Rows[0]["Account_id"].ToString());
                        loginCookie["Name"] = enc.Encrypt(ds.Tables[0].Rows[0]["Name"].ToString());
                        loginCookie["UserType"] = enc.Encrypt(ds.Tables[0].Rows[0]["UserType"].ToString());
                        loginCookie["Status"] = enc.Encrypt(ds.Tables[0].Rows[0]["Status"].ToString());
                        Response.Cookies.Add(loginCookie);
                        return Redirect("/");
                    }
                    if (Type == "ADMIN" && Status == "True")
                    {
                        loginCookie = new HttpCookie("crystalthoughts");
                        loginCookie["Account_id"] = enc.Encrypt(ds.Tables[0].Rows[0]["Account_id"].ToString());
                        loginCookie["Name"] = enc.Encrypt(ds.Tables[0].Rows[0]["Name"].ToString());
                        loginCookie["UserType"] = enc.Encrypt(ds.Tables[0].Rows[0]["UserType"].ToString());
                        loginCookie["Status"] = enc.Encrypt(ds.Tables[0].Rows[0]["Status"].ToString());
                        Response.Cookies.Add(loginCookie);
                        return Redirect("/admin");
                    }
                    else
                    {
                        TempData["MSGlogin"] = "EMAIL-ID / PASSWORD IS INCORRECT PLEASE TRY AGAIN";
                        return Redirect("/Home");
                    }
                }
            }
            catch (Exception ex)
            { }
            return View();
        }

    }
}