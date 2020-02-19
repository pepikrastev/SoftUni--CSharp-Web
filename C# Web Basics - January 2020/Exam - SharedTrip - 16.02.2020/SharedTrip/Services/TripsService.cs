using SharedTrip.Models;
using SharedTrip.ViewModels.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Add(TripsAddInputModel tripAddInputModel)
        {
            Trip trip = new Trip()
            {
               StartPoint = tripAddInputModel.StartPoint,
               EndPoint = tripAddInputModel.EndPoint,
               DepartureTime = tripAddInputModel.DepartureTime,
               ImagePath = tripAddInputModel.imagePath,
               Seats = tripAddInputModel.Seats,
               Description = tripAddInputModel.Description
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();

            return trip.Id;
        }

        public IEnumerable<AllTripsViewModel> GetAll()
        {
            var allTrips = this.db.Trips.Select(x => new AllTripsViewModel
            {

                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = x.Seats,

            }).
            ToArray();
            return allTrips;
        }

        public GetTripViewModel GetTrip(string id)
        {
            return this.db.Trips.Where(x => x.Id == id).Select(t => new GetTripViewModel
            {
                Id = t.Id,
                DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Description = t.Description,
                EndPoint = t.EndPoint,
                ImagePath = t.ImagePath,
                Seats = t.Seats,
                StartPoint = t.StartPoint
            })
            .FirstOrDefault();
        }

        public bool AddUserToTrip(string tripId, string userId)
        {
            var targetTrip = this.db.Trips.FirstOrDefault(x => x.Id == tripId);
            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = userId
            };
            
            if (!this.db.UserTrips.Any(x => x.TripId == userTrip.TripId && x.UserId == userTrip.UserId) && targetTrip.Seats > 0)
            {
                targetTrip.Seats -= 1;
                targetTrip.UserTrips.Add(userTrip);
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
