using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Domain;

namespace WebApplication1.Business
{
    public interface IProductRepository
    {

        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductByID(int productID);
        Task<Product> InsertProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int productID);

    }
}
