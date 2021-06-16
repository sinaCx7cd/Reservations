using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.ViewModels
{
    public class AddReservationVm
    {
        public DateTime Date { get; set; }
        public List<int> Hours { get; set; }
    }
}
