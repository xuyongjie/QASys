using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity.EntityDTO;
using QA.Models;

namespace QA.Repo
{
    public class QuestionRepository : IQuestionRepository
    {
        public IEnumerable<QuestionDTO> GetAllQuestions()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Questions.Select(q => new QuestionDTO() { Id = q.Id, UserId = q.UserId, AnswerCount = dbContext.Answers.Where(a => a.Id == q.Id).Count(), AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(), Content = q.Content, UserHeadImageUrl = dbContext.Users.First(u => u.Id == q.UserId).HeadImageUrl, UserNickName = dbContext.Users.First(u => u.Id == q.UserId).NickName });
            }
        }

        public IEnumerable<QuestionDTO> GetAttentionQuestions(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return null;
            }
        }

        public QuestionDetailDTO GetQuestionById(int questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionDTO> GetRaisedQuestions(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Questions.Where(q => q.UserId == userId).Select(q => new QuestionDTO() { Id = q.Id, UserId = q.UserId, AnswerCount = dbContext.Answers.Where(a => a.Id == q.Id).Count(), AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(), Content = q.Content, UserHeadImageUrl = dbContext.Users.First(u => u.Id == q.UserId).HeadImageUrl, UserNickName = dbContext.Users.First(u => u.Id == q.UserId).NickName });
            }
        }

        public IEnumerable<QuestionDTO> GetTimeLineAllQuestions(string userId)
        {
            return null;
        }
    }
}