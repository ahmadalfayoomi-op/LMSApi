using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure.Authorization
{
    public static class AuthorizationPolicies
    {
        public static IServiceCollection AddLmsAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Announcements permissions
                options.AddPolicy("Announcements.View", policy =>
                    policy.RequireClaim("permission", "Announcements.View"));

                options.AddPolicy("Announcements.Create", policy =>
                    policy.RequireClaim("permission", "Announcements.Create"));

                options.AddPolicy("Announcements.Update", policy =>
                    policy.RequireClaim("permission", "Announcements.Update"));

                options.AddPolicy("Announcements.Delete", policy =>
                    policy.RequireClaim("permission", "Announcements.Delete"));


                // -------------------------
                // Assignments permissions
                // -------------------------
                options.AddPolicy("Assignments.View", policy =>
                    policy.RequireClaim("permission", "Assignments.View"));

                options.AddPolicy("Assignments.Create", policy =>
                    policy.RequireClaim("permission", "Assignments.Create"));

                options.AddPolicy("Assignments.Update", policy =>
                    policy.RequireClaim("permission", "Assignments.Update"));

                options.AddPolicy("Assignments.Delete", policy =>
                    policy.RequireClaim("permission", "Assignments.Delete"));



                // Badges
                options.AddPolicy("Badges.View", p =>
                    p.RequireClaim("permission", "Badges.View"));
                options.AddPolicy("Badges.Create", p =>
                    p.RequireClaim("permission", "Badges.Create"));
                options.AddPolicy("Badges.Update", p =>
                    p.RequireClaim("permission", "Badges.Update"));
                options.AddPolicy("Badges.Delete", p =>
                    p.RequireClaim("permission", "Badges.Delete"));


                // Bookmarks
                options.AddPolicy("Bookmarks.View", p =>
                    p.RequireClaim("permission", "Bookmarks.View"));
                options.AddPolicy("Bookmarks.Create", p =>
                    p.RequireClaim("permission", "Bookmarks.Create"));
                options.AddPolicy("Bookmarks.Delete", p =>
                    p.RequireClaim("permission", "Bookmarks.Delete"));


                // Categories
                options.AddPolicy("Categories.View", p =>
                    p.RequireClaim("permission", "Categories.View"));
                options.AddPolicy("Categories.Create", p =>
                    p.RequireClaim("permission", "Categories.Create"));
                options.AddPolicy("Categories.Update", p =>
                    p.RequireClaim("permission", "Categories.Update"));
                options.AddPolicy("Categories.Delete", p =>
                    p.RequireClaim("permission", "Categories.Delete"));


                // Certificates
                options.AddPolicy("Certificates.View", p =>
                    p.RequireClaim("permission", "Certificates.View"));
                options.AddPolicy("Certificates.Create", p =>
                    p.RequireClaim("permission", "Certificates.Create"));
                options.AddPolicy("Certificates.Delete", p =>
                    p.RequireClaim("permission", "Certificates.Delete"));


                // Course Events
                options.AddPolicy("CourseEvents.View", p =>
                    p.RequireClaim("permission", "CourseEvents.View"));
                options.AddPolicy("CourseEvents.Create", p =>
                    p.RequireClaim("permission", "CourseEvents.Create"));
                options.AddPolicy("CourseEvents.Update", p =>
                    p.RequireClaim("permission", "CourseEvents.Update"));
                options.AddPolicy("CourseEvents.Delete", p =>
                    p.RequireClaim("permission", "CourseEvents.Delete"));


                // Course Reviews
                options.AddPolicy("CourseReviews.View", p =>
                    p.RequireClaim("permission", "CourseReviews.View"));
                options.AddPolicy("CourseReviews.Create", p =>
                    p.RequireClaim("permission", "CourseReviews.Create"));
                options.AddPolicy("CourseReviews.Update", p =>
                    p.RequireClaim("permission", "CourseReviews.Update"));
                options.AddPolicy("CourseReviews.Delete", p =>
                    p.RequireClaim("permission", "CourseReviews.Delete"));


                // Courses
                options.AddPolicy("Courses.View", p =>
                    p.RequireClaim("permission", "Courses.View"));
                options.AddPolicy("Courses.Create", p =>
                    p.RequireClaim("permission", "Courses.Create"));
                options.AddPolicy("Courses.Update", p =>
                    p.RequireClaim("permission", "Courses.Update"));
                options.AddPolicy("Courses.Delete", p =>
                    p.RequireClaim("permission", "Courses.Delete"));


                // Discussions
                options.AddPolicy("Discussions.View", p =>
                    p.RequireClaim("permission", "Discussions.View"));
                options.AddPolicy("Discussions.Create", p =>
                    p.RequireClaim("permission", "Discussions.Create"));
                options.AddPolicy("Discussions.Reply", p =>
                    p.RequireClaim("permission", "Discussions.Reply"));
                options.AddPolicy("Discussions.Delete", p =>
                    p.RequireClaim("permission", "Discussions.Delete"));
                options.AddPolicy("Discussions.DeleteReply", p =>
                    p.RequireClaim("permission", "Discussions.DeleteReply"));


                // Enrollments
                options.AddPolicy("Enrollments.View", p =>
                    p.RequireClaim("permission", "Enrollments.View"));
                options.AddPolicy("Enrollments.Create", p =>
                    p.RequireClaim("permission", "Enrollments.Create"));
                options.AddPolicy("Enrollments.Update", p =>
                    p.RequireClaim("permission", "Enrollments.Update"));
                options.AddPolicy("Enrollments.Delete", p =>
                    p.RequireClaim("permission", "Enrollments.Delete"));

                // FavoriteCourses
                options.AddPolicy("FavoriteCourses.View", p =>
                p.RequireClaim("permission", "FavoriteCourses.View"));
                options.AddPolicy("FavoriteCourses.Add", p =>
                    p.RequireClaim("permission", "FavoriteCourses.Add"));
                options.AddPolicy("FavoriteCourses.Remove", p =>
                    p.RequireClaim("permission", "FavoriteCourses.Remove"));


                // Lessons
                options.AddPolicy("Lessons.View", p =>
                p.RequireClaim("permission", "Lessons.View"));

                options.AddPolicy("Lessons.Create", p =>
                    p.RequireClaim("permission", "Lessons.Create"));

                options.AddPolicy("Lessons.Update", p =>
                    p.RequireClaim("permission", "Lessons.Update"));

                options.AddPolicy("Lessons.Delete", p =>
                    p.RequireClaim("permission", "Lessons.Delete"));


                // Quizzes
                options.AddPolicy("Quizzes.View", p =>
                p.RequireClaim("permission", "Quizzes.View"));

                options.AddPolicy("Quizzes.Create", p =>
                    p.RequireClaim("permission", "Quizzes.Create"));

                options.AddPolicy("Quizzes.Update", p =>
                    p.RequireClaim("permission", "Quizzes.Update"));

                options.AddPolicy("Quizzes.Delete", p =>
                    p.RequireClaim("permission", "Quizzes.Delete"));


                // Roles
                options.AddPolicy("Roles.View", p =>
                    p.RequireClaim("permission", "Roles.View"));

                options.AddPolicy("Roles.Create", p =>
                    p.RequireClaim("permission", "Roles.Create"));

                options.AddPolicy("Roles.Update", p =>
                    p.RequireClaim("permission", "Roles.Update"));

                options.AddPolicy("Roles.Delete", p =>
                    p.RequireClaim("permission", "Roles.Delete"));




                // StudentAssignments
                options.AddPolicy("StudentAssignments.View", p =>
                    p.RequireClaim("permission", "StudentAssignments.View"));

                options.AddPolicy("StudentAssignments.Submit", p =>
                    p.RequireClaim("permission", "StudentAssignments.Submit"));

                options.AddPolicy("StudentAssignments.Update", p =>
                    p.RequireClaim("permission", "StudentAssignments.Update"));

                options.AddPolicy("StudentAssignments.Delete", p =>
                    p.RequireClaim("permission", "StudentAssignments.Delete"));


                // StudentNotes
                options.AddPolicy("StudentNotes.View", p =>
                    p.RequireClaim("permission", "StudentNotes.View"));

                options.AddPolicy("StudentNotes.Create", p =>
                    p.RequireClaim("permission", "StudentNotes.Create"));

                options.AddPolicy("StudentNotes.Update", p =>
                    p.RequireClaim("permission", "StudentNotes.Update"));

                options.AddPolicy("StudentNotes.Delete", p =>
                    p.RequireClaim("permission", "StudentNotes.Delete"));


                // StudentQuizAttempts
                options.AddPolicy("StudentQuizAttempts.View", p =>
                    p.RequireClaim("permission", "StudentQuizAttempts.View"));

                options.AddPolicy("StudentQuizAttempts.Create", p =>
                    p.RequireClaim("permission", "StudentQuizAttempts.Create"));

                options.AddPolicy("StudentQuizAttempts.Update", p =>
                    p.RequireClaim("permission", "StudentQuizAttempts.Update"));

                options.AddPolicy("StudentQuizAttempts.Delete", p =>
                    p.RequireClaim("permission", "StudentQuizAttempts.Delete"));

                // Tickets
                options.AddPolicy("Tickets.View", p =>
                    p.RequireClaim("permission", "Tickets.View"));

                options.AddPolicy("Tickets.Create", p =>
                    p.RequireClaim("permission", "Tickets.Create"));

                options.AddPolicy("Tickets.Update", p =>
                    p.RequireClaim("permission", "Tickets.Update"));

                options.AddPolicy("Tickets.Delete", p =>
                    p.RequireClaim("permission", "Tickets.Delete"));

                options.AddPolicy("Tickets.GetMessages", p =>
                    p.RequireClaim("permission", "Tickets.GetMessages"));

                options.AddPolicy("Tickets.AddMessage", p =>
                    p.RequireClaim("permission", "Tickets.AddMessage"));



                options.AddPolicy("TicketCategory.View", p =>
                    p.RequireClaim("permission", "TicketCategory.View"));

                options.AddPolicy("TicketCategory.Create", p =>
                    p.RequireClaim("permission", "TicketCategory.Create"));

                options.AddPolicy("TicketCategory.Update", p =>
                    p.RequireClaim("permission", "TicketCategory.Update"));

                options.AddPolicy("TicketCategory.Delete", p =>
                    p.RequireClaim("permission", "TicketCategory.Delete"));


                options.AddPolicy("KnowledgeBaseArticle.View", p =>
                    p.RequireClaim("permission", "KnowledgeBaseArticle.View"));

                options.AddPolicy("KnowledgeBaseArticle.Create", p =>
                    p.RequireClaim("permission", "KnowledgeBaseArticle.Create"));

                options.AddPolicy("KnowledgeBaseArticle.Update", p =>
                    p.RequireClaim("permission", "KnowledgeBaseArticle.Update"));

                options.AddPolicy("KnowledgeBaseArticle.Delete", p =>
                    p.RequireClaim("permission", "KnowledgeBaseArticle.Delete"));




                options.AddPolicy("Student.View", p =>
                    p.RequireClaim("permission", "Student.View"));

                options.AddPolicy("Student.Create", p =>
                    p.RequireClaim("permission", "Student.Create"));

                options.AddPolicy("Student.Update", p =>
                    p.RequireClaim("permission", "Student.Update"));

                options.AddPolicy("Student.Delete", p =>
                    p.RequireClaim("permission", "Student.Delete"));



                options.AddPolicy("Instructor.View", p =>
                    p.RequireClaim("permission", "Instructor.View"));

                options.AddPolicy("Instructor.Create", p =>
                    p.RequireClaim("permission", "Instructor.Create"));

                options.AddPolicy("Instructor.Update", p =>
                    p.RequireClaim("permission", "Instructor.Update"));

                options.AddPolicy("Instructor.Delete", p =>
                    p.RequireClaim("permission", "Instructor.Delete"));

            });

            return services;
        }
    }
}
