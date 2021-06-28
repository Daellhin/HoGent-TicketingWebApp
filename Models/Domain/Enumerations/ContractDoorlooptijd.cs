using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain.Enumerations {
	public enum ContractDoorlooptijd {
		[Display(Name = "One Year")]
		OneYear,
		[Display(Name = "Two Years")]
		TwoYear,
		[Display(Name = "Three Years")]
		ThreeYear
	}
}
