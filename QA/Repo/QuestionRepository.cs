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
                                select new QuestionDTO()
                                {
                                    Id = q.Id,
                                    UserId = q.UserId,
                                    AnswerCount = dbContext.Answers.Where(a => a.QuestionId == q.Id).Count(),
                                    AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(),
                                    Title=q.Title,
                                    Content = q.Content,
                                    UserHeadImageUrl = u.HeadImageUrl,
                                    UserNickName = u.NickName
                                };
            return questionQuery.ToList();
        }

        public IEnumerable<QuestionDTO> GetAttentionQuestions(string userId)
        {
            var questionQuery = from qa in dbContext.QuestionAttentions
                                where qa.UserId == userId
                                join u in dbContext.Users on qa.UserId equals u.Id
                                join q in dbContext.Questions on qa.QuestionId equals q.Id
                                select new QuestionDTO
                                {
                                    Id = q.Id,
                                    UserId = u.Id,
                                    AnswerCount = dbContext.Answers.Where(a => a.QuestionId == q.Id).Count(),
                                    AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(),
                                    Title = q.Title,
                                    Content = q.Content,
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
                                select new QuestionDTO()
                                {
                                    Id = q.Id,
                                    UserId = q.UserId,
                                    AnswerCount = dbContext.Answers.Where(a => a.QuestionId == q.Id).Count(),
                                    AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(),
                                    Title = q.Title,
                                    Content = q.Content,
                                    UserHeadImageUrl = u.HeadImageUrl,
                                    UserNickName = u.NickName
                                };
            return questionQuery.ToList();
        }
        public QuestionDetailDTO GetQuestionDetailById(int questionId)
        {
            var answersQuery = from a in dbContext.Answers
                               where a.QuestionId == questionId
                               join fu in dbContext.Users on a.FromUserId equals fu.Id
                               join tu in dbContext.Users on a.ToUserId equals tu.Id
                               select new AnswerDTO
                               {
                                   Content = a.Content,
                                   CreateTime = a.CreateTime,
                                   FromUserId = a.FromUserId,
                                   ToUserId = a.ToUserId,
                                   FromUserNickName = fu.NickName,
                                   ToUserNickName = tu.NickName,
                                   QuestionId = a.QuestionId,
                                   NiceCount = dbContext.Nices.Where(n => n.AnswerId == a.Id).Count()
                               };
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
                                          UserHeadImageUrl = u.HeadImageUrl,
                                          Answers = answersQuery.ToList()
                                      };
            return questionDetailQuery.FirstOrDefault();
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

        public int UpdateQuestion(string userId,int questionId, Question question)
        {
            var q = dbContext.Questions.Find(questionId);
            if(q==null||q.UserId!=userId)
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


        public int DeleteQuestionById(string userId,int questionId)
        {
            Question question = dbContext.Questions.Find(questionId);
            if (question == null)
            {
                return 0;
            }
            if(question.UserId!=userId)
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