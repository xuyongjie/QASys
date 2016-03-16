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
        /// <summary>
        /// 获取所有问题列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<QuestionDTO> GetAllQuestions();
        /// <summary>
        /// 或许userId用户提出的问题列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<QuestionDTO> GetRaisedQuestions(string userId);
        /// <summary>
        /// 获取userId用户关注的问题列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<QuestionDTO> GetAttentionQuestions(string userId);
        /// <summary>
        /// 获取问题详细信息
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        QuestionDetailDTO GetQuestionDetailById(string userId,int questionId);
        /// <summary>
        /// 更新某个问题
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        int UpdateQuestion(string userId,int questionId, Question question);
        /// <summary>
        /// 创建一个问题
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        int CreateQuestion(Question question);
        /// <summary>
        /// 删除某个问题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        int DeleteQuestionById(string userId,int questionId);
    }
}