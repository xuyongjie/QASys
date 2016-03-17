using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using Entity.EntityDTO;
using QA.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;

namespace QA.Repo
{
    public class QuestionRepository : IQuestionRepository
    {
        private ApplicationDbContext dbContext;
        public QuestionRepository()
        {
            dbContext = new ApplicationDbContext();
        }
        public IEnumerable<QuestionDTO> GetAllQuestions()
        {
            var questionQuery = from q in dbContext.Questions
                                join u in dbContext.Users on q.UserId equals u.Id
                                orderby q.CreateTime descending
                                select new QuestionDTO()
                                {
                                    Id = q.Id,
                                    UserId = q.UserId,
                                    AnswerCount = dbContext.Answers.Where(a => a.QuestionId == q.Id).Count(),
                                    AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(),
                                    Title = q.Title,
                                    Content = q.Content,
                                    CreateTime = q.CreateTime,
                                    UserHeadImageUrl = u.HeadImageUrl,
                                    UserNickName = u.NickName
                                };
            return questionQuery.ToList();
        }

        public IEnumerable<QuestionDTO> GetAttentionQuestions(string userId)
        {
            var questionQuery = from qa in dbContext.QuestionAttentions
                                where qa.UserId == userId
                                join q in dbContext.Questions on qa.QuestionId equals q.Id
                                join u in dbContext.Users on q.UserId equals u.Id
                                orderby qa.CreateTime descending
                                select new QuestionDTO
                                {
                                    Id = q.Id,
                                    UserId = u.Id,
                                    AnswerCount = dbContext.Answers.Where(a => a.QuestionId == q.Id).Count(),
                                    AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(),
                                    Title = q.Title,
                                    Content = q.Content,
                                    CreateTime = q.CreateTime,
                                    UserHeadImageUrl = u.HeadImageUrl,
                                    UserNickName = u.NickName
                                };
            return questionQuery.ToList();
        }

        public IEnumerable<QuestionDTO> GetRaisedQuestions(string userId)
        {
            var questionQuery = from q in dbContext.Questions
                                where q.UserId == userId
                                join u in dbContext.Users on q.UserId equals u.Id
                                orderby q.CreateTime descending
                                select new QuestionDTO()
                                {
                                    Id = q.Id,
                                    UserId = q.UserId,
                                    AnswerCount = dbContext.Answers.Where(a => a.QuestionId == q.Id).Count(),
                                    AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(),
                                    Title = q.Title,
                                    Content = q.Content,
                                    CreateTime = q.CreateTime,
                                    UserHeadImageUrl = u.HeadImageUrl,
                                    UserNickName = u.NickName
                                };
            return questionQuery.ToList();
        }
        public QuestionDetailDTO GetQuestionDetailById(string userId, int questionId)
        {
            var questionUserQuery = from q in dbContext.Questions
                                    where q.Id == questionId
                                    join u in dbContext.Users on q.UserId equals u.Id
                                    select u;
            var user = questionUserQuery.FirstOrDefault();

            var toAnswerAnswersQuery = from a in dbContext.Answers
                                       where a.QuestionId == questionId && a.AnswerType == 1
                                       join fu in dbContext.Users on a.FromUserId equals fu.Id
                                       join ta in dbContext.Answers on a.ToAnswerId equals ta.Id
                                       join tafu in dbContext.Users on ta.FromUserId equals tafu.Id orderby a.CreateTime
                                       select new AnswerDTO
                                       {
                                           Id = a.Id,
                                           Content = a.Content,
                                           CreateTime = a.CreateTime,
                                           FromUserId = a.FromUserId,
                                           AnswerType=a.AnswerType,
                                           ToAnswerId = a.ToAnswerId,
                                           FromUserNickName = fu.NickName,
                                           ToUserId = ta.FromUserId,
                                           ToUserNickName = tafu.NickName,
                                           QuestionId = a.QuestionId,
                                           NiceOrNot = string.IsNullOrEmpty(userId) ? false : dbContext.Nices.Where(n => n.AnswerId == a.Id && n.UserId == userId).Count() > 0,
                                           NiceCount = dbContext.Nices.Where(n => n.AnswerId == a.Id).Count()
                                       };
            var toQuestionAnswersQuery = from a in dbContext.Answers
                                         where a.QuestionId == questionId && a.AnswerType == 0
                                         join fu in dbContext.Users on a.FromUserId equals fu.Id orderby a.CreateTime
                                         select new AnswerDTO
                                         {
                                             Id = a.Id,
                                             Content = a.Content,
                                             CreateTime = a.CreateTime,
                                             FromUserId = a.FromUserId,
                                             AnswerType=a.AnswerType,
                                             ToAnswerId = a.ToAnswerId,
                                             FromUserNickName = fu.NickName,
                                             ToUserId = user.Id,
                                             ToUserNickName = user.NickName,
                                             QuestionId = a.QuestionId,
                                             NiceOrNot = string.IsNullOrEmpty(userId) ? false : dbContext.Nices.Where(n => n.AnswerId == a.Id && n.UserId == userId).Count() > 0,
                                             NiceCount = dbContext.Nices.Where(n => n.AnswerId == a.Id).Count()
                                         };

            var toQuestionAnswers = toQuestionAnswersQuery.ToList();
            var toAnswerAnswers = toAnswerAnswersQuery.ToList();
            List<AnswerDTO> sequenceAnswers = new List<AnswerDTO>();
            if (toQuestionAnswers != null)
            {
                foreach(var item in toQuestionAnswers)
                {
                    sequenceAnswers.Add(item);
                    if(toAnswerAnswers!=null)
                    {
                        while(true)
                        {
                            var last=sequenceAnswers[sequenceAnswers.Count - 1];
                            int i;
                            for(i=0; i<toAnswerAnswers.Count; i++)
                            {
                                var temp = toAnswerAnswers[i];
                                if(temp.ToAnswerId==last.Id)
                                {
                                    sequenceAnswers.Add(temp);
                                    break;
                                }
                            }
                            if(i==toAnswerAnswers.Count)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            bool attented = false;
            if (!string.IsNullOrEmpty(userId))
            {
                var attentionQuery = from qa in dbContext.QuestionAttentions
                                     where qa.UserId == userId && qa.QuestionId == questionId
                                     select qa;
                attented = attentionQuery.Count() > 0;
            }

            var questionDetailQuery = from q in dbContext.Questions
                                      where q.Id == questionId
                                      join u in dbContext.Users
                                      on q.UserId equals u.Id
                                      select new QuestionDetailDTO
                                      {
                                          Id = q.Id,
                                          Title = q.Title,
                                          Content = q.Content,
                                          CreateTime = q.CreateTime,
                                          UserId = u.Id,
                                          UserNickName = u.NickName,
                                          AnswerCount = sequenceAnswers.Count,
                                          AttentedOrNot = attented,
                                          UserHeadImageUrl = u.HeadImageUrl,
                                      };
            var result = questionDetailQuery.FirstOrDefault();
            if (result != null)
            {
                result.Answers = sequenceAnswers;
            }
            return result;
        }


        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public int CreateQuestion(Question question)
        {
            dbContext.Questions.Add(question);
            dbContext.SaveChanges();
            return 1;

        }

        public int UpdateQuestion(string userId, int questionId, Question question)
        {
            var q = dbContext.Questions.Find(questionId);
            if (q == null || q.UserId != userId)
            {
                return 0;
            }
            dbContext.Entry(question).State = EntityState.Modified;
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
            return 1;
        }


        public int DeleteQuestionById(string userId, int questionId)
        {
            Question question = dbContext.Questions.Find(questionId);
            if (question == null)
            {
                return 0;
            }
            if (question.UserId != userId)
            {
                return 0;
            }
            dbContext.Questions.Remove(question);
            dbContext.SaveChanges();
            return 1;
        }

        private bool QuestionExists(int id)
        {
            return dbContext.Questions.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}