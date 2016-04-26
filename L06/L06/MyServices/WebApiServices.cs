using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace L05_2.MyServices
{
    public class WebApiServices
    {
        private readonly HttpClient _httpClient;

        public WebApiServices()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataAsync(string city, string area)
        {
            _httpClient.DefaultRequestHeaders.Add("Referer", "http://www.family.com.tw/marketing/inquiry.aspx");
            _httpClient.DefaultRequestHeaders.Add("Host", "api.map.com.tw");

            var uri = "http://api.map.com.tw/net/familyShop.aspx?searchType=ShopList&type=&city=" + city + "&area=" + area + "&road=&fun=showStoreList&key=6F30E8BF706D653965BDE302661D1241F8BE9EBC";
            Debug.WriteLine(uri);

            var response = await _httpClient.GetAsync(uri);

            var responseAsString = await response.Content.ReadAsStringAsync();


            responseAsString = responseAsString.Remove(0, 14);
            responseAsString = responseAsString.Substring(0, responseAsString.Length -1);
            Debug.WriteLine(responseAsString);

            return responseAsString;

        }
    }
}
