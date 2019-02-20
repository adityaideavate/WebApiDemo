using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class TheatreWebApiController : ApiController
    {
        AdityaEntities4 db;

        public TheatreWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetTheatre()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Theatres.ToList());
        }

        public HttpResponseMessage GetSingleTheatre(int id)
        {
            var singleTheatre = db.Theatres.SingleOrDefault(t=>t.TheatreId == id);

            if (singleTheatre == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Theatre not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleTheatre);
        }

        public HttpResponseMessage PostTheatre(Theatre theatre)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Theatres.Add(theatre);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Theatre created successfully");
        }

        public HttpResponseMessage PutTheatre(Theatre theatre)
        {
            var singleTheate = db.Theatres.SingleOrDefault(t=>t.TheatreId == theatre.TheatreId);

            if (singleTheate == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Theate not found for updation");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Theatres.Add(singleTheate);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Theatre updated successfully");
        }

        public HttpResponseMessage DeleteTheatre(int id)
        {
            var singleTheatre = db.Theatres.SingleOrDefault(t=>t.TheatreId == id);

            if (singleTheatre == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Theatre not found for deletion");
            }

            db.Theatres.Remove(singleTheatre);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Theatre deleted successfuly");

        }

    }
}
