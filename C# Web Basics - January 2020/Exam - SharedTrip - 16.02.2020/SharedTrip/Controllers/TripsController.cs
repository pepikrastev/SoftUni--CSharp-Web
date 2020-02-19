using SharedTrip.Services;
using SharedTrip.ViewModels.Trip;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripsAddInputModel inputModel)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (inputModel.Seats < 2 || inputModel.Seats > 6)
            {
                return this.View();
            }

            if (string.IsNullOrWhiteSpace(inputModel.StartPoint) || string.IsNullOrWhiteSpace(inputModel.EndPoint) || inputModel.DepartureTime == null)
            {
                return this.Redirect("/Trips/Add");
            }

            if (inputModel.Description.Length > 80)
            {
                return this.View();
            }


            this.tripsService.Add(inputModel);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetTrip(tripId);
            return this.View(viewModel, "Details");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
           
            return this.View(this.tripsService.GetAll());
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.Request.SessionData["UserId"];
            var isAdded = this.tripsService.AddUserToTrip(tripId, userId);

            if (isAdded)
            {
                return this.Redirect("/Trips/All");
            }

            return this.Redirect($"/Trips/Details?tripId={tripId}");
        }
    }
}
