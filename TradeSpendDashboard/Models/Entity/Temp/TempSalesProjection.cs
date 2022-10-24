using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Temp
{
    public class TempSalesProjection
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string SalesProjectionId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Distributor { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Outlet { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string SubChannel { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ProductGroup { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        //[Column(TypeName = "decimal(5,2)")]
        public decimal GrossSales { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        //[Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        //[Column(TypeName = "decimal(5,2)")]
        public decimal Reward { get; set; }
    }
}
