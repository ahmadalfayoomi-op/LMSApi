using LMS.Application.Common;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options,
                            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Quiz> Quizzes => Set<Quiz>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<StudentAnswer> StudentAnswers => Set<StudentAnswer>();
        public DbSet<StudentQuizAttempt> StudentQuizAttempts => Set<StudentQuizAttempt>();
        public DbSet<StudentProgress> StudentProgress => Set<StudentProgress>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<StudentAssignment> StudentAssignments => Set<StudentAssignment>();
        public DbSet<Badge> Badges => Set<Badge>();
        public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
        public DbSet<Certificate> Certificates => Set<Certificate>();
        public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<ChatRoomParticipant> ChatRoomParticipants => Set<ChatRoomParticipant>();
        public DbSet<CourseEvent> CourseEvents => Set<CourseEvent>();
        public DbSet<CourseReview> CourseReviews => Set<CourseReview>();
        public DbSet<Discussion> Discussions => Set<Discussion>();
        public DbSet<DiscussionReply> DiscussionReplies => Set<DiscussionReply>();
        public DbSet<StudentBadge> StudentBadges => Set<StudentBadge>();
        public DbSet<StudentNote> StudentNotes => Set<StudentNote>();
        public DbSet<StudentActivityLog> StudentActivityLogs => Set<StudentActivityLog>();
        public DbSet<CourseViewLog> CourseViewLogs => Set<CourseViewLog>();
        public DbSet<FavoriteCourse> FavoriteCourses => Set<FavoriteCourse>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketMessage> TicketMessages => Set<TicketMessage>();
        public DbSet<KnowledgeBaseArticle> KnowledgeBaseArticles => Set<KnowledgeBaseArticle>();
        public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();
        public DbSet<TicketAttachment> TicketAttachments => Set<TicketAttachment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentBadge>()
                .HasOne(sb => sb.Student)
                .WithMany(s => s.StudentBadges)
                .HasForeignKey(sb => sb.StudentId);

            builder.Entity<StudentBadge>()
                .HasOne(sb => sb.Badge)
                .WithMany(b => b.StudentBadges)
                .HasForeignKey(sb => sb.BadgeId);

            builder.Entity<Lesson>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            builder.Entity<TicketMessage>()
                .HasOne(tm => tm.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(tm => tm.TicketId);

            builder.Entity<TicketMessage>()
                .HasOne(tm => tm.Sender)
                .WithMany()
                .HasForeignKey(tm => tm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_UserRole_RoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_UserRole_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });

            builder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId");
                        j.ToTable("RolePermissions");
                    });
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            int? currentUserId = null;

            if (_httpContextAccessor?.HttpContext != null)
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int uid))
                    currentUserId = uid;
            }

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property(e => e.CreatedAt).CurrentValue == default)
                        entry.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;

                    if (entry.Property(e => e.UpdatedAt).CurrentValue == default)
                        entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;

                    if (currentUserId.HasValue)
                        entry.Property(e => e.CreatedById).CurrentValue = currentUserId.Value;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;

                    if (currentUserId.HasValue)
                        entry.Property(e => e.UpdatedById).CurrentValue = currentUserId.Value;
                }
            }
        }
    }
}
