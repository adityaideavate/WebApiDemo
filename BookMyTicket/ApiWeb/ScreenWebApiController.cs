using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookMyTicket.ApiWeb
{
    public class ScreenWebApiController : ApiController
    {
        AdityaEntities4 db;

        public ScreenWebApiController()
        {
            db = new AdityaEntities4();
        }

        public HttpResponseMessage GetScreens()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Screens.ToList());
        }

        public HttpResponseMessage GetSingleScreen(int id)
        {
            var singleScreen = db.Screens.SingleOrDefault(s=>s.ScreenId == id);

            if (singleScreen == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Screen not found with id "+id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,singleScreen);
        }

        public HttpResponseMessage PostScreen(Screen screen)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Screens.Add(screen);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Sceen created successfully");

        }

        public HttpResponseMessage PutScreen(Screen screen)
        {
            var singleScreen = db.Screens.SingleOrDefault(s=>s.ScreenId == screen.ScreenId);

            if (singleScreen == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Screen not found to update");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Please provide correct information");
            }

            db.Screens.Add(screen);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Screen updated successfully");
        }

        public HttpResponseMessage DeleteScreen(int id)
        {
            var singleScsreen = db.Screens.SingleOrDefault(s => s.ScreenId == id);

            if (singleScsreen == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Screen not found to delete");
            }

            db.Screens.Remove(singleScsreen);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK,"Screen delete successfully");
        }

    }   
}
