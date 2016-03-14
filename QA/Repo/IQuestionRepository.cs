using Entity.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA.Repo
{
    interface IQuestionRepository
    {
        IEnumerable<QuestionDTO> GetAllQuestions();
        IEnumerable<QuestionDTO> GetRaisedQuestions(string userId);
        IEnumerable<QuestionDTO> GetAttentionQuestions(string userId);
        IEnumerable<QuestionDTO> GetTimeLineAllQuestions(string userId);
        QuestionDetailDTO GetQuestionById(int questionId);
    }
}