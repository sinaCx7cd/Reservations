using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Reservations.Services;
using Reservations.ViewModels;

namespace Reservations.Controllers
{
    [Route("reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("get")]
        public IActionResult GetReservations([FromQuery] DateTime date)
        {
            return Ok(_reservationService.GetReservation(date));
        }


        [HttpPost("add")]
        public IActionResult Add([FromBody] AddReservationVm addModel)
        {
            var result = _reservationService.AddReservation(addModel);
            return result ? (IActionResult)Ok() : BadRequest();
        }
    }
}
