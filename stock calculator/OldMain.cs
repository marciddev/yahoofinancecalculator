using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace stock_calculator
{
    public class OldMain
    {
        public static async Task<apiStuff> grabData(String s)
        {
            using (var client = new HttpClient())
            {//initiating HttpClient to grab from API.
                String url = "https://financialmodelingprep.com/api/v3/historical-price-full/IVE?serietype=line";//these are the api calls for 9 different yahoo stocks requested by client I was creating this for.
                var response = await client.GetStringAsync(url);
                apiStuff IVE = JsonConvert.DeserializeObject<apiStuff>(response);//deserializing from JSON to C# from the api.
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IVV?serietype=line";//repeatVVVVV
                response = await client.GetStringAsync(url);
                apiStuff IVV = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IVW?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IVW = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IJJ?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IJJ = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IJH?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IJH = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IJK?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IJK = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IWN?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IWN = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IWM?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IWM = JsonConvert.DeserializeObject<apiStuff>(response);
                url = "https://financialmodelingprep.com/api/v3/historical-price-full/IWO?serietype=line";
                response = await client.GetStringAsync(url);
                apiStuff IWO = JsonConvert.DeserializeObject<apiStuff>(response);
                if (s == "IVE")
                {
                    return IVE;
                }
                else if (s == "IVV")
                {
                    return IVV;
                }
                else if (s == "IVW")
                {
                    return IVW;
                }
                else if (s == "IJJ")
                {
                    return IJJ;
                }
                else if (s == "IJH")
                {
                    return IJH;
                }
                else if (s == "IJK")
                {
                    return IJK;
                }
                else if (s == "IWN")
                {
                    return IWN;
                }
                else if (s == "IWM")
                {
                    return IWM;
                }
                else if (s == "IWO")
                {
                    return IWO;
                }
                else
                {
                    return IVE;
                }
            }
        }
    }
    public class grabHolidayData
    {
        public static async Task<holidayClass> getHolidayData()
        {
            using (var client2 = new HttpClient())
            {
                String url = "https://holidayapi.com/v1/holidays?pretty&key=190014ba-b690-4a6b-9aab-5287be2894ad&country=US&year=2019";
                var response = await client2.GetStringAsync(url);
                holidayClass holidays = JsonConvert.DeserializeObject<holidayClass>(response);
                return holidays;
            }
        }
    }
}
