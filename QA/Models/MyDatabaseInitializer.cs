using Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QA.Util;
using System;
using System.Data.Entity;

namespace QA.Models
{
    class MyDatabaseInitializer:DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //TODO add some initialization
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationUser user1 = new ApplicationUser();
            user1.UserName = "xuyongjie1128@hotmail.com";
            user1.PasswordHash = new PasswordHasher().HashPassword("123456");
            user1.Email = "xuyongjie1128@hotmail.com";
            user1.NickName = "YoungJay";
            user1.PhoneNumber = "18867101652";
            user1.HeadImageUrl = Helper.DEFAULT_HEAD_IMAGE_URL;
            user1.CreateTime = DateTime.Now;
            manager.Create(user1);

            ApplicationUser user2 = new ApplicationUser();
            user2.UserName = "854207907@qq.com";
            user2.PasswordHash = new PasswordHasher().HashPassword("123456");
            user2.Email = "854207907@qq.com";
            user2.NickName = "YoungJay QQ";
            user2.PhoneNumber = "18867101652";
            user2.HeadImageUrl = Helper.DEFAULT_HEAD_IMAGE_URL;
            user2.CreateTime = DateTime.Now;
            manager.Create(user2);
            context.SaveChanges();

            Question question1 = new Question();
            question1.UserId = user1.Id;
            question1.Title = "Philosophy Problem";
            question1.Content = "Who am i, where i came from, where will i go?";
            context.Questions.Add(question1);

            Question question2 = new Question();
            question2.UserId = user2.Id;
            question2.Title = "Asp.net WebApi";
            question2.Content = "How to create a webapi hosted in web?";
            context.Questions.Add(question2);

            context.SaveChanges();

            QuestionAttention qa1 = new QuestionAttention();
            qa1.QuestionId = question1.Id;
            qa1.UserId = user1.Id;
            context.QuestionAttentions.Add(qa1);


            QuestionAttention qa2 = new QuestionAttention();
            qa2.QuestionId = question2.Id;
            qa2.UserId = user2.Id;
            context.QuestionAttentions.Add(qa2);

            QuestionAttention qa3 = new QuestionAttention();
            qa3.QuestionId = question1.Id;
            qa3.UserId = user2.Id;
            context.QuestionAttentions.Add(qa3);

            context.SaveChanges();

            Answer answer1 = new Answer();
            answer1.FromUserId = user2.Id;
            answer1.ToAnswerId = question1.UserId;
            answer1.QuestionId = question1.Id;
            answer1.Content = "You are a programer,you came from china,you will go to save the world!";
            context.Answers.Add(answer1);
            context.SaveChanges();

            Nice nice = new Nice();
            nice.AnswerId = answer1.Id;
            nice.UserId = user1.Id;
            context.Nices.Add(nice);
            context.SaveChanges();

            UserAttention ua = new UserAttention();
            ua.FromUserId = user2.Id;
            ua.ToUserId = user1.Id;
            context.UserAttentions.Add(ua);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
