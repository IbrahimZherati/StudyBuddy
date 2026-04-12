using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Infrastructure.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleType> ArticleTypes { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Day> Days { get; set; }
    public virtual DbSet<ClientUserAvailableDay> ClientUserAvailableDays { get; set; }

    public virtual DbSet<ClientFile> ClientFiles { get; set; }

    public virtual DbSet<ClientUser> ClientUsers { get; set; }

    public virtual DbSet<ClientUserGroupChat> ClientUserGroupChats { get; set; }

    public virtual DbSet<ClientUserSkill> ClientUserSkills { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Feed> Feeds { get; set; }

    public virtual DbSet<FeedReplay> FeedReplays { get; set; }

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

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<University> Universities { get; set; }
    public virtual DbSet<ClientUserLikeFeed> ClientUserLikeFeeds { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Article");

            entity.ToTable("Article");

            entity.Property(e => e.Discription).HasColumnType("text");

            entity.HasOne(d => d.ArticleType).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ArticleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Article_ArticleType");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Article_ClientUser");
        });

        modelBuilder.Entity<ArticleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_ArticleType");

            entity.ToTable("ArticleType");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_City");

            entity.ToTable("City");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_City_Country");
        });

        modelBuilder.Entity<ClientFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_ClientFile");

            entity.ToTable("ClientFile");

            entity.Property(e => e.Bin).HasColumnName("bin");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.ClientFiles)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientFile_ClientUser");
        });

        modelBuilder.Entity<ClientUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_ClientUser");

            entity.ToTable("ClientUser");

            entity.Property(e => e.Gender).HasDefaultValue(true);

            entity.HasOne(d => d.City).WithMany(p => p.ClientUsers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUser_City");

            entity.HasOne(d => d.Country).WithMany(p => p.ClientUsers)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUser_Country");

            entity.HasOne(d => d.Major).WithMany(p => p.ClientUsers)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("fk_ClientUser_Major");

            entity.HasOne(d => d.University).WithMany(p => p.ClientUsers)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("fk_ClientUser_University");

            entity.HasOne(d => d.User).WithMany(p => p.ClientUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUser_AspNetUsers");
        });

        modelBuilder.Entity<ClientUserGroupChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_ClientUserGroupChat");

            entity.ToTable("ClientUserGroupChat");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.ClientUserGroupChats)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUserGroupChat_ClientUser");

            entity.HasOne(d => d.GroupChat).WithMany(p => p.ClientUserGroupChats)
                .HasForeignKey(d => d.GroupChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUserGroupChat_GroupChat");
        });

        modelBuilder.Entity<ClientUserSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_ClientUserSkill");

            entity.ToTable("ClientUserSkill");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.ClientUserSkills)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUserSkill_ClientUser");

            entity.HasOne(d => d.Skill).WithMany(p => p.ClientUserSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ClientUserSkill_Skill");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Country");

            entity.ToTable("Country");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Event");

            entity.ToTable("Event");

            entity.Property(e => e.Description).HasColumnType("text");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.Events)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Event_ClientUser");
        });

        modelBuilder.Entity<Feed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Feed");

            entity.ToTable("Feed");

            entity.Property(e => e.Description).HasColumnType("text");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.Feeds)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Feed_ClientUser");
        });

        modelBuilder.Entity<FeedReplay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_FeedReplay");

            entity.ToTable("FeedReplay");

            entity.HasOne(d => d.Feed).WithMany(p => p.FeedReplays)
                .HasForeignKey(d => d.FeedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FeedReplay_Feed");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Friend");

            entity.ToTable("Friend");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.FriendClientUsers)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Friend_ClientUser");

            entity.HasOne(d => d.FriendNavigation).WithMany(p => p.FriendFriendNavigations)
                .HasForeignKey(d => d.FriendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Friend_ClientUser_0");
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_FriendRequest");

            entity.ToTable("FriendRequest");

            entity.Property(e => e.IsAccepted).HasDefaultValue(false);

            entity.HasOne(d => d.ClientUser).WithMany(p => p.FriendRequestClientUsers)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FriendRequest_ClientUser");

            entity.HasOne(d => d.Friend).WithMany(p => p.FriendRequestFriends)
                .HasForeignKey(d => d.FriendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FriendRequest_ClientUser_0");
        });

        modelBuilder.Entity<GroupChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Tbl");

            entity.ToTable("GroupChat");

            entity.HasOne(d => d.Major).WithMany(p => p.GroupChats)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GroupChat_Major");

            entity.HasOne(d => d.University).WithMany(p => p.GroupChats)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GroupChat_University");
        });

        modelBuilder.Entity<GroupMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_GroupMessage");

            entity.ToTable("GroupMessage");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.FromClientUser).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.FromClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GroupMessage_ClientUser");

            entity.HasOne(d => d.GroupChat).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.GroupChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GroupMessage_GroupChat");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Major");

            entity.ToTable("Major");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Message");

            entity.ToTable("Message");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Text).HasColumnType("text");

            entity.HasOne(d => d.FromClientUser).WithMany(p => p.MessageFromClientUsers)
                .HasForeignKey(d => d.FromClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Message_ClientUser_0");

            entity.HasOne(d => d.ToClientUser).WithMany(p => p.MessageToClientUsers)
                .HasForeignKey(d => d.ToClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Message_ClientUser");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Note");

            entity.ToTable("Note");

            entity.Property(e => e.Notes).HasColumnType("text");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.Notes)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Note_ClientUser");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Notification");

            entity.ToTable("Notification");

            entity.HasOne(d => d.FromClientUser).WithMany(p => p.NotificationFromClientUsers)
                .HasForeignKey(d => d.FromClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notification_ClientUser_0");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notification_NotificationType");

            entity.HasOne(d => d.ToClientUser).WithMany(p => p.NotificationToClientUsers)
                .HasForeignKey(d => d.ToClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notification_ClientUser");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_NotificationType");

            entity.ToTable("NotificationType");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Post");

            entity.ToTable("Post");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Text).HasColumnType("text");

            entity.HasOne(d => d.ClientUser).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ClientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Post_ClientUser");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Skill");

            entity.ToTable("Skill");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_University");

            entity.ToTable("University");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
