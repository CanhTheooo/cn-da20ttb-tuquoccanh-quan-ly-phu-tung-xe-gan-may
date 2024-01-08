using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePartStore.Domain;

namespace VehiclePartStore.Infrastructure
{
   public class LocalContext : DbContext
    {
        public LocalContext()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            Database.SetInitializer<LocalContext>(null);
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
