using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using QA.Models;

namespace QA.Repo
{
    public class NiceRepository : INiceRepository
    {
        private ApplicationDbContext dbContext;
        public NiceRepository()
        {
            dbContext = new ApplicationDbContext();
        }
        public Nice GetNiceDetail(int niceId)
        {
            var query = from n in dbContext.Nices where n.Id == niceId select n;
            return query.FirstOrDefault();
        }
        public int CreateNice(Nice nice)
        {
            var niceQuery = from n in dbContext.Nices
                                         where n.AnswerId == nice.AnswerId && n.UserId == nice.UserId
                                         select n;
            if (niceQuery.Count() > 0)
            {
                return 0;
            }
            dbContext.Nices.Add(nice);
            dbContext.SaveChanges();
            return 1;
        }


        public int RemoveNice(string userId, int answerId)
        {
            Nice nice = dbContext.Nices.Where(n => n.UserId == userId && n.AnswerId == answerId).FirstOrDefault();
            if (nice == null)
            {
                return 0;
            }
            if (nice.UserId != userId)
            {
                return 0;
            }
            dbContext.Nices.Remove(nice);
            dbContext.SaveChanges();
            return 1;
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}