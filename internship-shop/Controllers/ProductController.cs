using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Domain;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _repo;
        public static IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository repo, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<ActionResult<ProductListRepresentation>> GetAllProducts()
        {
            try
            {
                var dbProducts = await _repo.GetAllProducts();
                return Ok(new ProductListRepresentation(dbProducts));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpGet("{productID:int}")]
        public async Task<ActionResult<ProductRepresentation>> GetCategoryByID(int productID)
        {
            try
            {
                var dbProduct = await _repo.GetProductByID(productID);

                if (dbProduct == null)
                {
                    return NotFound();
                }

                return new ProductRepresentation(dbProduct.ProductID, dbProduct.Name);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<ProductRepresentation>> InsertProduct(Product product, [FromForm] ProductImage imageFile)
        {

            try
            {
                if (product == null)
                    return BadRequest();

                var dbProduct = await _repo.InsertProduct(product);
                return new ProductRepresentation(dbProduct.ProductID, dbProduct.Name);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new product record");
            }

        }


        [HttpPut("{productID:int}")]
        public async Task<ActionResult<ProductRepresentation>> Update(int productID, Product product)
        {

            try
            {
                if (productID != product.ProductID)
                    return BadRequest("Product ID mismatch");
                var productToUpdate = await _repo.GetProductByID(productID);
                if (productToUpdate == null)
                    return NotFound($"Product with Id = {productID} not found");
                var dbProduct = await _repo.UpdateProduct(product);
                return new ProductRepresentation(dbProduct.ProductID, dbProduct.Name);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }

        }


        [HttpDelete("{productID:int}")]
        public async Task<ActionResult<ProductRepresentation>> Delete(int productID)
        {

            try
            {
                var productToDelete = await _repo.GetProductByID(productID);
                if (productToDelete == null)
                {
                    return NotFound($"product with Id = {productID} not found");
                }
                var dbProduct = await _repo.DeleteProduct(productID);
                return new ProductRepresentation(dbProduct.ProductID, dbProduct.Name);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }

        }

        [HttpPost("UploadFile")]
        public string SaveImage([FromForm] ProductImage imageFile)
        {

            try
            {

                if (imageFile.File.Length > 0)
                {

                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (var filestream = System.IO.File.Create(path + imageFile.File.FileName))
                    {
                        imageFile.File.CopyTo(filestream);
                        filestream.Flush();
                        return "Uploaded.";
                    }
                }
                else
                {
                    return "Not Uploaded";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }




    }
}
