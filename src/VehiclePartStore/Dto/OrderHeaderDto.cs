using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Dto
{
    public class OrderHeaderDto
    {
        public int Index { get; set; }
        public int InternalOrderNum { get; set; }
        public CustomerDto Customer { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Description { get; set; }
        public virtual List<OrderDetailDto> OrderDetails { get; set; }
    }
}
