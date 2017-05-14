using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageWeb.Controllers;

using GarageWeb.Models.ViewModel;
using GarageWeb.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GarageWeb.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        private LiqPay pay_helper = new LiqPay("", "");

        [TestMethod]
        public void OrederProcessingTest()
        {
            var result = pay_helper.PayTest("",1,"test","","","","","");
            result.Wait();
            System.Diagnostics.Debug.WriteLine(result.Result);
        }
    }
}
