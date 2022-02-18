using System;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{
    [XmlRoot(ElementName = "Reservation")]
    public class Reservation : IReservation
    {
        [XmlAttribute(AttributeName = "ReservationId")]
        public int ReservationId { get; set ; }
        [XmlAttribute(AttributeName = "UserId")]
        public int UserId { get ; set ; }
        [XmlAttribute(AttributeName = "BookId")]
        public int BookId { get ; set ; }
        [XmlIgnore]
        public DateTime StartDate { get; set; }
        [XmlIgnore]
        public DateTime EndDate { get ; set; }

        [XmlAttribute(AttributeName = "StartDate")]
        public string serializableStartDate { get { return StartDate.ToString("dd/MM/yyyy"); } set {
                //StartDate = DateTime.Parse(value);
            } }
        [XmlAttribute(AttributeName = "EndDate")]
        public string serializableEndDate
        {
            get { return StartDate.ToString("dd/MM/yyyy"); }
            set
            {
                //this.EndDate = DateTime.Parse(value);
            }
        }

        public Reservation(int reservationId, int userId, int bookId, DateTime startDate, DateTime endDate)
        {
            ReservationId = reservationId;
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            EndDate = endDate;
        }
        public Reservation()
        { 
        }
    }



}
