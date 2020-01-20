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
using crystalthoughts.Helpers;

namespace crystalthoughts.Controllers
{
    public class videoController : Controller
    {
        DataSet ds = new DataSet();
        DLayer dl = new DLayer();
        Property p = new Property();
        EncryptDecrypt enc = new EncryptDecrypt();
        // GET: video
        public ActionResult Index()
        {
            try
            {
                YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyCcuv08bsWw3YwAgK3Aah2cv2BoL2lAH_Q" });
                var searchListRequest = yt.Search.List("snippet");
                searchListRequest.ChannelId = "UCyk2f2QGr2LEjLNopAcy6qA";
                var searchListResult = searchListRequest.Execute();
                List<Videolist> Videolist = new List<Videolist>();
                foreach (var item in searchListResult.Items)
                {
                    Videolist pl = new Videolist();
                    pl.Title = item.Snippet.Title;
                    pl.Videoimage = item.Snippet.Thumbnails.High.Url;
                    //pl.Video = "https://www.youtube.com/embed/" + item.Id.VideoId;
                    pl.Video = item.Id.VideoId;
                    pl.Description = item.Snippet.Description;
                    pl.publishat = item.Snippet.PublishedAt.ToString();
                    Videolist.Add(pl);
                }
                ViewBag.Videolist = Videolist;
            }
            catch (Exception e)
            { }
            return View();
        }
        public ActionResult details(string id)
        {
            TempData["videoid"] = id;
            var info = new crystalthoughts.Models.Videolist();
            try
            {
                YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyCcuv08bsWw3YwAgK3Aah2cv2BoL2lAH_Q" });
                var searchListRequest = yt.Search.List("snippet");
                searchListRequest.ChannelId = "UCyk2f2QGr2LEjLNopAcy6qA";
                var searchListResult = searchListRequest.Execute();
                foreach (var item in searchListResult.Items)
                {
                    if (item.Id.VideoId != null)
                    {
                        if (id == item.Id.VideoId)
                        {
                            info.Title = item.Snippet.Title;
                            info.Description = item.Snippet.Description;
                            info.Video = "https://www.youtube.com/embed/" + item.Id.VideoId;
                            info.Videoid = item.Id.VideoId;
                            info.Videoimage = item.Snippet.Thumbnails.High.Url;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            p.onTable = "Fetch_Comment_Data";
            ds = dl.FetchMaster(p);
            List<Account_Data> commentlist = new List<Account_Data>();
            try
            {
                foreach (DataRow i in ds.Tables[1].Rows)
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
            try
            {
                ac.user_id = enc.Decrypt(loginCookie["Account_id"]).ToString();
                ac.videoid = TempData["videoid"].ToString();
                ac.Status = "false";
                if (dl.Insertcomment(ac) > 0)
                {
                    TempData["MSG"] = "Thnakyou for Showing Intrest!";
                }
            }
            catch (Exception ex)
            { }
            return Redirect("/video/details/" + TempData["videoid"]);
        }
    }
}