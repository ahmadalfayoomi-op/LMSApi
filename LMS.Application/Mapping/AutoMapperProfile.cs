using AutoMapper;
using LMS.Application.DTOs.Assignment;
using LMS.Application.DTOs.Auth;
using LMS.Application.DTOs.Chat;
using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Others;
using LMS.Application.DTOs.Quizzes;
using LMS.Application.DTOs.Student;
using LMS.Application.DTOs.Ticket;
using LMS.Application.DTOs.User;
using LMS.Domain.Entities;

namespace LMS.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles)) 
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom(src => src.ProfileImage));

            // UserDto -> User
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore()) 
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());



            CreateMap<StudentDto, User>()
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedById, opt => opt.Ignore());

            // Register mappings
            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>();

            CreateMap<Instructor, RegisterDto>();
            CreateMap<RegisterDto, Instructor>();


            CreateMap<RegisterDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ReverseMap();


            CreateMap<RegisterDto, Instructor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<RegisterDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<RegisterDto, Instructor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());      
            
            // Role mappings
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Course mappings
            CreateMap<Course, CourseDto>();
            CreateMap<CourseEvent, CourseEventDto>();
            CreateMap<CourseReview, CourseReviewDto>();
            CreateMap<CourseViewLog, CourseViewLogDto>();
            CreateMap<FavoriteCourse, FavoriteCourseDto>();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<CourseDto, Course>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Category mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Enrollment mappings
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<EnrollmentDto, Enrollment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Student mappings
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom(src => src.User.ProfileImage))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));

            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Bookmark, BookmarkDto>().ReverseMap();
            CreateMap<Certificate, CertificateDto>().ReverseMap();
            CreateMap<Discussion, DiscussionDto>().ReverseMap();
            CreateMap<DiscussionReply, DiscussionReplyDto>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
            CreateMap<StudentActivityLog, StudentActivityLogDto>().ReverseMap();
            CreateMap<StudentAnswer, StudentAnswerDto>().ReverseMap();
            CreateMap<StudentAssignment, StudentAssignmentDto>().ReverseMap();
            CreateMap<StudentBadge, StudentBadgeDto>().ReverseMap();
            CreateMap<StudentNote, StudentNoteDto>().ReverseMap();
            CreateMap<StudentProgress, StudentProgressDto>().ReverseMap();
            CreateMap<StudentQuizAttempt, StudentQuizAttemptDto>().ReverseMap();


            // Lesson mappings
            CreateMap<Lesson, LessonDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedById, opt => opt.Ignore());

            // Others mappings
            CreateMap<Announcement, AnnouncementDto>().ReverseMap();
            CreateMap<Badge, BadgeDto>().ReverseMap();


            // Assignment mappings
            CreateMap<Assignment, AssignmentDto>().ReverseMap();
            CreateMap<StudentAssignment, StudentAssignmentDto>().ReverseMap();

            // Chat mappings
            CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
            CreateMap<ChatRoom, ChatRoomDto>().ReverseMap();
            CreateMap<StudentAssignment, StudentAssignmentDto>().ReverseMap();


            // Quiz mappings
            CreateMap<Quiz, QuizDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();


            // Tickets mapping
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.TicketCategoryName, opt => opt.MapFrom(src => src.TicketCategory != null ? src.TicketCategory.Name : null))
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.User != null ? src.User.Username : null))
                .ForMember(dest => dest.AssignedToName, opt => opt.MapFrom(src => src.AssignedTo != null ? src.AssignedTo.Username : null))
                .ReverseMap();

            CreateMap<TicketMessage, TicketMessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Username))
                .ReverseMap();

            CreateMap<TicketAttachment, TicketAttachmentDto>().ReverseMap();

            CreateMap<KnowledgeBaseArticle, KnowledgeBaseArticleDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.TicketCategory != null ? src.TicketCategory.Name : null))
                .ReverseMap();

            CreateMap<TicketCategory, TicketCategoryDto>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.KnowledgeBaseArticles))
                .ReverseMap();


        }
    }
}
