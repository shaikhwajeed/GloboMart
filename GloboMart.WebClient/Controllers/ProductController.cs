using GloboMart.Common.Helpers;
using GloboMart.Common.Models;
using GloboMart.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GloboMart.WebClient.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            //ProductCatalogUrl
            List<ProductViewModel> productModels = new List<Models.ProductViewModel>();
            try
            {
                // Get all products for demo purpose. We could provide filters on UI based on Type, Price Range etc... 
                var productData = WebHelper.GetRequest(ConfigurationManager.AppSettings["ProductCatalogUrl"]);
                var products = JsonHelper.ConverToObject<List<ProductDetails>>(productData);
                List<ProductPrice> prices = new List<ProductPrice>();

                try
                {
                    // Get pricing Data.
                    var pricingData = WebHelper.GetRequest(ConfigurationManager.AppSettings["ProductPricingUrl"]);
                    prices = JsonHelper.ConverToObject<List<ProductPrice>>(pricingData);
                }
                catch (Exception)
                {
                    // Let it run even if we don't have Pricing data for now.
                }

                foreach (var product in products)
                {
                    // Set data to be shown in UI.
                    var model = new ProductViewModel();
                    model.Id = product.Id;
                    model.Name = product.Name;
                    model.Type = product.Type;

                    // Get pricing data.
                    var priceDetail = prices.Where(p => p.Id == product.Id).FirstOrDefault();
                    if (priceDetail != null)
                        model.Price = priceDetail.Price;
                    productModels.Add(model);
                }

            }
            catch (WebException)
            {
                ViewBag.Exception = "Unable to connect to the server right now. Please try again later.";
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.ToString();
            }

            return View(productModels);
        }

        [ActionName("Create")]
        public ActionResult CreateAsync()
        {
            return View();
        }

        // GET: Product/Create
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateAsync([Bind(Include = "Name,Type,Price")] ProductViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // Get data from Model.
                    var productCatalogData = new ProductDetails();
                    productCatalogData.Name = model.Name;
                    productCatalogData.Type = model.Type;

                    // Add Product Data.
                    var response = WebHelper.PostRequest(ConfigurationManager.AppSettings["CreateProductCatalogUrl"], JsonHelper.ConverToJson(productCatalogData));
                    var productId = JsonHelper.ConverToObject<string>(response);

                    var priceData = new ProductPrice();
                    priceData.Id = productId;
                    priceData.Price = model.Price;
                    // Add Pricing Data.
                    WebHelper.PostRequest(ConfigurationManager.AppSettings["AddProductPriceUrl"], JsonHelper.ConverToJson(priceData));

                    return RedirectToAction("Index");
                }
                catch (WebException)
                {
                    ViewBag.Exception = "Unable to connect to the server right now. Please try again later.";
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex.ToString();
                }
            }

            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var url = string.Format(ConfigurationManager.AppSettings["GetProductDetailsUrl"], id);
            var productData = WebHelper.GetRequest(url);
            var item = JsonHelper.ConverToObject<ProductDetails>(productData);
            if (productData == null)
            {
                return HttpNotFound();
            }

            var model = new ProductViewModel();
            model.Id = item.Id;
            model.Name = item.Name;
            model.Type = item.Type;

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id")] string id)
        {
            // await DocumentDBRepository<Item>.DeleteItemAsync(id);
            var url = string.Format(ConfigurationManager.AppSettings["DeleteProductCatalogUrl"], id);
            var productData = WebHelper.Delete(url);
            return RedirectToAction("Index");
        }

    }
}
