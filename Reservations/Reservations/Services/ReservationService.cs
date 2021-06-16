using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Reservations.ViewModels;

namespace Reservations.Services
{
    public interface IReservationService
    {
        public bool AddReservation(AddReservationVm addModel);
        public List<int> GetReservation(DateTime date);
    }

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddReservation(AddReservationVm addModel)
        {
            if (addModel.Hours == null || !addModel.Hours.Any())
            {
                return false;
            }

            if (!IsAddingPossible(addModel))
            {
                return false;
            }

            var reservations = new List<Reservation>();
            foreach (var hour in addModel.Hours)
            {
                if (hour < 0 || hour > 23)
                {
                    return false;
                }

                var date = new DateTime(addModel.Date.Year, addModel.Date.Month, addModel.Date.Day, hour, 0, 0);
                var reservation = new Reservation
                {
                    Start = date
                };
                reservations.Add(reservation);
            }

            _dbContext.Reservations.AddRange(reservations);
            _dbContext.SaveChanges();

            return true;
        }

        public List<int> GetReservation(DateTime date)
        {
            return _dbContext.Reservations
                .Where(r => r.Start.Date == date.Date)
                .Select(r => r.Start.Hour)
                .ToList();
        }

        private bool IsAddingPossible(AddReservationVm addModel)
        {
            var reservations = _dbContext.Reservations
                .Where(r => r.Start.Date == addModel.Date.Date)
                .ToList();

            foreach (var hour in addModel.Hours)
            {
                if (reservations.Any(r => r.Start.Hour == hour))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
