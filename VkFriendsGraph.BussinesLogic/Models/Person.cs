using Newtonsoft.Json;

namespace VkFriendsGraph.BussinesLogic.Vk
{
    public struct Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("can_access_closed")]
        public bool CanAccessClosed { get; set; }

        [JsonProperty("track_code")]
        public string TrackCode { get; set; }

        [JsonProperty("bdate")]
        public string BornDate { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("photo_200_orig")]
        public string PhotoUrl { get; set; }
        public string FullName {
            get
            {
                return FirstName + " " + LastName;
            }

            private set { }
        }

        public override string ToString()
        {
            string output = FullName + " ";
            if (City.Title != null)
            {
                output += $"This person is from {City.Title}. ";
            }
            if (BornDate != null)
            {
                output += $"Birthday: {BornDate}"; 
            }
            return output;
        }
    }

    public struct City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
        
}
