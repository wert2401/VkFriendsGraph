using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkFriendsGraph.BussinesLogic.Vk
{
    static class JSONProcessor
    {
        public static List<Person> ParsePeople(string response)
        {
            TryParseError(response);
            
            JObject o = JObject.Parse(response);
            List<Person> people = o["response"]["items"].ToObject<List<Person>>();
            return people;
        }

        public static Person ParsePerson(string response)
        {
            TryParseError(response);

            JObject o = JObject.Parse(response);
            Person p = o["response"].ToObject<List<Person>>()[0];
            return p;
        }

        public static void TryParseError(string response)
        {
            Error e;
            e = JsonConvert.DeserializeObject<Error>(response);
            if (e.ErrorObject != null)
            {
                throw new Exception(e.ErrorObject.ToString());
            }
        }
    }
}
