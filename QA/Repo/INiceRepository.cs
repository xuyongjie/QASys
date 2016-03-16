using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Repo
{
    interface INiceRepository:IDisposable
    {
        int CreateNice(Nice nice);
        int RemoveNice(string userId, int answerId);
        Nice GetNice(int niceId);
    }
}
