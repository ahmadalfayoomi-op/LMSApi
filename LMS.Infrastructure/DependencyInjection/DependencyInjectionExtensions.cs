using Infrastructure.Repositories.Course;
using Infrastructure.Repositories.Instructor;
using Infrastructure.Repositories.Student;
using Infrastructure.Repositories.User;
using LMS.Application.Interfaces.Assignment;
using LMS.Application.Interfaces.Auth;
using LMS.Application.Interfaces.Chat;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Application.Interfaces.Instructor;
using LMS.Application.Interfaces.Lesson;
using LMS.Application.Interfaces.Others;
using LMS.Application.Interfaces.Quizzes;
using LMS.Application.Interfaces.Student;
using LMS.Application.Interfaces.Ticket;
using LMS.Application.Interfaces.User;
using LMS.Infrastructure.Repositories.Assignment;
using LMS.Infrastructure.Repositories.Auth;
using LMS.Infrastructure.Repositories.Chat;
using LMS.Infrastructure.Repositories.Configuration;
using LMS.Infrastructure.Repositories.Course;
using LMS.Infrastructure.Repositories.Lesson;
using LMS.Infrastructure.Repositories.Others;
using LMS.Infrastructure.Repositories.Quizzes;
using LMS.Infrastructure.Repositories.Student;
using LMS.Infrastructure.Repositories.Ticket;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace LMS.Infrastructure.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICateogryRepository, CateogryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IStudentAssignmentRepository, StudentAssignmentRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ICourseEventRepository, CourseEventRepository>();
            services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
            services.AddScoped<ICourseViewLogRepository, CourseViewLogRepository>();
            services.AddScoped<IFavoriteCourseRepository, FavoriteCourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizGeneratorService, QuizGeneratorService>();
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            services.AddScoped<IStudentActivityLogRepository, StudentActivityLogRepository>();
            services.AddScoped<IStudentBadgeRepository, StudentBadgeRepository>();
            services.AddScoped<IStudentNoteRepository, StudentNoteRepository>();
            services.AddScoped<IStudentProgressRepository, StudentProgressRepository>();
            services.AddScoped<IStudentQuizAttemptRepository, StudentQuizAttemptRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IKnowledgeBaseArticleRepository, KnowledgeBaseArticleRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();



            services.AddScoped<IWhisperService>(sp =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();

                var baseDir = AppContext.BaseDirectory;

                var whisperExe = Path.Combine(baseDir, @"LMS.Tools\whisper.cpp\build\bin\Release\whisper-cli.exe");
                var modelPath = Path.Combine(baseDir, @"LMS.Tools\whisper.cpp\models\ggml-base.en.bin");
                var ffmpegPath = @"C:\Projects\ffmpeg-2025-08-25-git-1b62f9d3ae-full_build\ffmpeg-2025-08-25-git-1b62f9d3ae-full_build\bin\ffmpeg.exe";

                return new WhisperService(env, whisperExe, modelPath, ffmpegPath);
            });



            return services;
        }
    }

}
