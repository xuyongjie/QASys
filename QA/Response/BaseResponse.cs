using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Response
{
    interface BaseResponse
    {
        int Status { get; set; }
        string ErrorMessage { get; set; }
    }
}
