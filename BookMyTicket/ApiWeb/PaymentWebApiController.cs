using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class PaymentWebApiController : ApiController
    {
        AdityaEntities4 db;

        public PaymentWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetPayments()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Payments.ToList());
        }

        public HttpResponseMessage GetSinglePayment(int id)
        {
            var singlePayment = db.Payments.SingleOrDefault(p=>p.PaymentId == id);

            if (singlePayment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Payment not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singlePayment);
        }

    }
}
