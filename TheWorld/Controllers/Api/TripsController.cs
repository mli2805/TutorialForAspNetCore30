using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private readonly IMyDbRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TripsController> _logger;

        public TripsController(IMyDbRepository repository, IMapper mapper, ILogger<TripsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var allTrips = _repository.GetAllTrips();

                return Ok(_mapper.Map<IEnumerable<TripViewModel>>(allTrips));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get All Trips: {e.Message}");
                return BadRequest($"TripsController Get: {e.Message}");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel tripVm)
        {
            if (ModelState.IsValid)
            {
                var newTrip = _mapper.Map<Trip>(tripVm);
                _repository.AddTrip(newTrip);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{tripVm.Name}", _mapper.Map<TripViewModel>(newTrip));
                }
            }

            return BadRequest("Failed to save");
        }
    }
}
