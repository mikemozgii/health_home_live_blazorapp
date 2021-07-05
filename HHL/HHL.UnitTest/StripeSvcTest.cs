using HHL.Auth.Core.Services;
using HHL.Core.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stripe;
using System.Threading.Tasks;

namespace HHL.UnitTest
{
    [TestClass]
    public class StripeSvcTest
    {
        //[TestMethod]
        //public async Task RegisterTestMethod()
        //{

        //    HHLConfigHdr.Init();


        //    var options11 = new CustomerCreateOptions
        //    {
        //        Description = "Customer for jenny.rosen@example.com"
        //    };

        //    var service = new CustomerService();
        //    Customer customer = service.Create(options11);




        //    var options = new PaymentMethodCreateOptions
        //    {
        //        Type = "card",
        //        Card = new PaymentMethodCardCreateOptions()
        //        {
        //            Number = "4242424242424242",
        //            ExpMonth = 5,
        //            ExpYear = 2020,
        //            Cvc = "123",
        //        },
               
        //    };

        //    var service = new PaymentMethodService();
        //    var paymentMethod = service.Create(options);


        //    var options2 = new PaymentMethodAttachOptions
        //    {
        //        CustomerId = "111",
        //    };
        //    var paymentMethod123 = service.Attach(paymentMethod.Id, options2);


        //    var options1 = new PaymentMethodListOptions
        //    {
        //        Limit = 3,
        //        CustomerId = "111"
        //    };
        //    var paymentmethods = service.List(options1);


        //    //var accAuthSvc = new AccAuthSvc();

        //    //var resp = await accAuthSvc.Register(new Auth.Core.Models.RegisterRequest
        //    //{

        //    //    Email = "khokhlov.mikhail@gmail.com",
        //    //    AccountName = "mikekh",
        //    //    Password = "Temp3232"
        //    //});

        //    //Assert.AreNotEqual(null, resp);
        //}

        //[TestMethod]
        //public async Task SignInTestMethod()
        //{
        //    var accAuthSvc = new AccAuthSvc();

        //    var resp = await accAuthSvc.SignIn(new Auth.Core.Models.SignInRequest
        //    {

        //        Name = "khokhlov.mikhail@gmail.com",
        //        Password = "Temp3232"
        //    });

        //    Assert.AreNotEqual(null, resp);
        //}
    }
}
