using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using QA.Models;

namespace QA.Repo
{
    public class QuestionAttentionRepository : IQuestionAttentionRepository
    {
        private ApplicationDbContext dbContext;
        public QuestionAttentionRepository()
        {
            dbContext = new ApplicationDbContext();
        }
        public int CreateQuestionAttention(QuestionAttention attention)
        {
            var questionAttentionQuery = from qa in dbContext.QuestionAttentions
                                         where qa.QuestionId == attention.QuestionId && qa.UserId == attention.UserId
                                         select qa;
            if(questionAttentionQuery.Count()>0)
            {
                return 0;
            }
            dbContext.QuestionAttentions.Add(attention);
            dbContext.SaveChanges();
            return 1;
        }


        public int RemoveQuestionAttention(string userId,int questionId)
        {
            QuestionAttention attention = dbContext.QuestionAttentions.Where(qa => qa.UserId == userId && qa.QuestionId == questionId).FirstOrDefault();
            if (attention == null)
            {
                return 0;
            }
            if (attention.UserId != userId)
            {
                return 0;
            }
            dbContext.QuestionAttentions.Remove(attention);
            dbContext.SaveChanges();
            return 1;
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

        public QuestionAttention GetQuestionAttention(int id)
        {
            return dbContext.QuestionAttentions.Find(id);
        }
    }
}