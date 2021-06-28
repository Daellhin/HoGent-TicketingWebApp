using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain.Enumerations {
	public enum TicketAanmaakManier {
		[Display(Name = "Email")]
		Email,
		[Display(Name = "Telephone")]
		Telephone,
		[Display(Name = "Web")]
		Application
	}
}
