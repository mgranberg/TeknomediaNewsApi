using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml;

namespace TeknomediaNewsApi.Controllers
{
    [Route("api/[controller]")]
    
    // GET api/news
    //URL/api/(namn på controllern, i detta fall news)
    public class NewsController : Controller
    {        

        // GET api/news/expressen
        //efter namn på controllern så specificeras namnet på metoden, i detta fall expressen

        [HttpGet ("[action]")]
        public async Task<string> Expressen()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://feeds.expressen.se");
                var response = await client.GetAsync($"/nyheter/");
                response.EnsureSuccessStatusCode();

               

                var stringResult = await response.Content.ReadAsStringAsync();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(stringResult);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                string jsonParsed = jsonText.Replace("#cdata-section", "cdata");

                return jsonParsed;
            }
        }

        // GET api/news/svd
        //efter namn på controllern så specificeras namnet på metoden, i detta fall Svenska Dagbladet

        [HttpGet("[action]")]
        public async Task<string> Svd()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.svd.se");
                var response = await client.GetAsync($"/?service=rss");
                response.EnsureSuccessStatusCode();



                var stringResult = await response.Content.ReadAsStringAsync();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(stringResult);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                string jsonParsed = jsonText.Replace("#cdata-section", "cdata");

                return jsonParsed;
            }
        }

        // GET api/news/expressen
        //efter namn på controllern så specificeras namnet på metoden, i detta fall Norrköping tidningar

        [HttpGet("[action]")]
        public async Task<string> Nt()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.nt.se");
                var response = await client.GetAsync($"/nyheter/norrkoping/rss/");
                response.EnsureSuccessStatusCode();



                var stringResult = await response.Content.ReadAsStringAsync();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(stringResult);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                string jsonParsed = jsonText.Replace("#cdata-section", "cdata");

                return jsonParsed;
            }
        }
    }
}
