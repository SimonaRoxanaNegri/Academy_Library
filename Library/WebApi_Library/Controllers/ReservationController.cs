using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;
using System.Collections.Generic;
using System.Web.Http;


namespace WebApi_Library.Controllers
{
    
    public class ReservationController : ApiController
    {

        readonly IBusinessLogics logic = new BusinessLogics(new DbDal());

        [HttpGet]
        public List<IReservationsHistory> GetReservationHistory()
        {
            return logic.RequestReservationHistory(0, 0, 0);
        }

        [HttpGet]
        public List<IReservationsHistory> GetReservationHistoryByBookId([FromUri] int BookId)
        {
            return logic.RequestReservationHistory(BookId, 0,0);
        }

        [HttpGet]
        public List<IReservationsHistory> GetReservationHistoryByUserId([FromUri] int UserId)
        {
            return logic.RequestReservationHistory(0, UserId,0);
        }

        [HttpGet]
        public List<IReservationsHistory> GetReservationHistoryByReservationId([FromUri] int ReservationId)
        {
            return logic.RequestReservationHistory(0, 0, ReservationId);
        }

        [HttpPost]
        public IResponse<IReservationsHistory> AddReservation([FromBody] Reservation reservation)
        {
            return logic.RequestReservation(reservation);
        }

        [HttpDelete]
        public IResponse<IReservationsHistory> DeleteReservation([FromUri] int ReservationId)
        {
            return logic.RequestCloseReservation(ReservationId);
        }

    }
}