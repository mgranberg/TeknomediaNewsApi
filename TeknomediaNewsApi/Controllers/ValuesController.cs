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
            //Öppnar upp kopplingen
            using (var client = new HttpClient())
            {
                //kör GetAsync metoden på httpclienten med URL som parameter.
                //inget görs förens denna fått ett svar pga await keyword.
                var response = await client.GetAsync("https://feeds.expressen.se/nyheter/");
                //Kollar så att den fått tillbaka ett successmeddelande
                response.EnsureSuccessStatusCode();

                //Här läser vi content på svaret som en string asynkront men väntar tills den är klar
                var stringResult = await response.Content.ReadAsStringAsync();

                //skapar ett nytt xml dokument
                XmlDocument doc = new XmlDocument();
                //laddar xml från strängen
                doc.LoadXml(stringResult);
                //serialiserar xml dokumentet till en json-sträng
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                //Då jag inte visste hur jag skulle hantera cdata så valde jag att byta ut parametern för att ändå kunna använda detta i Angular
                string jsonParsed = jsonText.Replace("#cdata-section", "cdata");

                //skickar tillbaka den nya json-strängen.
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
                var response = await client.GetAsync("https://www.svd.se/?service=rss");
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
                var response = await client.GetAsync("http://www.nt.se/nyheter/norrkoping/rss/");
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
