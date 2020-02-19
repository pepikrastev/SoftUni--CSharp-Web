using SharedTrip.Models;
using SharedTrip.ViewModels.Trip;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        string Add(TripsAddInputModel tripAddInputModel);

        IEnumerable<AllTripsViewModel> GetAll();

        GetTripViewModel GetTrip(string id);

        bool AddUserToTrip(string tripId, string userId);
    }
}
