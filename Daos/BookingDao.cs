using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class BookingDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();

        public void Add(Booking booking)
        {
            myDb.bookings.Add(booking);
            myDb.SaveChanges();
        }

        public Booking CheckBooking(int idRoom)
        {
            return myDb.bookings.FirstOrDefault(x => x.idRoom == idRoom  && x.status != 2);
        }

        public List<Booking> CheckBook(int idRoom)
        {
            return myDb.bookings.Where(x => x.idRoom == idRoom && x.status == 0 || x.status == 1).ToList();
        }

        public List<Booking> GetBookingsByIdUser(int idUser)
        {
            return myDb.bookings.Where(x => x.idUser == idUser).ToList();
        }

        public Booking GetBookingById(int id)
        {
            return myDb.bookings.FirstOrDefault(x => x.idBooking == id);
        }

        public List<Booking> getAll()
        {
            return myDb.bookings.OrderByDescending(x => x.createdDate).ToList();
        }

        public List<BookingService> getBS(int id)
        {
            return myDb.BookingServices.Where(x => x.idBooking == id).ToList();
        }

        public void update(Booking booking)
        {
            var obj = myDb.bookings.FirstOrDefault(x => x.idBooking == booking.idBooking);
            obj.status = booking.status;
            obj.isPayment = booking.isPayment;
            myDb.SaveChanges();
        }

        public int statictis(int month)
        {
            string SQL = "Select SUM(totalMoney) FROM Bookings WHERE MONTH(createdDate) = '" + month + "'  AND isPayment = 1 ";
            int? result = myDb.Database.SqlQuery<int?>(SQL).FirstOrDefault();
            if(result != null)
            {
                return (int)result;
            }
            else
            {
                return 0;
            }
            
        }

        public bool checkBooked(int userId, int roomId)
        {
            var obj = myDb.bookings.Where(x => x.idUser == userId && x.idRoom == roomId && x.status == 3).ToList();
            return obj.Count > 0;
        }
    }
}