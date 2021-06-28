using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain.Enumerations {
	public enum TicketAanmaakTijd {
		[Display(Name = "All Time")]
		AllTime,
		[Display(Name = "Working Hours")]
		WorkingHours
	}
}
