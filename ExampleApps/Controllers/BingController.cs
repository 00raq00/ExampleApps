using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BingSearcher;

namespace WebApplication6.Controllers
{
    public class BingController : ApiController
    {

        [Route("api/Bing/{SearchOverPage}/")]
        [Route("api/Bing/")]
        public IHttpActionResult Get(int top, string name, string SearchOverPage, bool SearchAsOnePhrase = true)
        {
            BingSearchParameters bingSearchParameters = new BingSearchParameters();
            bingSearchParameters.UseDefaultNetworkCredentials = true;
            bingSearchParameters.SearchAsOnePhrase = SearchAsOnePhrase;
            bingSearchParameters.SearchOverPage = SearchOverPage;
            int count = 0;
            return   Ok(new { results = GetData(top, name, bingSearchParameters, ref count) });
        }

        private static List<BingSearchEntity> GetData(int top, string name, BingSearchParameters bingSearchParameters, ref int count)
        {
            var values = new List<BingSearcher.BingSearchEntity>();
            foreach (var tmp in BingSearcher.BingSearchClient.GetBingSearchEntityLazyList(name, bingSearchParameters))
            {
                values.Add(tmp);
                count++;
                if (count >= top)
                    break;
            }

            return values;
        }
    }
}
