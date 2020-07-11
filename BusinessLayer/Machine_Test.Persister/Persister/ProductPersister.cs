using Machine_Test.Dal;
using Machine_Test.IDal;
using Machine_Test.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Machine_Test.Persister
{
    public class ProductPersister
    {
        public Product Insert(Product product, SqlConnection con = null, SqlTransaction trans = null)
        {
            IProductDal IproductDal = new ProductDal();
            ProductDto productNameDto = new ProductDto
            {
                ProductAttribute ="Name",            
                ProductDetails = product.Name,
                ProductId= IproductDal.GetHighestProductId()+1,
                IsActive =true
            };
            IproductDal.Insert(productNameDto);
            ProductDto productPriceDto = new ProductDto
            {
                ProductAttribute ="Price",
                ProductDetails = product.Price.ToString(),
                ProductId = productNameDto.ProductId,
                IsActive = true
            };
            IproductDal.Insert(productPriceDto);

            return product;
        }

        public Product Update(Product product, SqlConnection con = null, SqlTransaction trans = null)
        {
            IProductDal IproductDal = new ProductDal();
            ProductDto productNameDto = new ProductDto
            {
               
                ProductAttribute = "Name",
                ProductDetails = product.Name,
                ProductId = product.ProductId,
                IsActive = true
            };
            IproductDal.UpdateName(productNameDto);
            ProductDto productPriceDto = new ProductDto
            {
                
                ProductAttribute = "Price",
                ProductDetails = product.Price.ToString(),
                ProductId = product.ProductId,
                IsActive = true
            };
            IproductDal.UpdatePrice(productPriceDto);

            return product;
        }


        public Product Delete(Product product)
        {
            IProductDal IproductDal = new ProductDal();
            ProductDto productDto = new ProductDto
            {
                Id = product.Id,
                ProductId=product.ProductId,
                IsActive = false
            };
            IproductDal.Delete(productDto);
            
            return product;
        }
        public static Product GetProduct(Int32 ProductId)
        {
            IProductDal IproductDal = new ProductDal();
            List<Product> productDtoList = ProductList.GetList(ProductId);
            Product product = new Product();
            product.ProductId = ProductId;
            product.Name = productDtoList.Where(a => a.ProductAttribute == "Name").FirstOrDefault().ProductDetails;
            product.Price = long.Parse(productDtoList.Where(a => a.ProductAttribute == "Price").FirstOrDefault().ProductDetails);

            return product;
        }



        public static Product Get()
        {
            Product product = new Product();
            return product;
        }

        public static ProductPersister GetPersister()
        {
            ProductPersister productPersister = new ProductPersister();
            return productPersister;
        }


    }
}
