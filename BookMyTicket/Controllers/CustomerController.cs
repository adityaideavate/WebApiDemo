using BookMyTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookMyTicket.Controllers
{
    public class CustomerController : Controller
    {


        public List<Customer> getCustomer()
        {
            List<Customer> customerList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50301/api/");
                //HTTP GET
                var responseTask = client.GetAsync("CustomerWebApi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Customer>>();
                    readTask.Wait();

                    customerList = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    customerList = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

                return customerList;
            }
        }



        AdityaEntities4 db = new AdityaEntities4();
        // GET: Customer
        public ActionResult Index()
        {
          //  return View(db.Customers.ToList());

            //getCustomer

            return View(getCustomer());
        }


        public ActionResult CustomerForm(string id)
        {
         //   var aspnetuserindb = db.AspNetUsers.SingleOrDefault(temp=>temp.Id == id);

            Customer anu = new Customer() {
               Id=id
            };

            CustomerViewForm cvf = new CustomerViewForm()
            {
                Customer=anu
            };


            return View(cvf);
        }

        public ActionResult CreateCustomer(Customer Customer)
        {
            if (Customer.CustomerId == 0)
            {
                //db.Customers.Add(Customer);
                //db.SaveChanges();



                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50301/api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Customer>("CustomerWebApi", Customer);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Show");
                    }
                    else
                    {
                        return Content("error occured please contact administrator");
                    }
                }




            }


            return RedirectToAction("Index","Show");
        }




    }
}