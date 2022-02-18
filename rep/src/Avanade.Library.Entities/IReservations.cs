using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avanade.Library.Entities
{
    public interface IReservations
    {
        List<Reservation> listOf { get; set; }
    }
}    