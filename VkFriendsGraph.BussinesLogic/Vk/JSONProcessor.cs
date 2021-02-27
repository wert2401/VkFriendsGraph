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
            JObject o = JObject.Parse(response);
            List<Person> people = o["response"]["items"].ToObject<List<Person>>();
            return people;
        }

        public static bool TryParseError(string response)
        {
            Error e;
            e = JsonConvert.DeserializeObject<Error>(response);
            if (e.ErrorObject != null)
            {
                return true;
            }
            return false;
        }

        public static Person ParsePerson(string response)
        {
            return JsonConvert.DeserializeObject<Person>(response);
        }

    }
}
