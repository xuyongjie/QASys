using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Repo
{
    interface IQuestionAttentionRepository : IDisposable
    {
        int CreateQuestionAttention(QuestionAttention attention);
        int RemoveQuestionAttention(string userId, int questionId);
        QuestionAttention GetQuestionAttention(int id);
    }
}
