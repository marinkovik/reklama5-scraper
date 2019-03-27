using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.Xml;
using HtmlAgilityPack;
using FlashScoreScraper.Models;

namespace FlashScoreScraper.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<ActionResult> Index(int? id)
        {
            List<carAds> carAdss = new List<carAds>();
            var url = "http://reklama5.mk/Search?q=&city=&sell=0&sell=1&buy=0&buy=1&trade=0&trade=1&includeOld=0&includeOld=1&includeNew=0&includeNew=1&f31=&priceFrom=&priceTo=&f33_from=&f33_to=&f36_from=&f36_to=&f35=&f37=&f138=&f10016_from=&f10016_to=&private=0&company=0&page=" + 1 + "&SortByPrice=0&zz=1&cat=24";
            if (id.HasValue)
                url = "http://reklama5.mk/Search?q=&city=&sell=0&sell=1&buy=0&buy=1&trade=0&trade=1&includeOld=0&includeOld=1&includeNew=0&includeNew=1&f31=&priceFrom=&priceTo=&f33_from=&f33_to=&f36_from=&f36_to=&f35=&f37=&f138=&f10016_from=&f10016_to=&private=0&company=0&page=" + id.ToString() + "&SortByPrice=0&zz=1&cat=24";

            var httpClient = new HttpClient();

            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("OglasResults"))
                .ToList();

            foreach (var div in divs)
            {
                var time = div.ChildNodes.ElementAt(3).ChildNodes.ElementAt(2).InnerText;
                var carName = div.ChildNodes.ElementAt(6).ChildNodes.ElementAt(0).InnerText;
                var carLinkToDescription = div.ChildNodes.ElementAt(6).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Attributes.ElementAt(0).DeEntitizeValue;
                var carPrice = div.ChildNodes.ElementAt(6).ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).InnerText;

                url = "http://reklama5.mk" + carLinkToDescription;
                httpClient = new HttpClient();
                html = await httpClient.GetStringAsync(url);
                htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var carDescription = "";
                try
                {
                    carDescription = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[1]/div[9]/div[1]/div[7]/div[1]/div[7]/p[3]").InnerText;
                }
                catch
                {}
                //var carLocation = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[1]/div[9]/div[2]/div[1]/p[1]/b[1]").InnerText;
                //var carSellerName = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[1]/div[9]/div[2]/div[2]/p[1]/b").InnerText;

                var carSellerPhone = "";
                try
                {
                    carSellerPhone = hideLastDigit(htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[1]/div[9]/div[2]/div[2]/p[2]/label").InnerText.ToString());
                }
                catch
                {}
                var tmpAd = new carAds() { time = time, carName = carName, carPrice = carPrice, carLinkToDescription = carLinkToDescription, carDescription = carDescription, carSellerPhone = carSellerPhone};

                carAdss.Add(tmpAd);
            }
            return View(carAdss);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string hideLastDigit(string telephoneNumber)
        {
            string newTelephoneNumber = telephoneNumber.Remove(telephoneNumber.Length - 3) + "XXX";
            return newTelephoneNumber;
        }
    }
}