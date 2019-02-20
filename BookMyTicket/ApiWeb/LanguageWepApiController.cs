using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class LanguageWepApiController : ApiController
    {
        AdityaEntities4 db;

        public LanguageWepApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetLanguage()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Languages.ToList());
        }

        public HttpResponseMessage GetSingleLanguage(int id)
        {
            var singleLanguage = db.Languages.SingleOrDefault(l=>l.LanguageId == id);

            if (singleLanguage == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Language not found for id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleLanguage);

        }
    }
}
