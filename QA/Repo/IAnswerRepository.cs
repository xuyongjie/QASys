using Entity;
using Entity.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Repo
{
    interface IAnswerRepository:IDisposable
    {
        int CreateAnswer(Answer answer);
        int RemoveAnswer(string userId, int answerId);
        AnswerDTO GetAnswer(int answerId);
    }
}
