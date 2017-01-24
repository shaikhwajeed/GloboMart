using GloboMart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GloboMart.WebClient.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ProductType Type { get; set; }
        
        public decimal Price { get; set; }

    }
}