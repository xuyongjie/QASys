using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Results
{
    interface BaseResponse
    {
        int Status { get; set; }
        string ErrorMessage { get; set; }
        object Data { get; set; }
    }
}
