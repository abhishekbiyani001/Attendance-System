//using AttendanceSystem.Models;

//namespace AttendanceSystem.Data
//{
//    public class AttendanceService
//    {
//        private readonly List<Student> students = new List<Student>
//        {
//            new() { Id = 1, Name = "Abhishek Biyani", Email = "abhishek@gmail.com" },
//            new() { Id = 2, Name = "Pratik Kumbhar", Email = "pkpotter@gmail.com" },
//            new() { Id = 3, Name = "Aryan Nimbalkar", Email = "aryan@gmail.com" },
//            new() { Id = 4, Name = "Keshav Darak", Email = "keshav@gmail.com" },
//            new() { Id = 5, Name = "AB", Email = "ab@gmail.com" },
//            new() { Id = 6, Name = "Random1", Email = "r1@gmail.com" },
//            new() { Id = 7, Name = "Random2", Email = "r2@gmail.com" },
//            new() { Id = 8, Name = "John Doe", Email = "john@gmail.com" },
//            new() { Id = 9, Name = "Jane Smith", Email = "jane@gmail.com" },
//        };

//        private List<AttendanceRecord> attendanceRecords = new();

//        public List<Student> GetStudents() => students;

//        public void RecordAttendance(int studentId, bool isPresent, DateTime date)
//        {
//            var existingRecord = attendanceRecords.FirstOrDefault(ar => ar.StudentId == studentId && ar.Date.Date == date.Date);

//            if (existingRecord != null)
//            {
//                existingRecord.IsPresent = isPresent;
//            }
//            else
//            {
//                attendanceRecords.Add(new AttendanceRecord
//                {
//                    Id = attendanceRecords.Count + 1,
//                    StudentId = studentId,
//                    Date = date,
//                    IsPresent = isPresent
//                });
//            }
//        }

//        public void DeleteAttendanceRecord(int recordId)
//        {
//            var recordToRemove = attendanceRecords.FirstOrDefault(ar => ar.Id == recordId);
//            if (recordToRemove != null)
//            {
//                attendanceRecords.Remove(recordToRemove);
//            }
//        }
//        public List<AttendanceRecord> GetAttendanceRecords()
//        {
//            return attendanceRecords;
//        }
//    }
//}

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

        public void RecordAttendance(int studentId, bool isPresent, DateTime date, string studentName)
        {
            var existingRecord = _context.AttendanceRecords.FirstOrDefault(ar => ar.StudentId == studentId && ar.Date.Date == date.Date);

            if (existingRecord != null)
            {
                existingRecord.IsPresent = isPresent;
                _context.SaveChanges();
            }
            else
            {
                var newRecord = new AttendanceRecord(studentId, date, isPresent, studentName);

            _context.AttendanceRecords.Add(newRecord);
                _context.SaveChanges();
            }
        }

        public void DeleteAttendanceRecord(int recordId)
        {
            var recordToRemove = _context.AttendanceRecords.FirstOrDefault(ar => ar.Id == recordId);
            if (recordToRemove != null)
            {
                _context.AttendanceRecords.Remove(recordToRemove);
                _context.SaveChanges();
            }
        }

        public List<AttendanceRecord> GetAttendanceRecords()
        {
            return _context.AttendanceRecords.ToList();
        }
    }
}
