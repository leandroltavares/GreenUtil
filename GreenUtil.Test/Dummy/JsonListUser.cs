using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Dummy
{
    public class JsonListUser
    {
        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("per_page")]

        public string PerPage { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        public string total_pages { get; set; }

        [JsonProperty("data")]
        public List<JsonUser> Users { get; set; }
    }
}
