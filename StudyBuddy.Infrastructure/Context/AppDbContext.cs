using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Infrastructure.Context;

public partial class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<StudyInterest> StudyInterests { get; set; }

    public virtual DbSet<ArticleType> ArticleTypes { get; set; }

    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Day> Days { get; set; }
    public virtual DbSet<ClientUserAvailableDay> ClientUserAvailableDays { get; set; }

    public virtual DbSet<ClientFile> ClientFiles { get; set; }

    public virtual DbSet<ClientUser> ClientUsers { get; set; }

    public virtual DbSet<ClientUserGroupChat> ClientUserGroupChats { get; set; }

    public virtual DbSet<ClientUserSkill> ClientUserSkills { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

   

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<GroupChat> GroupChats { get; set; }

    public virtual DbSet<GroupMessage> GroupMessages { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<ClientUserLikePost> ClientUserLikePosts { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<University> Universities { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<GroupInvite> GroupInvites { get; set; }
    public virtual DbSet<ClientUserGroupMessageRead> ClientUserGroupMessageReads { get; set; }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.HasIndex(f => new { f.FirstFriendId, f.SecondFriendId }).IsUnique();

            entity.HasOne(f => f.FirstFriend)
                  .WithMany(u => u.FirstFriends)
                  .HasForeignKey(f => f.FirstFriendId)
                  .OnDelete(DeleteBehavior.Restrict); 

            entity.HasOne(f => f.SecondFriend)
                  .WithMany(u => u.SecondFriends)
                  .HasForeignKey(f => f.SecondFriendId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.HasIndex(f => new { f.FromClientUserId, f.ToClientUserId }).IsUnique();

            entity.HasOne(fr => fr.FromClientUser)
                  .WithMany(u => u.FriendRequestFromClientUsers)
                  .HasForeignKey(fr => fr.FromClientUserId)
                  .OnDelete(DeleteBehavior.Restrict); // Use Restrict to avoid circular delete errors

            entity.HasOne(fr => fr.ToClientUser)
                  .WithMany(u => u.FriendRequestToClientUsers)
                  .HasForeignKey(fr => fr.ToClientUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(f => f.Id);

           

            entity.HasOne(fr => fr.FromClientUser)
                  .WithMany(u => u.MessageFromClientUsers)
                  .HasForeignKey(fr => fr.FromClientUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(fr => fr.ToClientUser)
                  .WithMany(u => u.MessageToClientUsers)
                  .HasForeignKey(fr => fr.ToClientUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(f => f.Id);

           

            entity.HasOne(fr => fr.FromClientUser)
                  .WithMany(u => u.NotificationFromClientUsers)
                  .HasForeignKey(fr => fr.FromClientUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(fr => fr.ToClientUser)
                  .WithMany(u => u.NotificationToClientUsers)
                  .HasForeignKey(fr => fr.ToClientUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
