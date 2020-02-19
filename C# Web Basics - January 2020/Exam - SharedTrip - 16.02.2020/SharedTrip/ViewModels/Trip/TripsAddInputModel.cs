using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trip
{
   public class TripsAddInputModel
    {

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }

        public string imagePath { get; set; }

        public string Description { get; set; }

    }
}
