using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Dto
{
   public class OrderDetailDto
    {
        public int InternalOrderLineNum { get; set; }
        public int InternalOrderNum { get; set; }
        public CategoryDto Category { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        
      
     
    }
}
