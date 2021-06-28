using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class TicketEditViewModel {

		[Display(Name = "Ticket Id")]
		public int Id { get; }

		[Display(Name = "Start date")]
		[DataType(DataType.DateTime)]
		public DateTime Datumaanmaak { get; }

		[Display(Name = "End date")]
		public DateTime? Datumafgehandeld { get; set; }

		[Display(Name = "Contract")]
		public Contract Contract { get; set; }

		[Display(Name = "Department")]
		public Dienst Dienst { get; set; }

		[Display(Name = "Comments")]
		public ICollection<Comment> Comments { get; set; }

		[Display(Name = "Assigned Technician")]
		public Werknemer Toegewezentechnieker { get; set; }

		// Editable fields
		[Display(Name = "Description")]
		public string Omschrijving { get; set; }

		[Display(Name = "Current Status")]
		public TicketStatus Status { get; set; }

		[Display(Name = "Title")]
		public string Titel { get; }

		[Display(Name = "Urgency")]
		public TicketUrgency Urgency { get; set; }

		[Display(Name = "Add a new comment")]
		public string NewComment { get; set; }

		[Display(Name = "Attachments")]
		public List<BijlageViewModel> NewBijlages { get; set; }

		public TicketEditViewModel() {
			NewBijlages = new List<BijlageViewModel>();
		}

		public TicketEditViewModel(Ticket ticket) : this() {
			Id = ticket.Id;
			Datumaanmaak = ticket.DatumAanmaak;
			Datumafgehandeld = ticket.DatumAfgehandeld;
			Contract = ticket.Contract;
			Toegewezentechnieker = ticket.ToegewezenTechnieker;
			NewBijlages = ticket.Bijlages.Select((e, index) => new BijlageViewModel(index,e.Bijlage)).ToList();

			Dienst = ticket.Dienst;
			Comments = ticket.Comments;
			Omschrijving = ticket.Omschrijving;
			Status = ticket.Status;
			Titel = ticket.Titel;
			Urgency = ticket.Urgency;
		}

		public bool IsOpen() {
			return Status != TicketStatus.Cancelled && Status != TicketStatus.Finished;
		}
	}

}
