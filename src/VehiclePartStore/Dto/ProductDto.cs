using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Dto
{
    public class ProductDto
    {
        public int Index { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }        
        public virtual CategoryDto Category { get; set; }
    }
}
