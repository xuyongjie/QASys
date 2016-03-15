using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Repo
{
    interface IQuestionAttentionRepository
    {
        int CreateQuestionAttention(QuestionAttention attention);
        int RemoveQuestionAttention(int id);
    }
}
