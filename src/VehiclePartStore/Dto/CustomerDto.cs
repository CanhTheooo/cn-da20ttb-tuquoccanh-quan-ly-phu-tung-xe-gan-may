using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Dto
{
   public class CustomerDto
    {
        public int Index { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
