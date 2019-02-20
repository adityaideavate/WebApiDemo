using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class CustomerWebApiController : ApiController
    {
        AdityaEntities4 db;

        public CustomerWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetCustomers()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Customers.ToList());
        }

        public HttpResponseMessage GetSingleCustomer(int id)
        {
            var singleCustomer = db.Customers.SingleOrDefault(c=>c.CustomerId == id);

            if (singleCustomer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Customer not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleCustomer);
        }

        public HttpResponseMessage PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please provide correct information");
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,customer);
        }

        public HttpResponseMessage PutCustomer(Customer customer)
        {
            var singleCustomer = db.Customers.SingleOrDefault(c=>c.CustomerId == customer.CustomerId);

            if (singleCustomer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"No customer found to update");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            singleCustomer.CustomerCity = customer.CustomerCity;
            singleCustomer.CustomerContactNo = customer.CustomerContactNo;
            singleCustomer.CustomerName= customer.CustomerName;

            db.SaveChanges();

            return Request.CreateErrorResponse(HttpStatusCode.OK,"Customer information updated successfully");

        }

        public HttpResponseMessage DeleteCustomer(int id)
        {
            var singleCustomer = db.Customers.SingleOrDefault(c=>c.CustomerId == id);

            if (singleCustomer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Customer not found to delete");
            }

            db.Customers.Remove(singleCustomer);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Product successfully deleted");

        }
    }
}
