using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Avanade.Library.Entities;
using System.Text.Json;
using Avanade.Library.DAL;
using System.Text;

namespace Avanade.Library.Proxy
{
    public static class ReservationProxy
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string HttpBasePath = "http://localhost:85/api/reservation";
        private static readonly string MimeType = "application/json";

        public static async Task<Response<ReservationsHistory>> DeleteReservation(int ReservationId)
        {
            HttpResponseMessage streamTask = await client.DeleteAsync(HttpBasePath + "/DeleteReservation?ReservationId=" + ReservationId);
            var reservations = await JsonSerializer.DeserializeAsync<Response<ReservationsHistory>>(await streamTask.Content.ReadAsStreamAsync());
            return reservations;
        }

        public static async Task<Response<ReservationsHistory>> AddReservation(Reservation content)
        {
            string json = JsonSerializer.Serialize<IReservation>(content);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, MimeType);
            HttpResponseMessage streamTask = await client.PostAsync(HttpBasePath + "/AddReservation", stringContent);
            var response = await JsonSerializer.DeserializeAsync<Response<ReservationsHistory>>(await streamTask.Content.ReadAsStreamAsync());
            return response;
        }

        public static async Task<List<ReservationsHistory>> GetReservationHistory()
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetReservationHistory");
            var response = await JsonSerializer.DeserializeAsync<List<ReservationsHistory>>(await streamTask.Content.ReadAsStreamAsync());
            return response;
        }

        public static async Task<List<ReservationsHistory>> GetReservationHistoryByBookId(int BookId)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetReservationHistoryByBookId?BookId=" + BookId);
            var response = await JsonSerializer.DeserializeAsync<List<ReservationsHistory>>(await streamTask.Content.ReadAsStreamAsync());
            return response;
        }

        public static async Task<List<ReservationsHistory>> GetReservationHistoryByUserId(int UserId)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetReservationHistoryByUserId?UserId=" + UserId);
            var response = await JsonSerializer.DeserializeAsync<List<ReservationsHistory>>(await streamTask.Content.ReadAsStreamAsync());
            return response;
        }

        public static async Task<List<ReservationsHistory>> GetReservationHistoryByReservationId(int ReservationId)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetReservationHistoryByReservationId?ReservationId=" + ReservationId);
            var response = await JsonSerializer.DeserializeAsync<List<ReservationsHistory>>(await streamTask.Content.ReadAsStreamAsync());
            return response;
        }


    }
}
