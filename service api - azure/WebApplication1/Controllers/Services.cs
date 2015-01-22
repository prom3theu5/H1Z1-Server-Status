using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace h1z1status.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServicesController : ApiController
    {
     
        //
        //Get Servers From Sony Live Page as HTML and Parse into an ArrayList of type List<string>
        //
        private List<List<string>> GetServers()
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://lp.soe.com/h1z1/live/worlds");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            return doc.DocumentNode.SelectSingleNode("//table[@id='lpServerStatus']")
            .Descendants("tr")
            .Skip(1)
            .Where(tr=>tr.Elements("td").Count()>1)
            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            .ToList();
        }

        
        //
        //Apply our Server Models and return the split list (US and EU)
        //
        public Models.Servers GetServerStatuses()
        {
            var servers = GetServers();
            var returnServers = new Models.Servers();
            
            foreach (var item in servers)
            {
                var newServer = new Models.Server() { ServerName = item[0], State = item[1] };
                
                if (!newServer.ServerName.Contains("(EU)"))
                {
                    returnServers.US.Add(newServer);
                }
                else
                {
                    returnServers.EU.Add(newServer);
                }
            }
            return returnServers;
        }
    }
}