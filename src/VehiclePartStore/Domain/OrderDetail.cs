using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePartStore.Domain
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int InternalOrderLineNum { get; set; }
        public int InternalOrderNum { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
       
        [ForeignKey("CategoryId")]
        public  Category Category { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
       
    }
}
