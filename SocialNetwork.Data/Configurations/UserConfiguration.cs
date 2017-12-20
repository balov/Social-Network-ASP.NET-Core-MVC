using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
              .HasMany(u => u.MessagesReceived)
              .WithOne(m => m.Receiver)
              .HasForeignKey(m => m.ReceiverId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasMany(u => u.MessagesSent)
              .WithOne(m => m.Sender)
              .HasForeignKey(m => m.SenderId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(u => u.Friends)
                .WithOne(uf => uf.User)
                .HasForeignKey(uf => uf.UserId);

            builder
               .HasMany(u => u.OtherFriends)
               .WithOne(uf => uf.Friend)
               .HasForeignKey(uf => uf.FriendId);

            builder
                .HasMany(u => u.Photos)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            builder
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}