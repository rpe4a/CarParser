using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Parser
{
    internal static class QueryFactory
    {
        private const string APIKEY = "4f4611b01820d5157d96ed5115ad1042";
        private static Dictionary<string, object> Parameters;
        private static readonly string[] SOURCE = {"auto.ru", "am.ru", "irr.ru", "avito.ru"};

        static QueryFactory()
        {
            Parameters = new Dictionary<string, object>
            {
                ["api_key"] = APIKEY,
                ["region"] = 2276,
                //["last"] = 1, 
                //["source"] = string.Join(",", SOURCE),
                //["condition"] = 1,
                //["car"] = 1,
                //["limit"] = 50
            };
        }

        public static HttpRequestMessage GetLastHourOffers()
        {
            return new HttpRequestMessage(HttpMethod.Get, $"latest/get_ads/{GetQueryParametersStr()}")
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };

        }

        public static HttpRequestMessage GetNewOffers()
        {
            return new HttpRequestMessage(HttpMethod.Get, $"latest/get_new_ads/{GetQueryParametersStr()}")
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };

        }

        private static string GetQueryParametersStr()
        {
            return "?" + string.Join("&", Parameters.Select(x => $"{x.Key}={x.Value}"));
        }
    }
}
