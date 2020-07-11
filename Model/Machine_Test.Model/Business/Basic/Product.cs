using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.Model
{
    public class Product
    {
        public Int32 Id { get; set; }

        public Int32 ProductId { get; set; }

        public String Name { get; set; }

        public long Price { get; set; }

        public Boolean IsActive { get; set; }

        public String ProductAttribute { get; set; }

        public String ProductDetails { get; set; }

    }
}
