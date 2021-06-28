using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2021_dotnet_g_04.Tests.Data {
	public class DummyDbContext {

		public List<ContractType> ContractTypes { get; set; }
		public List<Contract> Contracts { get; set; }
        public List<Contract> ContractsNietActief { get; set; }
        public Klant Klant1 { get; set; }
		public Klant KlantZonderActiefContract { get; set; }
		public ICollection<Contactpersoon> ContactPersonen { get; set; }
		public ICollection<Ticket> TicketsBijContract1 { get; set; }
		public ICollection<Ticket> TicketsBijContract2 { get; set; }
		public ICollection<Ticket> TicketsBijContract3 { get; set; }
		public Werknemer Werknemer { get; set; }
		public Contract Contract1 { get; set; }
		public Ticket Ticket1 { get; set; }
		public Ticket TicketFinished { get; set; }
		public Ticket TicketCanceled { get; set; }
		public ContractType ContractType1 { get; set; }
		public ContractType ContractType2 { get; set; }


		public DummyDbContext() {
			TicketsBijContract1 = new List<Ticket>();
			TicketsBijContract2 = new List<Ticket>();
			TicketsBijContract3 = new List<Ticket>();
			TicketBijlage ticketBijlage = new TicketBijlage() { Bijlage = "testbijlage.com", TicketId = 1 };
			List<TicketBijlage> ticketBijlages = new List<TicketBijlage>();
			ticketBijlages.Add(ticketBijlage);

			//comments
			Comment comment1 = new Comment() { Id = 1, Opmerking = "Een opmerking", PersoonDieOpmerkingToevoegt = "Lotus", Ticket = Ticket1, TicketId = 1, Tijdstip = DateTime.Now };
			Comment comment2 = new Comment() { Id = 2, Opmerking = "Een opmerking twee", PersoonDieOpmerkingToevoegt = "Sutol", Ticket = Ticket1, TicketId = 1, Tijdstip = DateTime.Now };
			List<Comment> comments = new List<Comment> { comment1, comment1 };

			//ticketaanmaakmanieren
			List<ContractTypeTicketAanmaakmanier> ticketAanmaakmanieren = new List<ContractTypeTicketAanmaakmanier> { new ContractTypeTicketAanmaakmanier() { TicketAanmaakmanier = TicketAanmaakManier.Application } };

			//Contracttypes
			ContractType1 = new ContractType() {
				Id = 1,
				MinimaleDoorlooptijd = ContractDoorlooptijd.OneYear,
				Naam = "ContractType1",
				Prijs = new decimal(984.5),
				Status = ContractTypeStatus.Active,
				TicketAanmaaktijd = TicketAanmaakTijd.AllTime,
				TicketAfhandeltijd = 45,
				TicketAanmaakmanieren = ticketAanmaakmanieren
			};

			ContractType2 = new ContractType() {
				Id = 2,
				MinimaleDoorlooptijd = ContractDoorlooptijd.TwoYear,
				Naam = "ContractType2",
				Prijs = new decimal(1500.5),
				Status = ContractTypeStatus.Active,
				TicketAanmaaktijd = TicketAanmaakTijd.WorkingHours,
				TicketAfhandeltijd = 10,
				Contracts = Contracts,
				TicketAanmaakmanieren = ticketAanmaakmanieren
			};

			ContractType ContractType3 = new ContractType() {
				Id = 3,
				MinimaleDoorlooptijd = ContractDoorlooptijd.ThreeYear,
				Naam = "ContractType3",
				Prijs = new decimal(2700),
				Status = ContractTypeStatus.Inactive,
				TicketAanmaaktijd = TicketAanmaakTijd.AllTime,
				TicketAfhandeltijd = 20,
				TicketAanmaakmanieren = ticketAanmaakmanieren
			};
			ContractTypes = new List<ContractType>() {ContractType1, ContractType2, ContractType3};

			//Contracts
			Contract1 = new Contract() {
				ContractType = ContractType1,
				ContracttypeId = 1,
				Status = ContractStatus.Active,
				Einddatum = DateTime.Today.AddDays(50),
				Klant = Klant1,
				KlantId = 1,
				Nummer = 1,
				Startdatum = DateTime.Today,
				Tickets = TicketsBijContract1,
			};
			Contract Contract2 = new Contract() {
				ContractType = ContractType2,
				ContracttypeId = 2,
				Status = ContractStatus.Active,
				Einddatum = DateTime.Today.AddDays(50),
				Klant = Klant1,
				KlantId = 1,
				Nummer = 2,
				Startdatum = DateTime.Today,
				Tickets = TicketsBijContract2,
			};
			Contract Contract3 = new Contract()
			{
				ContractType = ContractType3,
				ContracttypeId = 3,
				Status = ContractStatus.Cancelled,
				Einddatum = DateTime.Today.AddDays(50),
				Klant = Klant1,
				KlantId = 1,
				Nummer = 3,
				Startdatum = DateTime.Today,
				Tickets = TicketsBijContract3,
			};
			Contracts = new List<Contract>() { Contract1, Contract2, Contract3 };
			ContractsNietActief = new List<Contract>() { Contract3 };

			//ticket
			Ticket1 = new Ticket() {
				BekekenDoorTechnieker = false,
				Bijlages = ticketBijlages,
				Comments = comments,
				Contract = Contract1,
				ContractNummer = 1,
				DatumAanmaak = DateTime.Now,
				DatumAfgehandeld = DateTime.Today.AddDays(5),
				Dienst = Dienst.Admin,
				Id = 1,
				Omschrijving = "Eerste test ticket",
				Status = TicketStatus.AwaitingCustomerInformation,
				Titel = "Test",
				ToegewezenTechnieker = Werknemer,
				ToegewezenTechniekerId = 2,
				Urgency = TicketUrgency.NoProductionImpact
			};

			TicketFinished = new Ticket
			{
				BekekenDoorTechnieker = false,
				Bijlages = ticketBijlages,
				Comments = comments,
				Contract = Contract1,
				ContractNummer = 1,
				DatumAanmaak = DateTime.Now,
				DatumAfgehandeld = DateTime.Today.AddDays(5),
				Dienst = Dienst.Admin,
				Id = 5,
				Omschrijving = "Eerste test ticket",
				Status = TicketStatus.Finished,
				Titel = "Test",
				ToegewezenTechnieker = Werknemer,
				ToegewezenTechniekerId = 2,
				Urgency = TicketUrgency.NoProductionImpact
			};

			TicketCanceled = new Ticket
			{
				BekekenDoorTechnieker = false,
				Bijlages = ticketBijlages,
				Comments = comments,
				Contract = Contract1,
				ContractNummer = 1,
				DatumAanmaak = DateTime.Now,
				DatumAfgehandeld = DateTime.Today.AddDays(5),
				Dienst = Dienst.Admin,
				Id = 5,
				Omschrijving = "Eerste test ticket",
				Status = TicketStatus.Cancelled,
				Titel = "Test",
				ToegewezenTechnieker = Werknemer,
				ToegewezenTechniekerId = 2,
				Urgency = TicketUrgency.NoProductionImpact
			};

			Ticket Ticket2 = new Ticket() {
				BekekenDoorTechnieker = true,
				Bijlages = ticketBijlages,
				Comments = comments,
				Contract = Contract1,
				ContractNummer = 1,
				DatumAanmaak = DateTime.Now,
				DatumAfgehandeld = DateTime.Today.AddDays(5),
				Dienst = Dienst.Admin,
				Id = 2,
				Omschrijving = "Tweede Test Ticket",
				Status = TicketStatus.ReceivedCustomerInformation,
				Titel = "Test2",
				ToegewezenTechnieker = Werknemer,
				ToegewezenTechniekerId = 2,
				Urgency = TicketUrgency.NoProductionImpact
			};
			
			Ticket Ticket3 = new Ticket() {
				BekekenDoorTechnieker = true,
				Bijlages = ticketBijlages,
				Comments = comments,
				Contract = Contract2,
				ContractNummer = 2,
				DatumAanmaak = DateTime.Now,
				DatumAfgehandeld = DateTime.Today.AddDays(5),
				Dienst = Dienst.Admin,
				Id = 3,
				Omschrijving = "Tweede Test Ticket",
				Status = TicketStatus.Finished,
				Titel = "Test2",
				ToegewezenTechnieker = Werknemer,
				ToegewezenTechniekerId = 2,
				Urgency = TicketUrgency.NoProductionImpact
			};
			TicketsBijContract1.Add(Ticket1);
			TicketsBijContract1.Add(Ticket2);
			TicketsBijContract2.Add(Ticket3);

			//ContactPersonen
			ContactPersonen = new List<Contactpersoon>();
			Contactpersoon contactPersoon = new Contactpersoon() { Email = "contact@hotmail.com", Klant = Klant1, KlantId = 1, Naam = "Ye", Voornaam = "Elly" };
			ContactPersonen.Add(contactPersoon);

			//Werknemers
			Werknemer = new Werknemer() {
				Naam = "Werknemer1",
				Land = "BE",
				Id = 2,
				Busnr = "a",
				DatumInDienstTreding = DateTime.Today.AddDays(-5),
				Dienst = Dienst.Admin,
				Email = "Werknemer@test.be",
				Gebruikersnaam = "werknemerGebruiker",
				Huisnummer = 4,
				Postcode = "9760",
				Status = GebruikersStatus.Active,
				Straat = "Polderstraat",
				Tickets = TicketsBijContract1,
				Voornaam = "werk",
				Wachtwoord = "Wachtwoord",
				Werknemerstype = WerknemersType.Administrator,
				Woonplaats = "Ninove",
			};

			Klant1 = new Klant() {
				Id = 1,
				Contracten = Contracts,
				Bedrijfsnaam = "Lotus",
				Busnr = "a",
				Contactpersonen = ContactPersonen,
				Datumklantgeworden = DateTime.Today.AddDays(-5),
				Gebruikersnaam = "LotusLover",
				Huisnummer = 44,
				Land = "BE",
				Postcode = "9750",
				Status = GebruikersStatus.Active,
				Straat = "Veldstraat",
				Wachtwoord = "TestWachtwoord1",
				Woonplaats = "Oost-Vlaanderen"
			};

			KlantZonderActiefContract = new Klant()
			{
				Id = 1,
				Contracten = ContractsNietActief,
				Bedrijfsnaam = "Lotus",
				Busnr = "a",
				Contactpersonen = ContactPersonen,
				Datumklantgeworden = DateTime.Today.AddDays(-5),
				Gebruikersnaam = "LotusLover",
				Huisnummer = 44,
				Land = "BE",
				Postcode = "9750",
				Status = GebruikersStatus.Active,
				Straat = "Veldstraat",
				Wachtwoord = "TestWachtwoord1",
				Woonplaats = "Oost-Vlaanderen"
			};

		}

	}

}
