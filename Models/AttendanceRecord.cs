using AttendanceSystem.Pages;

namespace AttendanceSystem.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string StudentName { get; set; }

        public AttendanceRecord(int studentId, DateTime date, bool isPresent, string studentName)
        {
            //Id = id;
            StudentId = studentId;
            Date = date;
            IsPresent = isPresent;
            StudentName = studentName;
        }
    }
}