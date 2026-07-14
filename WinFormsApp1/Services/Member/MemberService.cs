using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class MemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public MemberService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IReadOnlyList<object>> GetAllAsync()
        {
            var members = await _unitOfWork.Repository<Member>().GetAllAsync();
            return members.Select(m => new
            {
                m.Id,
                FullName = $"{m.FirstName} {m.LastName}",
                m.Email,
                MemberType = m.MemberType.ToString(),
                Status = m.Status.ToString(),
                m.Department,
                m.StudentId
            }).ToList();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            var results = await _unitOfWork.Repository<Member>()
                .FindAsync(m => m.Id == id);
            var member = results.FirstOrDefault();
            if (member != null)
            {
                // Force load LibraryCard if not already loaded
                _ = _context.Entry(member).Reference(m => m.LibraryCard).LoadAsync();
            }
            return member;
        }

        public async Task<(bool Success, string Message)> CreateAsync(Member member)
        {
            if (await _context.Members.AnyAsync(m => m.Email == member.Email))
                return (false, "A member with this email already exists.");

            member.CreatedAt = DateTime.UtcNow;
            member.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Member>().AddAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return (true, $"Member '{member.FirstName} {member.LastName}' created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateAsync(Member member)
        {
            if (await _context.Members.AnyAsync(m => m.Email == member.Email && m.Id != member.Id))
                return (false, "Another member with this email already exists.");

            member.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Member>().UpdateAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return (true, $"Member '{member.FirstName} {member.LastName}' updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var member = await _unitOfWork.Repository<Member>().GetByIdAsync(id);
            if (member == null)
                return (false, "Member not found.");

            bool hasActiveBorrows = await _context.BorrowRecords
                .AnyAsync(br => br.MemberId == id && br.Status == BorrowStatus.Active);

            if (hasActiveBorrows)
                return (false, "Cannot delete member with active borrow records.");

            await _unitOfWork.Repository<Member>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return (true, $"Member '{member.FirstName} {member.LastName}' deleted successfully.");
        }

        public async Task<IReadOnlyList<object>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            var lowerKeyword = keyword.ToLower();

            var members = await _unitOfWork.Repository<Member>()
                .FindAsync(m =>
                    m.FirstName.ToLower().Contains(lowerKeyword) ||
                    m.LastName.ToLower().Contains(lowerKeyword) ||
                    m.Email.ToLower().Contains(lowerKeyword) ||
                    (m.StudentId != null && m.StudentId.ToLower().Contains(lowerKeyword)));

            return members.Select(m => new
            {
                m.Id,
                FullName = $"{m.FirstName} {m.LastName}",
                m.Email,
                MemberType = m.MemberType.ToString(),
                Status = m.Status.ToString(),
                m.Department,
                m.StudentId
            }).ToList();
        }

        public async Task<(int Imported, int Updated, int Skipped, string Message)> ImportFromCsvAsync(string filePath)
        {
            int imported = 0;
            int updated = 0;
            int skipped = 0;
            var errors = new List<string>();

            var lines = await File.ReadAllLinesAsync(filePath);

            if (lines.Length < 2)
                return (0, 0, 0, "CSV file is empty or has no data rows.");

            // Skip header row
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    skipped++;
                    continue;
                }

                var parts = ParseCsvLine(line);
                if (parts.Length < 5)
                {
                    errors.Add($"Row {i + 1}: Insufficient columns.");
                    skipped++;
                    continue;
                }

                var firstName = parts[0].Trim();
                var lastName = parts[1].Trim();
                var email = parts[2].Trim();
                var phone = parts[3].Trim();
                var memberTypeStr = parts[4].Trim();
                var department = parts.Length > 5 ? parts[5].Trim() : null;
                var studentId = parts.Length > 6 ? parts[6].Trim() : null;

                // Validate MemberType
                if (!Enum.TryParse<MemberType>(memberTypeStr, true, out var memberType))
                {
                    throw new InvalidOperationException(
                        $"Row {i + 1}: Invalid MemberType '{memberTypeStr}'. Valid values: Student, Teacher, Staff, External.");
                }

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(email))
                {
                    errors.Add($"Row {i + 1}: FirstName and Email are required.");
                    skipped++;
                    continue;
                }

                // Find existing by Email or StudentId
                var existing = await _context.Members
                    .FirstOrDefaultAsync(m => m.Email == email);

                if (existing == null && !string.IsNullOrWhiteSpace(studentId))
                {
                    existing = await _context.Members
                        .FirstOrDefaultAsync(m => m.StudentId == studentId);
                }

                if (existing != null)
                {
                    // Update existing
                    existing.FirstName = firstName;
                    existing.LastName = lastName;
                    existing.Email = email;
                    existing.Phone = phone;
                    existing.MemberType = memberType;
                    existing.Department = string.IsNullOrWhiteSpace(department) ? null : department;
                    existing.StudentId = string.IsNullOrWhiteSpace(studentId) ? null : studentId;
                    existing.UpdatedAt = DateTime.UtcNow;
                    updated++;
                }
                else
                {
                    // Create new
                    var member = new Member
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Phone = phone,
                        MemberType = memberType,
                        Department = string.IsNullOrWhiteSpace(department) ? null : department,
                        StudentId = string.IsNullOrWhiteSpace(studentId) ? null : studentId,
                        Status = MemberStatus.Active,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _unitOfWork.Repository<Member>().AddAsync(member);
                    imported++;
                }
            }

            await _unitOfWork.SaveChangesAsync();

            string message = $"Import complete: {imported} created, {updated} updated, {skipped} skipped.";
            if (errors.Count > 0)
                message += $" Errors: {string.Join("; ", errors)}";

            return (imported, updated, skipped, message);
        }

        private static string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var current = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            result.Add(current.ToString());
            return result.ToArray();
        }
    }
}
