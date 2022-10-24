using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class Entity : IEntity
    {
        public long Id { get; set; }

        public bool IsActive { get; set; }
        
        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string CreatedBy { get; set; }

        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string UpdatedBy { get; set; }

        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime UpdatedDate { get; set; }
    }
}
