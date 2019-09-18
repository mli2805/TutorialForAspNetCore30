using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{

    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly GeoCoordsService _geoCoordsService;


        public StopsController(IWorldRepository repository, IMapper mapper,
            ILogger<StopsController> logger, GeoCoordsService geoCoordsService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _geoCoordsService = geoCoordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                if (trip != null)
                    return Ok(_mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get stops {e.Message}");
            }

            return BadRequest("Failed to get stops");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel stopVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = _mapper.Map<Stop>(stopVm);

                    var coors = await _geoCoordsService.GetCoordsAsync(newStop.Name);
                    if (!coors.Success)
                    {
                        _logger.LogError(coors.Message);
                    }
                    else
                    {
                        newStop.Latitude = coors.Latitude;
                        newStop.Longitude = coors.Longitude;

                        _repository.AddStop(tripName, newStop);
                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                                _mapper.Map<StopViewModel>(newStop));
                        }
                    }

                   
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save new stop {e.Message}");
            }

            return BadRequest("Failed to save new stop");
        }
    }
}
