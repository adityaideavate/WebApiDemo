using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Serialization;

namespace BookMyTicket.ApiWeb
{
    public class ShowWebApiController : ApiController
    {
        AdityaEntities4 db;

        public ShowWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetShows()
        {
  
            return Request.CreateResponse(HttpStatusCode.OK, db.Shows.ToList());
        }

        public HttpResponseMessage GetSingleShow(int id)
        {
            var singleShow = db.Shows.SingleOrDefault(s=>s.ShowId == id);

            if (singleShow == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Show not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleShow);
        }

        public HttpResponseMessage PostShow(Show show)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Shows.Add(show);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Show successfully crated");

        }

        public HttpResponseMessage PutShow(Show show)
        {
            var singleShow = db.Shows.SingleOrDefault(s=>s.ShowId == show.ShowId);

            if (singleShow == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"SHow not found to update");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            singleShow.Movie = show.Movie;
            singleShow.Rate = show.Rate;
            singleShow.ScreenId = show.ScreenId;
            singleShow.Time = show.Time;

            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Show updated successfully");

        }

        public HttpResponseMessage DeleteShow(int id)
        {
            var singleshow = db.Shows.SingleOrDefault(s=>s.ShowId == id);

            if (singleshow == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Show not found to delete with id "+id);
            }

            db.Shows.Remove(singleshow);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Show deleted successfully");
        }   

    }
}
