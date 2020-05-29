using NGO.Models;
using NGO.Models.EntityModels;
using NGO.Models.PayPalModel;
using NGO.Models.ViewModels;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGO.Controllers
{
    public class PayPalController : Controller
    {
        // GET: PayPal
        public ActionResult Index()
        {
            return View();
        }
        //work with paypal payment
        private Payment payment;

        //create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var usrDonate = (UserDonateView)Session["ud"];
            var lsItem = new ItemList() { items = new List<Item>() };
            lsItem.items.Add(new Item { name = usrDonate.DonateName, currency = "USD", price = usrDonate.Money.ToString(), quantity = "1", sku = "sku" });

            var payer = new Payer()
            {
                payment_method = "paypal",
            };
            var redictUrl = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            var detail = new Details() { tax = "0", shipping = "0", subtotal = usrDonate.Money.ToString() }; //subtotal : total order, note: sum(price*quantity) if sum is incorrect, it will have an error 400.
            var amount = new Amount() { currency = "USD", details = detail, total = usrDonate.Money.ToString() }; //total= tax + shipping + subtotal
            var transList = new List<Transaction>();
            transList.Add(new Transaction
            {
                description = "Donate using PayPal",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = lsItem,

            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transList,
                redirect_urls = redictUrl
            };
            return this.payment.Create(apiContext);
        }
        //create execute payment method
        private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentID)
        {
            var paymentExecute = new PaymentExecution() { payer_id = payerID };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(apiContext, paymentExecute);
        }
        //create method
        public ActionResult PaymentWithPaypal()
        {
            var usrDonate = (UserDonateView)Session["ud"];
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerID = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerID))
                {
                    //create a payment
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPaypal?guid=";
                    string guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseUri + guid);

                    var link = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (link.MoveNext())
                    {
                        Links links = link.Current;
                        if (links.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = links.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerID, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        TempData["error"] = "Donate Failed. Please try again!";
                        return RedirectToAction("Donate","Home");
                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                TempData["error"] = "Donate Failed. Please try again!";
                PaypalLogger.Log("Error: " + ex.Message);
                return RedirectToAction("Donate", "Home");
            }
            var ud = new UserDonate { DonateID = usrDonate.DonateID, Money = usrDonate.Money, UserID = usrDonate.UserID, TypeCard = usrDonate.TypeCard };
            if (Repositories.InsertUD(ud)>0)
            {
                TempData["success"]= "Donate Successfully! We appreciate it!";
            }
            else
            {
                TempData["error"] = "Donate Failed. Please try again!";
            }
            return RedirectToAction("Donate", "Home");
        }

    }
}