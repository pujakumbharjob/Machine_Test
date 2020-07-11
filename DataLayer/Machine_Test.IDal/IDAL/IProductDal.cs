using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.IDal
{
    public interface IProductDal
    {
        List<ProductDto> GetList(Int32 ProductId);

        ProductDto GetProduct(Int32 Id);

        Int32  GetHighestProductId();

        ProductDto Insert(ProductDto productDto);

        ProductDto UpdateName(ProductDto productDto);

        ProductDto UpdatePrice(ProductDto productDto);

        List<ProductUpdateDto> GetPIVOTList();

       ProductDto Delete(ProductDto productDto);
    }
}
