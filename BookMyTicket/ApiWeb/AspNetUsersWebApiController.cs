using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class AspNetUsersWebApiController : ApiController
    {
        AdityaEntities4 db;

        public AspNetUsersWebApiController()
        {
            db = new AdityaEntities4();
        }


        public HttpResponseMessage GetAllAspNetUsers()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.AspNetUsers.ToList());
        }

        public HttpResponseMessage GetSingleApsNetUser(string id)
        {
            var singleAspNetUser = db.AspNetUsers.SingleOrDefault(u => u.Id == id);

            if (singleAspNetUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"User not found with given id");
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleAspNetUser);
        }

    }
}
