using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Domain
{
    [Table("OrderHeader")]
    public class OrderHeader
    {
        [Key]
        public int InternalOrderNum { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Description { get; set; }
        public virtual List<OrderDetail> OrderDetails {get;set;}
    }
}
