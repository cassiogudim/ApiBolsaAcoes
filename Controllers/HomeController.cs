using ApiAcoesNetFramework.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiAcoesNetFramework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("ApiAcoes")]
        [HttpGet]
        public ActionResult ApiAcoes()
        {
            var client = new RestClient("https://www.infomoney.com.br/webservices/services.asmx/GetStockSearchData?hash=cfcd208495d565ef66e7dff9f98764da");
            var request = new RestRequest(Method.POST);
            //request.AddHeader("Postman-Token", "74d3e064-45b6-4338-8e2a-8c2c504ac0ce");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\"sEcho\":8,\"iDisplayStart\":0,\"iDisplayLength\":10000,\"iSortCol_0\":1,\"sSortDir_0\":\"asc\",\"sSearch\":\"\",\"TypeBind\":\"cot\",\"MarketID\":\"0\",\"SectorID\":\"0\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var item = response.Content.Replace("\\", "");
            item = item.Replace("{\"d\":\"{", "{\"d\":{");
            item = item.Replace("\"]]}\"}", "\"]]}}");

            //apiAcoes result = item;
            ViewData["retorno"] = item;
            return View();
        }
    }
}
