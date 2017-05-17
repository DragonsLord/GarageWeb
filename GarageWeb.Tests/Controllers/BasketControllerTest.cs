using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageWeb.Controllers;

using GarageWeb.Models.ViewModel;
using GarageWeb.Infrastructure;
using GarageWeb.Models.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GarageWeb.Models;

namespace GarageWeb.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        private LiqPay pay_helper = new LiqPay("", "");
        private OrdersRepository repo = new OrdersRepository();

        [TestMethod]
        public void OrederProcessingTest()
        {
            ////var result = pay_helper.PayTest("", 5, "test","","","","", "");
            //var result = pay_helper.CheckOutTest(5, "TEST");
            //result.Wait();
            //System.Diagnostics.Debug.WriteLine(result.Result);
        }
        [TestMethod]
        public void AddOrderTest()
        {
            Order o = new Order()
            {
                Name = "Test",
                DeliveryAddress = null,
                Phone = "380633388156",
                ToPay = 100,
                Time = new DateTime(2017, 5, 10, 6, 0, 0)
            };
            repo.Add(o);
            System.Diagnostics.Debug.WriteLine(o.Id);
        }
    }
}
