using AutoMapper;
using LMS.Application.DTOs.Others;
using LMS.Application.Interfaces.Others;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Others
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BadgeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BadgeDto>> GetAllAsync(CancellationToken ct = default)
        {
            var badges = await _context.Badges.ToListAsync(ct);
            return _mapper.Map<IEnumerable<BadgeDto>>(badges);
        }

        public async Task<BadgeDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var badge = await _context.Badges.FindAsync(new object[] { id }, ct);
            return badge == null ? null : _mapper.Map<BadgeDto>(badge);
        }

        public async Task<BadgeDto> AddAsync(BadgeDto badgeDto, CancellationToken ct = default)
        {
            var badge = _mapper.Map<Badge>(badgeDto);
            _context.Badges.Add(badge);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<BadgeDto>(badge);
        }

        public async Task<BadgeDto> UpdateAsync(BadgeDto badgeDto, CancellationToken ct = default)
        {
            var badge = await _context.Badges.FindAsync(new object[] { badgeDto.Id }, ct);
            if (badge == null) throw new KeyNotFoundException("Badge not found");

            _mapper.Map(badgeDto, badge);
            _context.Badges.Update(badge);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<BadgeDto>(badge);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var badge = await _context.Badges.FindAsync(new object[] { id }, ct);
            if (badge != null)
            {
                _context.Badges.Remove(badge);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
