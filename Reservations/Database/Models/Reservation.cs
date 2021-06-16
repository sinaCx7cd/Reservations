using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    [Index(nameof(Start))]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime Start { get; set; }
    }
}

