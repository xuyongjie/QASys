using Entity;
using Entity.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA.Repo
{
    interface IQuestionRepository:IDisposable
    {
        IEnumerable<QuestionDTO> GetAllQuestions();
        IEnumerable<QuestionDTO> GetRaisedQuestions(string userId);
        IEnumerable<QuestionDTO> GetAttentionQuestions(string userId);
        IEnumerable<QuestionDTO> GetTimeLineAllQuestions(string userId);
        QuestionDetailDTO GetQuestionById(int questionId);
        int UpdateQuestion(int questionId, Question question);
        int CreateQuestion(Question question);
        int DeleteQuestionById(int questionId);
    }
}