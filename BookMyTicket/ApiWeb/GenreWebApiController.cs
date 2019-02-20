using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class GenreWebApiController : ApiController
    {

        AdityaEntities4 db;

        public GenreWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetGenre()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Genres.ToList());
        }

        public HttpResponseMessage GetSingleGenre(int id)
        {
            var singleGenre = db.Genres.SingleOrDefault(g=>g.GenreId == id);

            if (singleGenre == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Genre not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleGenre);
        }

        public HttpResponseMessage PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Genres.Add(genre);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Genre created successfully");
        }


        public HttpResponseMessage PutGenre(Genre genre)
        {
            var singleGenre = db.Genres.SingleOrDefault(g=>g.GenreId == genre.GenreId);

            if (singleGenre == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Genre not found to updated");
            }

            if (!ModelState.IsValid)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            return Request.CreateResponse(HttpStatusCode.OK,"Genre updated successfully");
        }


        public HttpResponseMessage DeleteGenre(int id)
        {
            var singleGenre = db.Genres.SingleOrDefault(g => g.GenreId == id);

            if (singleGenre == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Genre not found too delte");
            }


            db.Genres.Remove(singleGenre);
            db.SaveChanges();

            return Request.CreateErrorResponse(HttpStatusCode.OK,"Genre deleted successfully");

        }

    }
}
