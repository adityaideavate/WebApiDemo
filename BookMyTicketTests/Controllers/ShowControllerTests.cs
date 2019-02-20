using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookMyTicket.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyTicket.ViewModel;
using System.Web.Script.Serialization;
using System.Web.Mvc;

namespace BookMyTicket.Controllers.Tests
{
    [TestClass()]
    public class ShowControllerTests
    {
     
        [TestMethod()]
        public void BookingAvailablityTest()
        {
            // Arrange              
            DateTime dateofbooking = Convert.ToDateTime("02/15/2019");       ///MM/dd/yyyy


            int showId = 71;
            DateTime DateOfBooking = Convert.ToDateTime("2/18/2019");        
            int expected = 190;

            ShowController sc = new ShowController();

           var var1 = sc.BookingAvailablity(dateofbooking, showId).Data;

            var actual = Convert.ToInt32(var1);
            //Act


     

     
            Assert.AreEqual(expected, actual) ;
        }

        [TestMethod()]
        public void BookingCalculationTest()
        {
            //Arrange
            ShowController sw = new ShowController();

            Booking b = new Booking() { NoOfSeats =5,BookingId=10,DateOfBooking=Convert.ToDateTime("2/18/2019"), Email="abc@abc.com",PaymentId=1,ShowId=43,TimeOfBooking=DateTime.Now,TotalAmount=0};
            Show s = new Show() {MovieId=14,Rate=200,ScreenId=6,ShowId=43, Time=TimeSpan.Parse("22:00:00")};

            BookingViewModel bvm = new BookingViewModel() { booking= b ,show = s};
            


            //bvm.booking.NoOfSeats = 4;
            //bvm.show.Rate = 190;

            int expected =1000;

            sw.BookingCalculation(bvm);

            var actual = bvm.booking.TotalAmount;

            //Act

            Assert.AreEqual(expected,actual);
        }

       
    }
}