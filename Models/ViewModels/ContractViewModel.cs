using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class ContractViewModel {

		[Display(Name = "Contract ID")]
		public int Nummer { get; set; }

		[Display(Name = "Start Date")]
		[DataType(DataType.Date)]
		public DateTime Startdatum { get; set; }

		[Display(Name = "End Date")]
		[DataType(DataType.Date)]
		public DateTime? Einddatum { get; set; }

		[Display(Name = "Status")]
		[EnumDataType(typeof(ContractStatus))]
		public ContractStatus Status { get; set; }

		[Display(Name = "Contract Type")]
		public ContractTypeViewModel ContractType { get; set; }

		public ContractViewModel(Contract contract) {
			Nummer = contract.Nummer;
			Startdatum = contract.Startdatum;
			Einddatum = contract.Einddatum;
			Status = contract.Status;
			ContractType = new ContractTypeViewModel(contract.ContractType);
		}

		public bool IsOpen() {
			return Status != ContractStatus.Finished && Status != ContractStatus.Cancelled;
		}
	}
}
