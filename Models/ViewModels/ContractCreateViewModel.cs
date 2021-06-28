using _2021_dotnet_g_04.Extensions;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class ContractCreateViewModel {

		[Display(Name = "Start Date")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "{0} is required")]
		public DateTime Startdatum { get; set; }

		[Display(Name = "Contract Type")]
		[Required(ErrorMessage = "{0} is required")]
		public int ContractTypeId { get; set; }

		public ContractCreateViewModel() {
			Startdatum = DateTime.Today;
		}

	}
}
