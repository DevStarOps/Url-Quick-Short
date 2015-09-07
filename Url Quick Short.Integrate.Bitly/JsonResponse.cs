using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Url_Quick_Short.Integrate.Bitly
{

    public class JsonResponse
    {
        public int status_code { get; set; }
        public string status_txt { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string long_url { get; set; }
        public string url { get; set; }
        public string hash { get; set; }
        public string global_hash { get; set; }
        public int new_hash { get; set; }
    }

}
