﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class User
    {
        public User()
        {
            this.UserTrips = new HashSet<UserTrip>();
        }

        public string Id { get; set; }

        //[MaxLength(20), Required]
        public string Username { get; set; }

        //[Required]
        public string Email { get; set; }

        //[MaxLength(20), Required]
        public string Password { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; }
    }
}
