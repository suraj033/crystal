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

namespace crystalthoughts.Controllers
{
    public class HomeController : Controller
    {
        Property p = new Property();
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
                    pl.Video =item.Id.VideoId;
                    pl.publishat =Convert.ToDateTime(item.Snippet.PublishedAt).ToString("dd-MM-yyy");
                    pl.Description = item.Snippet.Description;
                    Videolist.Add(pl);
                }
                ViewBag.Videolist = Videolist;              
            }
            catch (Exception e)
            {}
            return View();
        }
    }
}