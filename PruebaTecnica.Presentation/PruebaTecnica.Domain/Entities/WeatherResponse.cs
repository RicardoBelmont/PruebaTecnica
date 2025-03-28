using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Entities
{
    public class WeatherResponse : IResponseBase<CityWeather>
    {
        public HttpStatusCode status { get; set ; }
        public CityWeather Response { get; set; }
    }
}
