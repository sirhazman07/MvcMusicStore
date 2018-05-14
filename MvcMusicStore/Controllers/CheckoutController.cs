using MvcMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        //Dependency Injection of Context
        MusicStoreEntities storeDB = new MusicStoreEntities();
        //Promotional Code
        const string PromoCode = "FREE";
        
        //
        // GET: /Checkout/AddressAndPayment
        [HttpGet]
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch (Exception)
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        [HttpGet]
        public ActionResult Complete(int id)
        {
            //Validate customer own this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                //Note: Error View was automatically cretaed for us in the /Views.Shared folder
                //when we began the project.
                return View("Error");
            }
        }

    }
}