using Machine_Test.IDal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.Dal
{
    public class ProductDal : IProductDal
    {
        public ProductDto Delete(ProductDto productDto)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(" Update MachineTest set IsActive=0 where ProductId=@productId", con);
            cmd.Parameters.AddWithValue("@productId", productDto.ProductId);

            cmd.ExecuteNonQuery();
            con.Close();

            return productDto;
        }

        public List<ProductDto> GetList(Int32 ProductId)
        {
            List<ProductDto> result = new List<ProductDto>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select Id,ProductId,ProductAttribute, ProductDetails from MachineTest where ProductId =@productId and IsActive = 1", con);
            con.Open();
            cmd.Parameters.AddWithValue("@productId", ProductId);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result.Add(GetDto(dr));
            }
            con.Close();
            return result;
        }

        public ProductDto GetProduct(int Id)
        {
            ProductDto result = new ProductDto();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(" Select Id,ProductId,ProductAttribute,ProductDetails,IsActive from MachineTest  Where Id=@id and IsActive=1", con);
            cmd.Parameters.AddWithValue("@id", Id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = GetDto(dr);
            }
            con.Close();
            return result;
        }

        public ProductDto Insert(ProductDto productDto)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(" Insert Into MachineTest(ProductId,ProductAttribute,ProductDetails,IsActive) Output Inserted.Id Values(@productId,@productAttribute,@productDetails,@isActive) ", con);
            cmd.Parameters.AddWithValue("@productId", productDto.ProductId);
            cmd.Parameters.AddWithValue("@productAttribute", productDto.ProductAttribute);
            cmd.Parameters.AddWithValue("@productDetails", productDto.ProductDetails);
            cmd.Parameters.AddWithValue("@isactive", productDto.IsActive);

            Int32 InsertedId = Int32.Parse(cmd.ExecuteScalar().ToString());
            productDto.Id = InsertedId;

                con.Close();
           
            return productDto;
        }

        public ProductDto UpdateName(ProductDto productDto)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(" Update MachineTest set ProductId=@productId,ProductAttribute=@productAttribute,ProductDetails=@productDetails,IsActive=@isActive where ProductId=@productid and ProductAttribute='Name'", con);
            cmd.Parameters.AddWithValue("@productId", productDto.ProductId);
            cmd.Parameters.AddWithValue("@productAttribute", productDto.ProductAttribute);
            cmd.Parameters.AddWithValue("@productDetails", productDto.ProductDetails);
            cmd.Parameters.AddWithValue("@isactive", productDto.IsActive);

            cmd.ExecuteNonQuery();
            con.Close();

            return productDto;
        }

        public ProductDto UpdatePrice(ProductDto productDto)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(" Update MachineTest set ProductId=@productId,ProductAttribute=@productAttribute,ProductDetails=@productDetails,IsActive=@isActive where ProductId=@productid and ProductAttribute='Price'", con);
            cmd.Parameters.AddWithValue("@productId", productDto.ProductId);
            cmd.Parameters.AddWithValue("@productAttribute", productDto.ProductAttribute);
            cmd.Parameters.AddWithValue("@productDetails", productDto.ProductDetails);
            cmd.Parameters.AddWithValue("@isactive", productDto.IsActive);


            cmd.ExecuteNonQuery();
            con.Close();

            return productDto;
        }

        public ProductDto GetDto(SqlDataReader sdr)
        {
            return new ProductDto
            {
                Id = sdr.GetInt32(sdr.GetOrdinal("Id")),
                ProductId = sdr.GetInt32(sdr.GetOrdinal("ProductId")),
                ProductAttribute = sdr.GetString(sdr.GetOrdinal("ProductAttribute")),
                ProductDetails = sdr.GetString(sdr.GetOrdinal("ProductDetails")),
                //IsActive = sdr.GetBoolean(sdr.GetOrdinal("IsActive")),

            };
        }

        public ProductUpdateDto GetDtoforPIVOTE(SqlDataReader sdr)
        {
            return new ProductUpdateDto
            {
                ProductId = sdr.GetInt32(sdr.GetOrdinal("ProductId")),
                Name = sdr.GetString(sdr.GetOrdinal("Name")),
                Price = sdr.GetString(sdr.GetOrdinal("Price")),
                //IsActive = sdr.GetBoolean(sdr.GetOrdinal("IsActive")),

            };
        }

        public Int32 GetHighestProductId()
        {
            
            Int32 result = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Case When max(ProductId) is null then 0 else max(ProductId) end from MachineTest where IsActive=1", con);
            result = Int32.Parse(cmd.ExecuteScalar().ToString());
            con.Close();

            return result;
        }

        public List<ProductUpdateDto> GetPIVOTList()
        {
            List<ProductUpdateDto> result = new List<ProductUpdateDto>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from (select ProductId, ProductAttribute, ProductDetails, IsActive from  MachineTest) MT PIVOT(max(ProductDetails) for ProductAttribute in ([Name],[Price])) PD where IsActive = 1", con);
              con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result.Add(GetDtoforPIVOTE(dr));
            }
            con.Close();
            return result;
        }
    }
}
