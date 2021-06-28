using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class ChartViewModel {

		public int Id { get; set; }
		public string ChartType { get; set; }
		public string Title { get; set; }
		public string YLabel { get; set; } // Value label
		public string XLabel { get; set; } // Key label
		public Dictionary<string, int> Data { get; set; }

		public ChartViewModel(int id, string chartType, string title, string yLabel, string xLabel, Dictionary<string, int> data) {
			Id = id;
			ChartType = chartType;
			Title = title;
			YLabel = yLabel;
			XLabel = xLabel;
			Data = data;
		}
	}
}
