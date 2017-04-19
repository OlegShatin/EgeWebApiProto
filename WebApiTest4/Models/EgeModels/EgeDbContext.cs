using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApiTest4.Parsing;

namespace WebApiTest4.Models.EgeModels
{
    public class EgeDbContext : IdentityDbContext<User, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        //static EgeDbContext()
        //{
        //    //Database.SetInitializer<EgeDbContext>(new DropCreateDatabaseAlways<EgeDbContext>());
        //    Database.SetInitializer<EgeDbContext>(new EgeDbIntializer());
        //}
        public EgeDbContext()
            : base("DefaultConnection")
        {
            
        }
        
        public static EgeDbContext Create()
        {
            var result = new EgeDbContext();
            //Scanner scanner = new Scanner();
            //var synchTask = scanner.AddNewTasks(result);
            //synchTask.Wait();
            return result;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserRoleIntPk>().ToTable("UserRole");
            modelBuilder.Entity<UserLoginIntPk>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaimIntPk>().ToTable("UserClaim");
            modelBuilder.Entity<RoleIntPk>().ToTable("Role");


        }

        //public virtual DbSet<TrainType> TrainTypes { get; set; }
        public virtual DbSet<TaskTopic> TaskTopics { get; set; }
        public virtual DbSet<EgeTask> Tasks { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<EgeTrain> EgeTrains { get; set; }
        public virtual DbSet<FreeTrain> FreeTrains { get; set; }
        public virtual DbSet<UserTaskAttempt> UserTaskAttempts { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        
    }

    class EgeDbIntializer : IDatabaseInitializer<EgeDbContext>
    {
        protected void Seed(EgeDbContext db)
        {
            using (db)
            {
                
                Scanner scanner = new Scanner();
                var synchTask = scanner.AddNewTasks(db);
                synchTask.Wait();
            }
            

        }

        public void InitializeDatabase(EgeDbContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}