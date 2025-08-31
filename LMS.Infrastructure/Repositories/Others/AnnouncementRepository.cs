using AutoMapper;
using LMS.Application.DTOs.Others;
using LMS.Application.Interfaces.Others;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Others
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AnnouncementRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default)
        {
            var announcements = await _context.Announcements
                .Where(a => a.CourseId == courseId)
                .OrderByDescending(a => a.PublishedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var announcement = await _context.Announcements.FindAsync(new object[] { id }, ct);
            return announcement == null ? null : _mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task<AnnouncementDto> AddAsync(AnnouncementDto announcementDto, CancellationToken ct = default)
        {
            var announcement = _mapper.Map<Announcement>(announcementDto);
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task<AnnouncementDto> UpdateAsync(AnnouncementDto announcementDto, CancellationToken ct = default)
        {
            var announcement = await _context.Announcements.FindAsync(new object[] { announcementDto.Id }, ct);
            if (announcement == null) throw new KeyNotFoundException("Announcement not found");

            _mapper.Map(announcementDto, announcement);
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var announcement = await _context.Announcements.FindAsync(new object[] { id }, ct);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
