using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class MovieWebApiController : ApiController
    {
        AdityaEntities4 db;

        public MovieWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetMovies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, db.Movies.ToList());
        }

        //public string GetMovies()
        //{
        //    return ("hello" );
        //}



        public HttpResponseMessage GetSingleMovie(int id)
        {
            var singleMovie = db.Movies.SingleOrDefault(m=>m.MovieId == id);

            if (singleMovie == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Movie not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleMovie);
        }

        public HttpResponseMessage PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provied correct information");
            }

            db.Movies.Add(movie);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Movie created successfully");
        }

        public HttpResponseMessage PutMovie(Movie movie)
        {
            var singleMovie = db.Movies.SingleOrDefault(m=>m.MovieId == movie.MovieId);

            if (singleMovie == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Movie not found ");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }


            singleMovie.MovieName = movie.MovieName;
            singleMovie.DateRelease = movie.DateRelease;
            singleMovie.DateEnd = movie.DateEnd;
            singleMovie.LanguageId = movie.LanguageId;
            singleMovie.GenreId = movie.GenreId;
            singleMovie.Description = movie.Description;
 

            //singleMovie.MoviePic = movie.MoviePic;
            //singleMovie.MovieWallpaper = movie.MovieWallpaper; 

            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Movie updated successfully");
        }


        public HttpResponseMessage DeleteMovie(int id)
        {
            var singleMovie = db.Movies.SingleOrDefault(m=>m.MovieId == id);

            if (singleMovie == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Movie not found to delete with id "+id);
            }

            db.Movies.Remove(singleMovie);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Movie deleted successfully");
        }

    }
}
