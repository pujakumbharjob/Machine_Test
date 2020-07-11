using Machine_Test.Dal;
using Machine_Test.IDal;
using Machine_Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.Persister
{
    public class ProductList
    {

        public static List<Product> GetList(Int32 ProductId)
        {
            return Fetch(ProductId);
        }

        private static List<Product> Fetch(Int32 ProductId)
        {
            IProductDal IproductDal = new ProductDal();
            List<Product> productList = new List<Product>();
            foreach (var item in IproductDal.GetList(ProductId))
            {
                Product productDto = new Product
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductAttribute=item.ProductAttribute,
                    ProductDetails=item.ProductDetails
                };
                productList.Add(productDto);
            }
            return productList;
        }

        public static List<Product> GetPIVOTList()
        {
            return fetPIVOTList();
        }

        private static List<Product> fetPIVOTList()
        {
            IProductDal IproductDal = new ProductDal();
            List<Product> productList = new List<Product>();
            foreach (var item in IproductDal.GetPIVOTList())
            {
                Product productDto = new Product
                {
                  
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Price = long.Parse(item.Price.ToString())
                };
                productList.Add(productDto);
            }
            return productList;
        }
    }
}
