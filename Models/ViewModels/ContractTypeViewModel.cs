using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class ContractTypeViewModel {

		public int Id { get; set; }

		[Display(Name = "Contract Length")]
		[EnumDataType(typeof(ContractDoorlooptijd))]
		public ContractDoorlooptijd MinimaleDoorlooptijd { get; set; }

		[Display(Name = "Name")]
		public string Naam { get; set; }

		[Display(Name = "Price")]
		//[DataType(DataType.Currency)] Dit toont een £ teken ipv euro
		[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public decimal Prijs { get; set; }

		[Display(Name = "Ticket Submission Time")]
		[EnumDataType(typeof(TicketAanmaakTijd))]
		public TicketAanmaakTijd TicketAanmaaktijd { get; set; }

		[Display(Name = "Resolve Time")]
		[DisplayFormat(DataFormatString = "{0} days")]
		public int TicketAfhandeltijd { get; set; } // in aantal dagen

		[Display(Name = "Ticket Submission Via")]
		[EnumDataType(typeof(TicketAanmaakManier))]
		public ICollection<TicketAanmaakManier> TicketAanmaakmanieren { get; }

		public ContractTypeViewModel() {
		}

		public ContractTypeViewModel(ContractType contractType) {
			Id = contractType.Id;
			MinimaleDoorlooptijd = contractType.MinimaleDoorlooptijd;
			Naam = contractType.Naam;
			Prijs = contractType.Prijs;
			TicketAanmaaktijd = contractType.TicketAanmaaktijd;
			TicketAfhandeltijd = contractType.TicketAfhandeltijd;
			if (contractType.TicketAanmaakmanieren != null) {
				TicketAanmaakmanieren = contractType.TicketAanmaakmanieren.Select(e => e.TicketAanmaakmanier).ToList();
			}
		}
	}
}
