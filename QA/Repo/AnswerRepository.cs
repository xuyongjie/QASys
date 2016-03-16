using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using QA.Models;
using Entity.EntityDTO;

namespace QA.Repo
{
    public class AnswerRepository : IAnswerRepository
    {
        private ApplicationDbContext dbContext;
        public AnswerRepository()
        {
            dbContext = new ApplicationDbContext();
        }
        public int CreateAnswer(Answer answer)
        {
            var answerQuery = from a in dbContext.Answers
                              where a.Id == answer.Id
                              select a;
            if (answerQuery.Count() > 0)
            {
                return 0;
            }
            dbContext.Answers.Add(answer);
            dbContext.SaveChanges();
            return 1;
        }

        public AnswerDTO GetAnswer(int answerId)
        {
            var query = from a in dbContext.Answers where a.Id == answerId select a;
            var answer = query.FirstOrDefault();
            var questionUserQuery = from q in dbContext.Questions
                                    where q.Id == answer.QuestionId
                                    join u in dbContext.Users on q.UserId equals u.Id
                                    select u;
            var user = questionUserQuery.FirstOrDefault();

            switch (answer.AnswerType)
            {
                case 0:
                    var toQuestionAnswerQuery = from a in dbContext.Answers
                                                where a.Id == answerId
                                                join fu in dbContext.Users on a.FromUserId equals fu.Id
                                                select new AnswerDTO
                                                {
                                                    Id = a.Id,
                                                    Content = a.Content,
                                                    CreateTime = a.CreateTime,
                                                    FromUserId = a.FromUserId,
                                                    ToAnswerId = a.ToAnswerId,
                                                    FromUserNickName = fu.NickName,
                                                    ToUserId = user.Id,
                                                    ToUserNickName = user.NickName,
                                                    QuestionId = a.QuestionId,
                                                    NiceCount = dbContext.Nices.Where(n => n.AnswerId == a.Id).Count()
                                                };
                    return toQuestionAnswerQuery.FirstOrDefault();
                case 1:
                    var toAnswerAnswersQuery = from a in dbContext.Answers
                                               where a.Id == answerId
                                               join fu in dbContext.Users on a.FromUserId equals fu.Id
                                               join ta in dbContext.Answers on a.ToAnswerId equals ta.Id
                                               join tafu in dbContext.Users on ta.FromUserId equals tafu.Id
                                               select new AnswerDTO
                                               {
                                                   Id = a.Id,
                                                   Content = a.Content,
                                                   CreateTime = a.CreateTime,
                                                   FromUserId = a.FromUserId,
                                                   ToAnswerId = a.ToAnswerId,
                                                   FromUserNickName = fu.NickName,
                                                   ToUserId = ta.FromUserId,
                                                   ToUserNickName = tafu.NickName,
                                                   QuestionId = a.QuestionId,
                                                   NiceCount = dbContext.Nices.Where(n => n.AnswerId == a.Id).Count()
                                               };
                    return toAnswerAnswersQuery.FirstOrDefault();
                default:
                    break;
            }

            return null;
        }

        public int RemoveAnswer(string userId, int answerId)
        {
            Answer answer = dbContext.Answers.Where(a => a.FromUserId == userId && a.Id == answerId).FirstOrDefault();
            if (answer == null)
            {
                return 0;
            }
            dbContext.Answers.Remove(answer);
            dbContext.SaveChanges();
            return 1;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}