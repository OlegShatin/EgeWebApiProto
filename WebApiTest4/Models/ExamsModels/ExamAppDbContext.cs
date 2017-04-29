using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApiTest4.Parsing;

namespace WebApiTest4.Models.ExamsModels
{
    public class ExamAppDbContext : IdentityDbContext<User, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        //static ExamAppDbContext()
        //{
        //    //Database.SetInitializer<ExamAppDbContext>(new DropCreateDatabaseAlways<ExamAppDbContext>());
        //    Database.SetInitializer<ExamAppDbContext>(new EgeDbIntializer());
        //}
        public ExamAppDbContext()
            : base("DefaultConnection")
        {
            
        }
        
        public static ExamAppDbContext Create()
        {
            var result = new ExamAppDbContext();
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

        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<TaskTopic> TaskTopics { get; set; }
        public virtual DbSet<ExamTask> Tasks { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<ExamTrain> ExamTrains { get; set; }
        public virtual DbSet<FreeTrain> FreeTrains { get; set; }
        public virtual DbSet<UserTaskAttempt> UserTaskAttempts { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        
    }

    class EgeDbIntializer : IDatabaseInitializer<ExamAppDbContext>
    {
        protected void Seed(ExamAppDbContext db)
        {
            using (db)
            {
                
                Scanner scanner = new Scanner();
                var synchTask = scanner.AddNewTasks(db);
                synchTask.Wait();
            }
            

        }

        public void InitializeDatabase(ExamAppDbContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}