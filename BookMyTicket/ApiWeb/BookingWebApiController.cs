using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class BookingWebApiController : ApiController
    {
        AdityaEntities4 db;

        public BookingWebApiController()
        {
            db = new AdityaEntities4();
        }

        //curd  create update   delete

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, db.Bookings.ToList());
        }


        public HttpResponseMessage GetSingleBooking(int id)
        {
            var singleBooking = db.Bookings.SingleOrDefault(b => b.BookingId == id);

            if (singleBooking == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Booking not found with Id " + id);
            }

            return Request.CreateResponse(HttpStatusCode.OK, singleBooking);
        }

        public HttpResponseMessage PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please provide correct information to create booking");
            }

            db.Bookings.Add(booking);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Booking created successfully");
        }

        public HttpResponseMessage DeleteBooking(int id)
        {
            var singleBooking = db.Bookings.SingleOrDefault(b=>b.BookingId == id);

            if (singleBooking == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Booking not found to delete");
            }

            db.Bookings.Remove(singleBooking);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Booking deleted successfully");
        }

    }
}
