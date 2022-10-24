using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Temp
{
    public class HistorycalContract
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Area { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Zone { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Distributor { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Distributor_Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Outlet_ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Oulet_Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ProductID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Product_Description { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        //[Column(TypeName = "decimal(5,2)")]
        public string Parent_SKU { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal Price_BeforeTax { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal Price_AfterTax { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal Direct_Discount { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal Quarter_reward { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal QtyMonth { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal SalesPerMonth { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal BudgetDir_Disc_excPPn { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal BudgetQuar_Rwrd_ExcPPn { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,3)")]
        public decimal CR { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Nama_BAS { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Status { get; set; }
    }
}
