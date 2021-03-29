using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using VkFriendsGraph.BussinesLogic.TokenHelper;
using System.Net;
using System.Diagnostics;

namespace VkFriendsGraph.BussinesLogic.Vk
{
    public class VkLogic
    {
        private string apiPath = "https://api.vk.com/method/";
        private string vkPath = "https://vk.com/";
        private string token = null;

        public VkLogic(bool useUserAuth = true)
        {
            if (useUserAuth)
            {
                token = AuthAndGetToken();
            }
            else
            {
                token = TokenManager.GetToken();
            }
        }

        public async Task<List<Person>> GetPersonFriendsAsync(int id)
        {
            string[] fields = new string[] { "bdate", "city", "photo_200_orig" };
            Dictionary<string, string> pars = new Dictionary<string, string>() { { "user_id", id.ToString() }, { "name_case", "nom" }, { "count", "200" }, { "order", "name" } };
            string method = "friends.get";

            string result = await MyHttpClient.Get(GetMethodUri(method, pars, fields));
            List<Person> people = JSONProcessor.ParsePeople(result);
            return people;
        }

        public async Task<List<Person>> GetPersonFriendsAsync(string address)
        {
            string id = await GetPersonIdAsync(address);
            if (id == "") throw new System.Exception("Cant get id of a person");

            string[] fields = new string[] { "bdate", "city", "photo_200_orig" };
            Dictionary<string, string> pars = new Dictionary<string, string>() { { "user_id", id }, { "name_case", "nom" }, { "count", "200" }, { "order", "name" } };
            string method = "friends.get";

            string result = await MyHttpClient.Get(GetMethodUri(method, pars, fields));
            List<Person> people = JSONProcessor.ParsePeople(result);
            return people;
        }


        public async Task<Person> GetPersonAsync(int id)
        {
            string[] fields = new string[] { "photo_200_orig" };
            Dictionary<string, string> pars = new Dictionary<string, string>() { { "user_ids", id.ToString() }, { "name_case", "Nom" } };
            string method = "users.get";

            string result = await MyHttpClient.Get(GetMethodUri(method, pars, fields));
            Person person = JSONProcessor.ParsePerson(result);
            return person;
        }

        public async Task<Person> GetPersonAsync(string address)
        {
            string id = await GetPersonIdAsync(address);
            if (id == "") throw new System.Exception("Cant get id of a person");

            string[] fields = new string[] { "photo_200_orig" };
            Dictionary<string, string> pars = new Dictionary<string, string>() { { "user_ids", id }, { "name_case", "Nom" } };
            string method = "users.get";

            string result = await MyHttpClient.Get(GetMethodUri(method, pars, fields));
            Person person = JSONProcessor.ParsePerson(result);
            return person;
        }

        public async Task<string> GetPersonIdAsync(string address)
        {
            if (!address.Contains(vkPath))
            {
                address = vkPath + address;
            }
            string rBody = await MyHttpClient.Get(address);
            string pattern = "([0-9]+)_([0-9]+)";
            string id = Regex.Match(rBody, pattern).ToString();
            return id.Split('_')[0];
        }

        private string AuthAndGetToken()
        {
            string url = "https://oauth.vk.com/authorize?client_id=7154369&display=page&redirect_uri=https://oauth.vk.com/blank.html&&response_type=token&v=5.130&state=123456";
            string token;

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage resp = httpClient.GetAsync(url).Result;
                token = resp.Content.ReadAsStringAsync().Result;
            }
            return token;
        }



        private string GetMethodUri(string methodName, Dictionary<string, string> pars, string[] fields)
        {
            string methodUri = apiPath;
            methodUri += methodName + "?";
            foreach (var item in pars)
            {
                methodUri += item.Key + "=" + item.Value + "&";
            }
            methodUri += "fields=";
            foreach (var item in fields)
            {
                methodUri += item + ",";
            }
            methodUri += "&access_token=" + token +"&v=5.101";
            return methodUri;
        }

        private string GetMethodUri(string methodName, Dictionary<string, string> pars)
        {
            string methodUri = apiPath;
            methodUri += methodName + "?";
            foreach (var item in pars)
            {
                methodUri += item.Key + "=" + item.Value + "&";
            }
            methodUri += "&access_token=" + token + "&v=5.101";
            return methodUri;
        }
    }
}
