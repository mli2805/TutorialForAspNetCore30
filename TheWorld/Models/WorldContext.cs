﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Models
{
    public class WorldContext : DbContext
    {
        private readonly IConfigurationRoot _config;
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public WorldContext(DbContextOptions options, IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }



    }

}