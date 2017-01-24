using GloboMart.Common.Helpers;
using GloboMart.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Product.Catalogue.Controllers
{
    public class ProductCatalogueController : ApiController
    {
        // GET: api/ProductCatalogue
        public async Task<IEnumerable<ProductDetails>> Get()
        {
            // ProductType productType
            var items = await DocumentDBRepositoryHelper<ProductDetails>.GetItemsAsync(f => true);
            return items;
        }

        [ActionName("GetProductInfo")]
        // GET api/ProductCatalogue/id
        public async Task<ProductDetails> GetProductInfo(string productId)
        {
            var product = await DocumentDBRepositoryHelper<ProductDetails>.GetItemAsync(productId);
            return product;
        }

        // GET: api/ProductCatalogue/DeleteProduct?productId={id}
        public async Task<bool> DeleteProduct(string productId)
        {
            try
            {
                var deletePriceUrl = string.Format(ConfigurationManager.AppSettings["DeleteProductPricingUrl"], productId);
                WebHelper.Delete(deletePriceUrl);
            }
            catch (Exception)
            {
                // Web Api call to Delete failed. TODO: Log.
            }

            await DocumentDBRepositoryHelper<ProductDetails>.DeleteItemAsync(productId);
            return true;
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<string> CreateAsync([FromBody] ProductDetails product)
        {
            try
            {
                var document = await DocumentDBRepositoryHelper<ProductDetails>.CreateItemAsync(product);

                return document.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
