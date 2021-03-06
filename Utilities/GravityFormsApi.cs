using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Extensions.Configuration;
using Roster.Models;

namespace Roster.Utilities
{
    public class GravityFormsApi
    {
        private string _baseUrl;
        private string _publicKey;
        private string _privateKey;
        private HttpClient _client;

        public GravityFormsApi(IConfiguration config, HttpClient client)
        {
            _baseUrl = config["GravityFormsUrl"];
            _publicKey = config["GravityFormsPublicKey"];
            _privateKey = config["GravityFormsPrivateKey"];
            if (_baseUrl == null || _publicKey == null || _privateKey == null)
            {
                throw new Exception("GravityForms config missing.");
            }
            _client = client;
        }

        public List<Registration> GetNewPaidRegistrations()
        {
            //Get most recent Registration.DateCreated
            //GetRegistrationsSince()
            return null;
        }

        public List<Registration> GetRegistrationsSince(string date)
        {
            string url = GetUrlForEntriesSince(date);
            
            var streamTask = _client.GetStreamAsync(url);
            streamTask.Wait();
            var serializer = new DataContractJsonSerializer(typeof(GravityFormRegistration));
            var gfRegistration = serializer.ReadObject(streamTask.Result) as GravityFormRegistration;
            return gfRegistration?.Response?.Registrations.Where(r => r.Active).ToList();
        }
        
        public string GetUrlForEntriesSince(string date, int numberOfEntries = 100)
        {
            string method = "GET";
            string route = "forms/1/entries";
            string queryString = "search[start_date]=" + date;
            queryString += $"&paging[page_size]={numberOfEntries}";
            string expires = Security.UtcTimestamp(new TimeSpan(0,10,0)).ToString();
            string url = GenerateUrl(_baseUrl, route, method, queryString, _publicKey, _privateKey, expires);
            return url;
        }

        public string GetUrlForLatestEntries(int numberOfEntries = 10)
        {
            string method = "GET";
            string route = "forms/1/entries";
            string queryString = $"paging[page_size]={numberOfEntries}";
            string expires = Security.UtcTimestamp(new TimeSpan(0,10,0)).ToString();
            string url = GenerateUrl(_baseUrl, route, method, queryString, _publicKey, _privateKey, expires);
            return url;
        }
       
        private string GenerateUrl(string baseUrl, string route, string method, string queryString, string publicKey, string privateKey, string expires)
        {
            string stringToSign = string.Format("{0}:{1}:{2}:{3}", publicKey, method, route, expires);
            var sig = Security.Sign(stringToSign, privateKey);

            string url = baseUrl + route + "/?api_key=" + publicKey + "&signature=" + sig + "&expires=" + expires;
            if (queryString != string.Empty)
                url += "&" + queryString;
            return url;
        }
    }
 
    public static class Security
    { 
        public static string Sign(string value, string key)
        {
            using (var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(key)))
            {
                return UrlEncodeTo64(hmac.ComputeHash(Encoding.ASCII.GetBytes(value)));
            }
        }
        public static string UrlEncodeTo64(byte[] bytesToEncode)
        {
            string returnValue = System.Convert.ToBase64String(bytesToEncode);
             return HttpUtility.UrlEncode(returnValue);
        }

        public static int UtcTimestamp(TimeSpan timeSpanToAdd)
        {
            TimeSpan ts = (DateTime.UtcNow.Add(timeSpanToAdd) - new DateTime(1970,1,1,0,0,0));
            int expires_int =  (int) ts.TotalSeconds;
            return expires_int;
        }
    }
}
