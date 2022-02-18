using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{   
    [DataContract]

    [XmlRoot(ElementName = "Reservation")]
    public class Reservation : IReservation
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ReservationId")]
        public int ReservationId { get; set ; }

        [DataMember]
        [XmlAttribute(AttributeName = "UserId")]
        public int UserId { get ; set ; }

        [DataMember]
        [XmlAttribute(AttributeName = "BookId")]
        public int BookId { get ; set ; }


        [XmlIgnore]
        [DataMember]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [XmlIgnore]
        [DataMember]
        public DateTime EndDate { get ; set; }

        [XmlAttribute(AttributeName = "StartDate")]
        public string serializableStartDate { get { return StartDate.ToString("dd/MM/yyyy"); } set {
                StartDate = DateTime.Parse(value);
            } }

        [XmlAttribute(AttributeName = "EndDate")]

        public string serializableEndDate
        {
            get { return StartDate.ToString("dd/MM/yyyy"); }
            set
            {
                this.EndDate = DateTime.Parse(value);
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
