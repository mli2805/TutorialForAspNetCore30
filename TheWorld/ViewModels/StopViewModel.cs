﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class StopViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Required]
        public int  Order { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

    }
}
