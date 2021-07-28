using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_dotnet_g_04.Models.Domain {
	public class Klant {
		public int Id { get; set; }
		public string Bedrijfsnaam { get; set; }
		public DateTime Datumklantgeworden { get; set; }
		public string Gebruikersnaam { get; set; }
		public GebruikersStatus Status { get; set; }
		public string Wachtwoord { get; set; }
		public string Busnr { get; set; }
		public int Huisnummer { get; set; }
		public string Land { get; set; }
		public string Postcode { get; set; }
		public string Straat { get; set; }
		public string Woonplaats { get; set; }

		public ICollection<Contactpersoon> Contactpersonen { get; set; }
		public ICollection<Contract> Contracten { get; set; }

		public Klant() {
			Contactpersonen = new HashSet<Contactpersoon>();
			Contracten = new HashSet<Contract>();
		}

		#region Contactpersoon methods
		public void AddContactpersoon(Contactpersoon contactpersoon) {
			Contactpersonen.Add(contactpersoon);
		}

		#endregion

		#region Ticket methods
		public void CreateTicket(String titel, TicketUrgency urgency, int nummer, Dienst dienst, String omschrijving, List<BijlageViewModel> bijlages, Werknemer toeTeWijzenTechnieker) {
			Ticket ticket = new Ticket(titel, urgency, Contracten.Where(e => e.Nummer == nummer).Single(), dienst, omschrijving, bijlages, toeTeWijzenTechnieker);
			AddTicketToContract(nummer, ticket);
		}

		private void AddTicketToContract(int nummer, Ticket ticket) {
			Contracten.Where(e => e.Nummer == nummer).Single().AddTicket(ticket);
		}

		public void EditTicket(int ticketId, TicketUrgency urgency, string newComment, List<TicketBijlage> newBijlages) {
			Ticket ticket = GetTicketBy(ticketId);
			if (ticket == null) {
				throw new KeyNotFoundException();
			}
			ticket.EditTicket(urgency, newComment, newBijlages, Gebruikersnaam);
		}

		public IEnumerable<Ticket> GetTickets() {
			return Contracten.SelectMany(e => e.Tickets);
		}

		public IEnumerable<Ticket> GetOpenTickets() {
			return GetTickets().Where(e => e.IsOpen());
		}

		public IEnumerable<Ticket> GetClosedTickets() {
			return GetTickets().Where(e => !e.IsOpen());
		}

		public Ticket GetTicketBy(int ticketId) {
			return GetTickets().Where(e => e.Id == ticketId).SingleOrDefault();
		}
		#endregion

		#region Contract methods
		public List<Contract> GetOpenContracts() {
			return Contracten.Where(e => e.IsOpen()).ToList();
		}

		public Contract GetContractBy(int id) {
			return Contracten.Where(e => e.Nummer == id).SingleOrDefault();
		}

		public List<Contract> GetClosedContracts() {
			return Contracten.Where(e => !e.IsOpen()).ToList();
		}

		public void CreateContract(DateTime startdatum, ContractType contractType) {
			//Hier wordt er gekeken of er openstaande contracten zijn van hetzelfde type die elkaar overlappen
			List<Contract> overlappingContracts = GetOpenContracts().Where(e => e.ContractType == contractType && startdatum.CompareTo(e.Startdatum) >= 0 && startdatum.CompareTo(e.Einddatum) <= 0).ToList();
			if (overlappingContracts.Count() > 0) {
				throw new ArgumentException($"You already have at least one contract of this type that overlaps: {contractType.Naam} - { overlappingContracts.First().Startdatum.ToShortDateString()}. Please select a different contract type or a different start date.");
			}
			Contract contract = new Contract(startdatum, contractType);
			AddContract(contract);
		}

		public void AddContract(Contract contract) {
			Contracten.Add(contract);
		}
		#endregion
	}
}
