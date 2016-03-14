using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.UWP.Core
{
    interface INavigatable
    {
        void NavigateTo(object parameter);
        void NavigateFrom(object parameter);
    }
}
