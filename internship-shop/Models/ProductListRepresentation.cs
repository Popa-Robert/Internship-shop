using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Domain;

namespace WebApplication1.Models
{
    public class ProductListRepresentation
    {
        public ProductListRepresentation(List<Product> products)
        {
            this.Products = products.Select(x => new ProductRepresentation(x.ProductID, x.Name)).ToList();
        }

        [JsonProperty(PropertyName = "products")]
        public List<ProductRepresentation> Products { get; set; }
    }
}