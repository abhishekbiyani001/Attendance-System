using AttendanceSystem.Models;

namespace AttendanceSystem.Data
{
    public class AttendanceService
    {
        private readonly AttendancedbContext _context;

        public AttendanceService(AttendancedbContext context)
        {
            _context = context;
        }
        public List<AttendanceRecord> GetFilteredAttendanceRecords(string searchTerm, DateTime? searchDate)
        {
            var query = _context.AttendanceRecords.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(record => _context.Students
                    .Any(student => student.Id == record.StudentId && student.Name.Contains(searchTerm)));
            }

            if (searchDate.HasValue)
            {
                query = query.Where(record => record.Date == searchDate.Value);
            }

            return query.ToList();
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public void RecordAttendance(int studentId, bool isPresent, DateTime dateOfAttendance, string studentName)
        {
            var existingRecord = _context.AttendanceRecords.FirstOrDefault(ar => ar.StudentId == studentId && ar.Date.Date == dateOfAttendance.Date);

            if (existingRecord != null)
            {
                existingRecord.IsPresent = isPresent;
            }
            else
            {
                var newRecord = new AttendanceRecord(studentId, dateOfAttendance, isPresent, studentName);

                _context.AttendanceRecords.Add(newRecord);
                _context.SaveChanges();
            }
        }

        public void DeleteAttendanceRecord(int recordId)
        {
            //var recordToRemove = _context.AttendanceRecords.FirstOrDefault(ar => ar.StudentId == recordId);
            var recordToRemove = _context.AttendanceRecords.Where(ar => ar.StudentId == recordId).ToList();
            if (recordToRemove.Any())
            {
                _context.AttendanceRecords.RemoveRange(recordToRemove);
                _context.SaveChanges();
            }
        }

        public List<AttendanceRecord> GetAttendanceRecords()
        {
            return _context.AttendanceRecords.ToList();
        }
    }
}