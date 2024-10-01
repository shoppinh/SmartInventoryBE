using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInventoryBE.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public decimal? Purchase { get; set; }
        public decimal? LastDividend { get; set; }
        public string? Industry { get; set; }
        public long? MarketCap { get; set; }
    }
}