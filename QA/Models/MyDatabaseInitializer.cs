using System.Data.Entity;

namespace QA.Models
{
    class MyDatabaseInitializer:DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //TODO add some initialization
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
