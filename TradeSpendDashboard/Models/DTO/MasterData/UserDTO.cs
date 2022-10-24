using TradeSpendDashboard.Models.DTO.MasterData;
using System.Collections.Generic;

namespace TradeSpendDashboard.Models.DTO.MasterData
{
    public class UserDTO 
    {
        public long ParentId { get; set; }

        public string ParentName { get; set; }

        public long LevelId { get; set; }

        public string LevelName { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}