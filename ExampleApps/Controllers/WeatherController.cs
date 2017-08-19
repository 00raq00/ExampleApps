using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BingSearcher;

namespace WebApplication6.Controllers
{
    public class WeatherController : ApiController
    {
        private const string Klucz = "ffc066768428360886a1528ac3d997cecec23dd7";

        [Route("api/Weather/{City}/{Radius}")]
        [Route("api/Weather/{City}/")]
        [Route("api/Weather/")]
        public IHttpActionResult Get(string City, int Radius=200)
        {
            using(Storm.serwerSOAPPortClient cl = new Storm.serwerSOAPPortClient())
            {
                var city = cl.miejscowosc(City, Klucz);
                if(city.x+city.y==0)
                {
                    return NotFound();
                }

                var storm = cl.szukaj_burzy(city.y.ToString(), city.x.ToString(), Radius, Klucz);
                var den = cl.ostrzezenia_pogodowe(city.y.Value, city.x.Value, Klucz);

                return Ok(new { results = storm.kierunek,storm.liczba,storm.odleglosc,storm.okres });
            }
        }
        
    }
}
