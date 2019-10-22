using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiErp.dao
{
   

    public class ApiResponse
    {
        public ApiResponse()
        {
            this.ErrorList = new List<string>();
        }

        public Nullable<long> id { get; set; }
        public List<string> ErrorList { get; set; }
        public bool Success { get; set; }
        public Object data { get; set; }

    }
}
