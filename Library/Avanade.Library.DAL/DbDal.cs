using Avanade.Library.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Avanade.Library.DAL
{
    public class DbDal : IDal
    {

        SqlConnection sqlCon = null;
        String SqlconString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public IUser GetUser(string Username, string Password)
        {
            IUser user = new User();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Select_User_Login", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Username", SqlDbType.VarChar).Value = Username;
                    sql_cmnd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = Password;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            { 
                                while (sqlDataReader.Read())
                                {
                                    user.UserId = sqlDataReader.GetInt32(0);
                                    user.Username = sqlDataReader.GetString(1);
                                    user.Password = sqlDataReader.GetString(2);
                                    user.Role = sqlDataReader.GetString(3);
                                }
                            }
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return user ?? new User();
        }

        public bool AddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity)
        {

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("BookInsert_Operation", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = Title;
                    sql_cmnd.Parameters.AddWithValue("@AuthorName", SqlDbType.VarChar).Value = AuthorName;
                    sql_cmnd.Parameters.AddWithValue("@AuthorSurname", SqlDbType.VarChar).Value = AuthorSurName;
                    sql_cmnd.Parameters.AddWithValue("@PublishingHouse", SqlDbType.VarChar).Value = PublishingHouse;
                    sql_cmnd.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = Quantity;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
                
            }
            catch(Exception)
            {
                return false;
            }

            return true;

        }

        public int AddReservation(int userId, int bookId, DateTime startDate, DateTime endDate)
        {
            IReservation reservation = new Reservation();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("ReservationInsert_Operation", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = bookId;
                    sql_cmnd.Parameters.AddWithValue("@StartDate", SqlDbType.Date).Value = startDate;
                    sql_cmnd.Parameters.AddWithValue("@EndDate", SqlDbType.Date).Value = endDate.AddDays(30);
                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    reservation.ReservationId = sqlDataReader.GetInt32(0);
                                }
                            }
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw;
            }

            return reservation.ReservationId;

        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Delete_Book", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = BookId;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteReservation(int BookId)
        {
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Delete_Reservation", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = BookId;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public int GetBookId(string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            IBook book = new Book();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Book", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = Title;
                    sql_cmnd.Parameters.AddWithValue("@AuthorName", SqlDbType.VarChar).Value = AuthorName;
                    sql_cmnd.Parameters.AddWithValue("@AuthorSurname", SqlDbType.VarChar).Value = AuthorSurName;
                    sql_cmnd.Parameters.AddWithValue("@PublishingHouse", SqlDbType.VarChar).Value = PublishingHouse;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    book.BookId = sqlDataReader.GetInt32(0);
                                }
                            }
                        }
                    }

                    sqlCon.Close();

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return book.BookId;
        }

        public IBook GetBookById(int BookId)
        {
            IBook book = new Book();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Book_ById", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = BookId;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    book.BookId = sqlDataReader.GetInt32(0);
                                    book.Title = sqlDataReader.GetString(1);
                                    book.AuthorName = sqlDataReader.GetString(2);
                                    book.AuthorSurname = sqlDataReader.GetString(3);
                                    book.PublishingHouse = sqlDataReader.GetString(4);
                                    book.Quantity = sqlDataReader.GetInt32(5);
                                }
                            }
                        }
                    }

                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return book;
        }

        public List<IBooksAvailables> GetBooksAvailables(string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {

            List<IBooksAvailables> bookAvailableList = new List<IBooksAvailables>();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Books_Availables", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = Title;
                    sql_cmnd.Parameters.AddWithValue("@AuthorName", SqlDbType.VarChar).Value = AuthorName;
                    sql_cmnd.Parameters.AddWithValue("@AuthorSurname", SqlDbType.VarChar).Value = AuthorSurName;
                    sql_cmnd.Parameters.AddWithValue("@PublishingHouse", SqlDbType.VarChar).Value = PublishingHouse;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    IBooksAvailables bookStatus = new BooksAvailables();
                                    bookStatus.BookId = sqlDataReader.GetInt32(0);
                                    bookStatus.Title = sqlDataReader.GetString(1);
                                    bookStatus.AuthorName = sqlDataReader.GetString(2);
                                    bookStatus.AuthorSurName = sqlDataReader.GetString(3);
                                    bookStatus.PublishingHouse = sqlDataReader.GetString(4);
                                    bookStatus.Quantity = sqlDataReader.GetInt32(5);
                                    bookStatus.DateAvailable = sqlDataReader.GetDateTime(6);
                                    bookAvailableList.Add(bookStatus);
                                }
                            }
                        }
                    }
                    sqlCon.Close();
                   
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return bookAvailableList;

        }

        public int GetDuplicateBook(string title, string authorName, string authorSurName, string publishingHouse)
        {
            IBook duplicateBookId = new Book();

            try
            {

                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Duplicate_Book", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = title;
                    sql_cmnd.Parameters.AddWithValue("@AuthorName", SqlDbType.VarChar).Value = authorName;
                    sql_cmnd.Parameters.AddWithValue("@AuthorSurName", SqlDbType.VarChar).Value = authorSurName;
                    sql_cmnd.Parameters.AddWithValue("@PublishingHouse", SqlDbType.VarChar).Value = publishingHouse;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    duplicateBookId.BookId = sqlDataReader.GetInt32(0);
                                }
                            }
                        }
                    }
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return duplicateBookId.BookId;
        }

        public List<IReservationsHistory> GetHistoryReservations(int BookId, int UserId, int ReservationId)
        {
            List<IReservationsHistory> reservationStatusListHistory = new List<IReservationsHistory>();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_History_Reservations", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = BookId;
                    sql_cmnd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = UserId;
                    sql_cmnd.Parameters.AddWithValue("@ReservationId", SqlDbType.Int).Value = ReservationId;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    IReservationsHistory historyReservationsStatus = new ReservationsHistory();
                                    historyReservationsStatus.UserId = sqlDataReader.GetInt32(0);
                                    historyReservationsStatus.BookId = sqlDataReader.GetInt32(1);
                                    historyReservationsStatus.Title = sqlDataReader.GetString(2);
                                    historyReservationsStatus.Username = sqlDataReader.GetString(3);
                                    historyReservationsStatus.StartDate = sqlDataReader.GetDateTime(4);
                                    historyReservationsStatus.EndDate = sqlDataReader.GetDateTime(5);
                                    historyReservationsStatus.ReservationId = sqlDataReader.GetInt32(6);
                                    historyReservationsStatus.Quantity = sqlDataReader.GetInt32(7);
                                    historyReservationsStatus.Reserved = sqlDataReader.GetInt32(8) == 1;
                                    reservationStatusListHistory.Add(historyReservationsStatus);
                                }
                            }
                        }
                    }
                    sqlCon.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return reservationStatusListHistory;
        }

        public List<IReservationsHistory> GetReservationByBookId(int bookId)
        {
            List<IReservationsHistory> reservationStatusList = new List<IReservationsHistory>();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Reservation_ByBookId", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = bookId;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    IReservationsHistory reservationStatus = new ReservationsHistory();
                                    reservationStatus.UserId = sqlDataReader.GetInt32(0);
                                    reservationStatus.BookId = sqlDataReader.GetInt32(1);
                                    reservationStatus.Title = sqlDataReader.GetString(2);
                                    reservationStatus.Username = sqlDataReader.GetString(3);
                                    reservationStatus.StartDate = sqlDataReader.GetDateTime(4);
                                    reservationStatus.EndDate = sqlDataReader.GetDateTime(5);
                                    reservationStatus.ReservationId = sqlDataReader.GetInt32(6);
                                    reservationStatus.Quantity = sqlDataReader.GetInt32(7);
                                    reservationStatus.Reserved = sqlDataReader.GetInt32(8) == 1;
                                    reservationStatusList.Add(reservationStatus);
                                }
                            }
                        }
                    }
                    sqlCon.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return reservationStatusList;

        }

        public List<IReservationsHistory> GetReservationByReservationId(int ReservationId)
        {
            List<IReservationsHistory> reservationStatusList = new List<IReservationsHistory>();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Reservation_ReservationId", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ReservationId", SqlDbType.Int).Value = ReservationId;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    IReservationsHistory reservationStatus = new ReservationsHistory();
                                    reservationStatus.UserId = sqlDataReader.GetInt32(0);
                                    reservationStatus.BookId = sqlDataReader.GetInt32(1);
                                    reservationStatus.Title = sqlDataReader.GetString(2);
                                    reservationStatus.Username = sqlDataReader.GetString(3);
                                    reservationStatus.StartDate = sqlDataReader.GetDateTime(4);
                                    reservationStatus.EndDate = sqlDataReader.GetDateTime(5);
                                    reservationStatus.ReservationId = sqlDataReader.GetInt32(6);
                                    reservationStatus.Quantity = sqlDataReader.GetInt32(7);
                                    reservationStatus.Reserved = sqlDataReader.GetInt32(8) == 1;
                                    reservationStatusList.Add(reservationStatus);
                                }
                            }
                        }
                    }
                    sqlCon.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return reservationStatusList;
        }

        public int GetReservationDays(int reservationId)
        {
            IReservation quantityOfDays = new Reservation();
            
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Get_Reservation_Days", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ReservationId", SqlDbType.Int).Value = reservationId;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    quantityOfDays.ReservationId = sqlDataReader.GetInt32(0);
                                }
                            }
                        }
                    }

                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return quantityOfDays.ReservationId;
        }


        public IBook UpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            IBook bookUpdated = new Book();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Update_Book", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = BookId;
                    sql_cmnd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = Title;
                    sql_cmnd.Parameters.AddWithValue("@AuthorName", SqlDbType.VarChar).Value = AuthorName;
                    sql_cmnd.Parameters.AddWithValue("@AuthorSurName", SqlDbType.VarChar).Value = AuthorSurName;
                    sql_cmnd.Parameters.AddWithValue("@PublishingHouse", SqlDbType.VarChar).Value = PublishingHouse;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    bookUpdated.Title = sqlDataReader.GetString(0);
                                    bookUpdated.AuthorName = sqlDataReader.GetString(1);
                                    bookUpdated.AuthorSurname = sqlDataReader.GetString(2);
                                    bookUpdated.PublishingHouse = sqlDataReader.GetString(3);
                                }
                            }
                        }
                    }

                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw;
            }

            return bookUpdated;

        }

        public string UpdateEndDateReservation(int ReservationId)
        {

            string updateEndDate;

            try
            {
                
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Update_EndDate_Reservation", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ReservationId", SqlDbType.Int).Value = ReservationId;
                    updateEndDate = ((DateTime)sql_cmnd.ExecuteScalar()).ToString("dd/MM/yyyy");
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return updateEndDate;
        }

        public int UpdateQuantityOfBook(int BookId, int Quantity)
        {
            IBook book = new Book();

            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Update_Book_Quantity", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BookId", SqlDbType.Int).Value = BookId;
                    sql_cmnd.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = Quantity;

                    using (SqlDataReader sqlDataReader = sql_cmnd.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                while (sqlDataReader.Read())
                                {
                                    book.Quantity = sqlDataReader.GetInt32(0);
                                }
                            }
                        }
                    }

                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Can not open connection ! ErrorCode: {ex.ErrorCode} Error: {ex.Message}");
                throw ex;
            }

            return book.Quantity;
        }

   
    }
}
