using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePartStore.Dto;
using VehiclePartStore.Infrastructure;

namespace VehiclePartStore.Services
{
   public class StatisticsService
    {
        public List<StatisticsDto> Get(CommonFilterDto filter)
        {
            using (var context = new LocalContext())
            {
                
                //data = context.OrderHeaders
                //    .Where(x => DbFunctions.TruncateTime(x.OrderDate) >= fromNewDate && DbFunctions.TruncateTime(x.OrderDate) <= toNewDate)
                //    .Join
                //    (
                //        context.OrderDetails,
                //        odh => odh.InternalOrderNum,
                //        od => od.InternalOrderNum,
                //        (ohd, od) => new {ohd,od}
                //    )
                //    .GroupBy(grp => new { grp.od.ProductId, grp.od.CategoryId })
                //    .Select(x => new 
                //    {
                //        CategoryId = x.Key.CategoryId,
                //        ProductId = x.Key.ProductId,
                //        TotalQty = x.Sum(y => y.od.OrderQty),
                //        TotalPrice = x.Sum(y=>y.od.TotalPrice)
                //    })
                //    .Join
                //    (
                //        context.Products,                        
                //        sta => sta.ProductId,
                //        prod => prod.ProductId,
                //        (JoinProd,prod) => new 
                //        {
                //            JoinProd,
                //            prod
                //        }
                //    )
                //    .Join
                //    (
                //        context.Categories,
                //        sta => sta.JoinProd.CategoryId,
                //        cat => cat.CategoryId,
                //        (JoinCat,cat) => new { JoinCat, cat }
                //    )
                //    .Select(x=> new StatisticsDto 
                //    {
                //        CategoryId = x.cat.CategoryId,
                //        CategoryName = x.cat.CategoryName,
                //        ProductId = x.JoinCat.prod.ProductId,
                //        ProductName = x.JoinCat.prod.ProductName,
                //        TotalQty = x.JoinCat.JoinProd.TotalQty,
                //        TotalPrice = x.JoinCat.JoinProd.TotalPrice
                //    })
                //    .ToList();

                string query = $"exec VehiclePart_Get_Statistic '{filter.fromDate}', '{filter.toDate}'";
                return context.Database.SqlQuery<StatisticsDto>(query).ToList();

            }
                
        }
    }
}
