using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime DateSale { get; set; }
    }
}
