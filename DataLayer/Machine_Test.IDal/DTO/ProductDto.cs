using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.IDal
{
   public class ProductDto
    {
        public Int32 Id { get; set; }

        public Int32 ProductId { get; set; }

        public String ProductAttribute { get; set; }

        public String ProductDetails { get; set; }

        public Boolean IsActive { get; set; }

    }
}
