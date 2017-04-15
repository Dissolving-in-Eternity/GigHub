using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
    }
}