using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTOs.Student
{
    public class LessonDto
    {
        public int Id { get; set; }               
        public string Title { get; set; } = string.Empty;
        public string ContentUrl { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
