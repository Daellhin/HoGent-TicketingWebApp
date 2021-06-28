using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace _2021_dotnet_g_04.Models.Domain {
	public class Contract {
		public int Nummer { get; set; }
		public DateTime Startdatum { get; set; }
		public DateTime? Einddatum { get; set; }
		public ContractStatus Status { get; set; }
		
		public ContractType ContractType { get; set; }
		public int ContracttypeId { get; set; }

		[JsonIgnore]
		public Klant Klant { get; set; }
		public int KlantId { get; set; }

		[JsonIgnore]
		public ICollection<Ticket> Tickets { get; set; }
		public string DisplayName => ContractType.Naam;

		public Contract() {
			Tickets = new HashSet<Ticket>();
		}

		public Contract(DateTime startDatum, ContractType contractType) : this() {
			if (startDatum.Date.CompareTo(DateTime.Now.Date) < 0) {
				throw new ArgumentException("The starting date of a contract may not be in the past");
			}
			if (contractType.Status.Equals(ContractTypeStatus.Inactive)) {
				throw new ArgumentException("A contract may only be signed with an active contract type");
			}

			Startdatum = startDatum;
			ContractType = contractType;
			Einddatum = contractType.MinimaleDoorlooptijd switch {
				ContractDoorlooptijd.OneYear => startDatum.AddYears(1),
				ContractDoorlooptijd.TwoYear => startDatum.AddYears(2),
				ContractDoorlooptijd.ThreeYear => startDatum.AddYears(3),
				_ => throw new ArgumentException("A contract length must be 1, 2 or 3 years long.")
			};
			if(startDatum.Date.CompareTo(DateTime.Now.Date) == 0)
				Status = ContractStatus.Active;
			else
				Status = ContractStatus.Pending;
		}

		public void AddTicket(Ticket ticket) {
			Tickets.Add(ticket);
		}

		public void CancelContract() {
			if (!IsOpen()) {
				throw new ArgumentException("A finished or cancelled contract can not be canceled");
			}
			Status = ContractStatus.Cancelled;
			Einddatum = DateTime.Now;
		}

		public bool IsOpen() {
			if (Status == ContractStatus.Pending && DateTime.Now.Date.CompareTo(Startdatum.Date) > 0)
				Status = ContractStatus.Active;
			if (DateTime.Now.Date.CompareTo(Einddatum.Value.Date) > 0 && Status == ContractStatus.Active)
				Status = ContractStatus.Finished;
			return Status != ContractStatus.Finished && Status != ContractStatus.Cancelled;
		}
	}
}
