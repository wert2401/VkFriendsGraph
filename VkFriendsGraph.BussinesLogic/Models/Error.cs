using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VkFriendsGraph.BussinesLogic.Vk
{
    struct Error
    {
        [JsonProperty("error")]
        public object ErrorObject { get; set; }
    }
}
