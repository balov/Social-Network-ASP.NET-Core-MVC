using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Configurations;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data
{
    public class SocialNetworkDbContext : IdentityDbContext<User>
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<FriendRequest> FriendRequests { get; set; }

        public DbSet<UserFriend> UserFriend { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FriendRequestConfiguration());
            builder.ApplyConfiguration(new UserInterestConfiguration());
            builder.ApplyConfiguration(new EventUserConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());

            base.OnModelCreating(builder);
        }
    }
}