﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using Entity.EntityDTO;
using QA.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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
            return dbContext.Questions.Select(q => new QuestionDTO() { Id = q.Id, UserId = q.UserId, AnswerCount = dbContext.Answers.Where(a => a.Id == q.Id).Count(), AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(), Content = q.Content, UserHeadImageUrl = dbContext.Users.First(u => u.Id == q.UserId).HeadImageUrl, UserNickName = dbContext.Users.First(u => u.Id == q.UserId).NickName });
        }

        public IEnumerable<QuestionDTO> GetAttentionQuestions(string userId)
        {
            return null;
        }

        public QuestionDetailDTO GetQuestionById(int questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionDTO> GetRaisedQuestions(string userId)
        {
            return dbContext.Questions.Where(q => q.UserId == userId).Select(q => new QuestionDTO() { Id = q.Id, UserId = q.UserId, AnswerCount = dbContext.Answers.Where(a => a.Id == q.Id).Count(), AttentionCount = dbContext.QuestionAttentions.Where(qa => qa.QuestionId == q.Id).Count(), Content = q.Content, UserHeadImageUrl = dbContext.Users.First(u => u.Id == q.UserId).HeadImageUrl, UserNickName = dbContext.Users.First(u => u.Id == q.UserId).NickName });
        }

        public IEnumerable<QuestionDTO> GetTimeLineAllQuestions(string userId)
        {
            return null;
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

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="question"></param>
        /// <returns>0表示该问题不存在；1表示更新成功；-1更新出错</returns>
        public int UpdateQuestion(int questionId, Question question)
        {

            dbContext.Entry(question).State = EntityState.Modified;
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(questionId))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            return 1;
        }


        public int DeleteQuestionById(int questionId)
        {
            Question question = dbContext.Questions.Find(questionId);
            if (question == null)
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