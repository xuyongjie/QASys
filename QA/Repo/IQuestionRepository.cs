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
        IEnumerable<QuestionDTO> GetRaisedQuestions();
        IEnumerable<QuestionDTO> GetTimeLineAllQuestions();
        QuestionDTO GetQuestionById(int questionId);
    }
}