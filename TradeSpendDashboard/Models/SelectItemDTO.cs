using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.DTO
{
	public class SelectItemDTO
	{
		public SelectItemDTO() { }
		public SelectItemDTO(string id, string text)
		{
			Id = id;
			Text = text;
		}

		public string Id { get; set; }

		public string Text { get; set; }
	}


	public class SelectItem2DTO
	{
		public SelectItem2DTO() { }
		public SelectItem2DTO(long id, string text)
		{
			Id = id;
			Text = text;
		}

		public long Id { get; set; }

		public string Text { get; set; }
	}
}
