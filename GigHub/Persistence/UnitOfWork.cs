using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    // Why Repository?
    // 1) hence maintainability - one place to retrieve/update the data;
    // 2) hence reusability - no need to write same data access code in different places;
    // 3) following the single responsibility principle (SRP), 
    // by making sure that one place is responsible for retrieving or updating the DB.

    // Why Unit of Work?
    // 1) aggregates all repositories initiation;
    // 2) separates (decouples) application from DbContext and repositories;
    // 3) takes care of saving all changes from multiple repositories in one place (Complete()).

    /// <summary>
    /// The "Unit of Work"
    /// </summary>
    /// <remarks>
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type.
    /// </remarks>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; }
        public IAttendanceRepository Attendances { get; }
        public IFollowingRepository Followings { get; }
        public IGenreRepository Genres { get; }
        public IApplicationUserRepository Users { get; }
        public INotificationRepository Notifications { get; }
        public IUserNotificationRepository UserNotifications { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Genres = new GenreRepository(context);
            Users = new ApplicationUserRepository(context);
            Notifications = new NotificationRepository(context);
            UserNotifications = new UserNotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}