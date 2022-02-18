using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{

    [XmlRoot("Reservations")]
    public class Reservations : IReservations
    {
       
        [XmlElement("Reservation")]
        public List<Reservation> listOf { get; set; }

        public Reservations(List<Reservation> reservationList) 
        {
            listOf = reservationList;
        }

        public Reservations()
        {

        }
    }
}
