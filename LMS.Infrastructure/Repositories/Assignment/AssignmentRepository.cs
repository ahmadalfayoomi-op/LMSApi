using AutoMapper;
using LMS.Application.DTOs.Assignment;
using LMS.Application.Interfaces.Assignment;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Assignment
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AssignmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssignmentDto>> GetAllByCourseAsync(int courseId, CancellationToken ct = default)
        {
            var assignments = await _context.Assignments
                .Where(a => a.CourseId == courseId)
                .OrderBy(a => a.DueDate)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
        }

        public async Task<AssignmentDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var assignment = await _context.Assignments.FindAsync(new object[] { id }, ct);
            return assignment == null ? null : _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task<AssignmentDto> AddAsync(AssignmentDto assignmentDto, CancellationToken ct = default)
        {
            var assignment = _mapper.Map<LMS.Domain.Entities.Assignment>(assignmentDto);
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task<AssignmentDto> UpdateAsync(AssignmentDto assignmentDto, CancellationToken ct = default)
        {
            var assignment = await _context.Assignments.FindAsync(new object[] { assignmentDto.Id }, ct);
            if (assignment == null) throw new KeyNotFoundException("Assignment not found");

            _mapper.Map(assignmentDto, assignment);
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var assignment = await _context.Assignments.FindAsync(new object[] { id }, ct);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync(ct);
            }
        }
    }

}
