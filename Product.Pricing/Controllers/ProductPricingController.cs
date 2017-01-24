using GloboMart.Common.Helpers;
using GloboMart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Product.Pricing.Controllers
{
    public class ProductPricingController : ApiController
    {
        // GET: api/ProductPricing
        public async Task<IEnumerable<ProductPrice>> Get()
        {
            // ProductType productType
            var items = await DocumentDBRepositoryHelper<ProductPrice>.GetItemsAsync(f => true);
            return items;
        }


        // GET api/ProductPricing/id
        public async Task<ProductPrice> GetPriceInfo(string productId)
        {
            var priceData = await DocumentDBRepositoryHelper<ProductPrice>.GetItemAsync(productId);
            return priceData;
        }


        [HttpPost]
        [ActionName("Create")]
        public async Task<string> CreateAsync([FromBody]  ProductPrice productPrice)
        {
            try
            {
                var document = await DocumentDBRepositoryHelper<ProductPrice>.CreateItemAsync(productPrice);
                return document.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/ProductPricing/DeletePriceData?productId={id}
        public async Task<bool> DeletePriceData(string productId)
        {
            await DocumentDBRepositoryHelper<ProductPrice>.DeleteItemAsync(productId);
            return true;
        }

    }
}
