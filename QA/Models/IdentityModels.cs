using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Entity;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace QA.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加个人资料数据，若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
        public string HeadImageUrl { get; set; }
        public DateTime CreateTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MyDatabaseInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries().Where(e => e.Entity is ICreateAndModify && (e.State == EntityState.Added || e.State == EntityState.Modified)).Select(e => e.Entity as ICreateAndModify))
            {
                history.ModifyTime = DateTime.Now;
                if (history.CreateTime == DateTime.MinValue)
                {
                    history.CreateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionAttention> QuestionAttentions { get; set; }
        public DbSet<UserAttention> UserAttentions { get; set; }
        public DbSet<Nice> Nices { get; set; }

    }
}