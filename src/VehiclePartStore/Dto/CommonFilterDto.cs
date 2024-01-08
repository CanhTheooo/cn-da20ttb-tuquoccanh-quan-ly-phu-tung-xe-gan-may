using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Dto
{
    public class CommonFilterDto
    {
        public int InternalOrderNum { get; set; }
        public int InternalOrderLineNum { get; set; }
        public int ProductId { get; set; } = -1;
        public int CategoryId { get; set; } = -1;
        public int CustomerId { get; set; } = -1;
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
}
