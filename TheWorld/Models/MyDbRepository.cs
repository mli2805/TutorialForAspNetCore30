using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class MyDbRepository : IMyDbRepository
    {
        private readonly MyDbContext _context;
        private readonly ILogger<MyDbRepository> _logger;

        public MyDbRepository(MyDbContext context, ILogger<MyDbRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all trips from the database");
            return _context.Trips.Include(t=>t.Stops).ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t=>t.Stops)
                .FirstOrDefault(t => t.Name == tripName);
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetTripByName(tripName);
            if (trip != null)
            {
                _context.Stops.Add(newStop);
                trip.Stops.Add(newStop);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
