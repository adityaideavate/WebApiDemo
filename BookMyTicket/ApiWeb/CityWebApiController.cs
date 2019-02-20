using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class CityWebApiController : ApiController
    {
        AdityaEntities4 db;

        public CityWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage getCities()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Cities.ToList());

        }

        public HttpResponseMessage getSinglCity(int id)
        {
            var singleCity = db.Cities.SingleOrDefault(c=>c.CityId == id);

            if (singleCity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"City not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleCity);
            
        }
        public HttpResponseMessage PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Cities.Add(city);
            db.SaveChanges();                 

            return Request.CreateResponse(HttpStatusCode.OK,"City added successfully");

        }

        public HttpResponseMessage PutCity(City city)
        {
            var singleCity = db.Cities.SingleOrDefault(c=>c.CityId == city.CityId);

            if (singleCity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"City not found to update ");
            }

            if(!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            singleCity.CityName = city.CityName;
            singleCity.Theatres = city.Theatres;

            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "City information updated successfully");

        }
        public HttpResponseMessage DeleteCity(int id)
        {
            var singleCity = db.Cities.SingleOrDefault(c=>c.CityId == id);

            if (singleCity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"City not found to delete with id "+id);
            }

            db.Cities.Remove(singleCity);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"City Deleted successfully");

        }


    }
}
