using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApiTest4.Models.EgeModels
{
    public class EgeDbContext : IdentityDbContext<User, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public EgeDbContext()
            : base("DefaultConnection")
        {
        }
        
        public static EgeDbContext Create()
        {
            return new EgeDbContext();
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

        public virtual DbSet<TrainType> TrainTypes { get; set; }
        public virtual DbSet<TaskTopic> TaskTopics { get; set; }
        public virtual DbSet<EgeTask> Tasks { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<UserTaskAttempt> UserTaskAttempts { get; set; }
    }
}