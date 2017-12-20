using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Configurations
{
    public class EventUserConfiguration : IEntityTypeConfiguration<EventUser>
    {
        public void Configure(EntityTypeBuilder<EventUser> builder)
        {
            builder.HasKey(eu => new { eu.EventId, eu.UserId });

            builder
                .HasOne(eu => eu.User)
                .WithMany(u => u.Events)
                .HasForeignKey(eu => eu.UserId);

            builder
               .HasOne(eu => eu.Event)
               .WithMany(e => e.Participants)
               .HasForeignKey(eu => eu.EventId);
        }
    }
}