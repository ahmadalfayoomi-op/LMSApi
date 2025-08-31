using AutoMapper;
using LMS.Application.DTOs.Assignment;
using LMS.Application.DTOs.Auth;
using LMS.Application.DTOs.Course;
using LMS.Application.DTOs.Others;
using LMS.Application.DTOs.Student;
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


            // Register mappings
            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>();

            CreateMap<Student, RegisterDto>();
            CreateMap<RegisterDto, Student>();



            // Role mappings
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Course mappings
            CreateMap<Course, CourseDto>();
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

        }
    }
}
