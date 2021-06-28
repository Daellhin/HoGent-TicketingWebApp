using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class TicketCreateViewModel {

		[Required(ErrorMessage = "{0} is required")]
		[Display(Name = "Title")]
		public string Titel { get; set; }	
		
		[Required(ErrorMessage = "{0} is required")]
		[EnumDataType(typeof(TicketUrgency))]
		public TicketUrgency Urgency { get; set; }

		[Required(ErrorMessage = "{0} is required")]
		[Display(Name = "Contract")]
		public int ContractNummer { get; set; }

		[Required(ErrorMessage = "{0} is required")]
		[EnumDataType(typeof(Dienst))]
		[Display(Name = "Department")]
		public Dienst Dienst { get; set; }

		[Required(ErrorMessage = "{0} is required")]
		[Display(Name = "Description")]
		public string Omschrijving { get; set; }

		[Display(Name = "Attachments")]
		public List<BijlageViewModel> NewBijlages { get; set; }

		public TicketCreateViewModel() {
			NewBijlages = new List<BijlageViewModel>();
		}

	}
}
