using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace QA.Models
{
    class MyDatabaseInitializer:DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //TODO add some initialization
            ApplicationUser user = new ApplicationUser();
            user.UserName = "xuyongjie1128@hotmail.com";
            user.PasswordHash = new PasswordHasher().HashPassword("123456");
            user.Email = "xuyongjie1128@hotmail.com";
            user.NickName = "YoungJay";
            user.PhoneNumber = "18867101652";
            user.HeadImageUrl = "";
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
