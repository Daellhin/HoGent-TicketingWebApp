using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace _2021_dotnet_g_04.Models.Domain {
	public class Ticket {

		public int Id { get; set; }
		public bool BekekenDoorTechnieker { get; set; }
		public DateTime DatumAanmaak { get; set; }
		public DateTime? DatumAfgehandeld { get; set; }
		public Dienst Dienst { get; set; }
		public string Omschrijving { get; set; }
		public TicketStatus Status { get; set; }
		public string Titel { get; set; }
		public TicketUrgency Urgency { get; set; }
		public int ContractNummer { get; set; }
		public int? ToegewezenTechniekerId { get; set; }
		public bool BekekenDoorKlant { get; set; }

		public Contract Contract { get; set; }
#nullable enable
		public Werknemer? ToegewezenTechnieker { get; set; }
#nullable disable
		public List<Comment> Comments { get; set; }
		public List<TicketBijlage> Bijlages { get; set; }

		public Ticket() {
			Comments = new List<Comment>();
			Bijlages = new List<TicketBijlage>();
			DatumAanmaak = DateTime.Now;
			Status = TicketStatus.Created;
		}

		public Ticket(String titel, TicketUrgency urgency, Contract contract, Dienst dienst, String omschrijving, List<BijlageViewModel> bijlages, Werknemer toeTeWijzenTechnieker) : this() {
			if (String.IsNullOrEmpty(titel)) {
				throw new ArgumentException("Title may not be empty");
			}
			if (String.IsNullOrEmpty(omschrijving)) {
				throw new ArgumentException("Description may not be empty");
			}
			if (contract == null) {
				throw new ArgumentException("A contract must be selected");
			}
			Titel = titel;
			Urgency = urgency;
			ContractNummer = contract.Nummer;
			Contract = contract;
			Dienst = dienst;
			Omschrijving = omschrijving;
			Bijlages = bijlages.Select(e => new TicketBijlage(e.Bijlage)).ToList();
			SetTechnician(toeTeWijzenTechnieker);
		}

		private void SetTechnician(Werknemer werknemer) {
			if (werknemer != null) {
				ToegewezenTechnieker = werknemer;
				Status = TicketStatus.Pending;
			}
			else
				Status = TicketStatus.Created;
		}

		public bool IsOpen() {
			return Status != TicketStatus.Cancelled && Status != TicketStatus.Finished;
		}

		public void EditTicket(TicketUrgency urgency, string newComment, List<TicketBijlage> newBijlages, string persoonDieOpmerkingToevoegt) {
			if (!IsOpen()) {
				throw new ArgumentException("A finished or cancelled ticket can not be edited");
			}

			Urgency = urgency;
			if (!String.IsNullOrEmpty(newComment))
				Comments.Add(new Comment(newComment, persoonDieOpmerkingToevoegt, this));

			Bijlages = newBijlages;
			Status = TicketStatus.ReceivedCustomerInformation;
		}

		public void CancelTicket() {
			if (!IsOpen()) {
				throw new ArgumentException("A finished or cancelled ticket can not be canceled");
			}
			Status = TicketStatus.Cancelled;
			DatumAfgehandeld = DateTime.Now;
		}

		public int GetHandlingTime() {
			if (IsOpen()) {
				throw new ArgumentException("The handling time can not be calculated of a open ticket");
			}
			return (DatumAfgehandeld.GetValueOrDefault() - DatumAanmaak).Days;
		}

		public bool IsFinishedOnTime() {
			return (Contract.ContractType.TicketAfhandeltijd - GetHandlingTime() ) >= 0;
		}
	}
}