using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
    public class Ward
    {
        [JsonProperty("WardCode")]// Nếu GHN trả về WardCode
        public string WardId { get; set; }

        //[JsonProperty("WardName")]
        public string WardName { get; set; }
        public int DistrictId { get; set; }
    }
}
