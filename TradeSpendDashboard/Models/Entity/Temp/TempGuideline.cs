using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Temp
{
    public class TempGuideline
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string GuidlineId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string SubChannelId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ProductGroupId { get; set; }
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
