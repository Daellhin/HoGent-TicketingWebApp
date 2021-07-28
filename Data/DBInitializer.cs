using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data {
	public class DBInitializer {
		private readonly ApplicationDbContext _context;

		public DBInitializer(ApplicationDbContext context) {
			_context = context;
		}

		public void MigrateDatabase() {
			_context.Database.Migrate();
			Console.WriteLine("Database migrated");
		}

		public void Run() {
			Console.WriteLine($"DataSource: {_context.Database.GetDbConnection().DataSource}");
			Console.WriteLine($"Database: {_context.Database.GetDbConnection().Database}");

			_context.Database.EnsureDeleted();
			Console.WriteLine("Database deleted");

			if (_context.Database.EnsureCreated()) {
				Console.WriteLine("Database created");
				AddData();
				_context.SaveChanges();
				Console.WriteLine("Database seeded");
			}else {
				throw new ArgumentException("Database is already created");
			}
		}

		/// <summary>
		/// Naamgeving: 
		/// 1. naam eigenaar (bv: lotus)
		/// 2. naam object type (bv: Klant) 
		/// 3. extra informatie(bv: StandardThreeYear)
		/// Een eigenaar start met zijn eigen naam
		/// Als object tot meerdere eigenaars kan behoren(bv: bij een contractType) dan naam eigenaar weglaten
		/// </summary>
		private void AddData() {
			// Werknemers
			Werknemer klaasTechniekerAdmin = new Werknemer() {
				Naam = "Van Malderen",
				Voornaam = "Klaas",
				Land = "BE",
				DatumInDienstTreding = DateTime.Today.AddDays(-100),
				Dienst = Dienst.Admin,
				Email = "klaas.vm@actemium.be",
				Gebruikersnaam = "KlaasVM",
				Huisnummer = 4,
				Postcode = "9760",
				Status = GebruikersStatus.Active,
				Straat = "Polderstraat",
				Wachtwoord = "Wachtwoord",
				Werknemerstype = WerknemersType.Technician,
				Woonplaats = "Ninove",
			};
			_context.Add(klaasTechniekerAdmin);

			Werknemer julietSupportManagerFinance = new Werknemer() {
				Naam = "Stokes",
				Voornaam = "Juliet",
				Land = "BE",
				DatumInDienstTreding = DateTime.Today.AddDays(-300),
				Dienst = Dienst.Finance,
				Email = "juliet.vm@actemium.be",
				Gebruikersnaam = "Juliet Stokes",
				Huisnummer = 4,
				Postcode = "9760",
				Status = GebruikersStatus.Active,
				Straat = "Polderstraat",
				Wachtwoord = "Wachtwoord",
				Werknemerstype = WerknemersType.SupportManager,
				Woonplaats = "Ninove",
			};
			_context.Add(julietSupportManagerFinance);

			// TicketAanmaakmanieren
			List<ContractTypeTicketAanmaakmanier> ticketAanmaakmanierStandard = new List<ContractTypeTicketAanmaakmanier> {
				new ContractTypeTicketAanmaakmanier(TicketAanmaakManier.Application),
				new ContractTypeTicketAanmaakmanier(TicketAanmaakManier.Email)
			};
			List<ContractTypeTicketAanmaakmanier> ticketAanmaakmanierPremium = new List<ContractTypeTicketAanmaakmanier> {
				new ContractTypeTicketAanmaakmanier(TicketAanmaakManier.Application),
				new ContractTypeTicketAanmaakmanier(TicketAanmaakManier.Email),
				new ContractTypeTicketAanmaakmanier(TicketAanmaakManier.Telephone)
			};

			// ContractTypes
			ContractType contractTypeStandardOneYear = new ContractType() {
				MinimaleDoorlooptijd = ContractDoorlooptijd.OneYear,
				Naam = "Standard Service - 1 Year",
				Prijs = new decimal(3600),
				Status = ContractTypeStatus.Active,
				TicketAanmaaktijd = TicketAanmaakTijd.WorkingHours,
				TicketAfhandeltijd = 5,
				TicketAanmaakmanieren = ticketAanmaakmanierStandard
			};
			_context.Add(contractTypeStandardOneYear);

			ContractType contractTypeStandardThreeYear = new ContractType() {
				MinimaleDoorlooptijd = ContractDoorlooptijd.ThreeYear,
				Naam = "Standard Service - 3 Year",
				Prijs = new decimal(9000),
				Status = ContractTypeStatus.Active,
				TicketAanmaaktijd = TicketAanmaakTijd.WorkingHours,
				TicketAfhandeltijd = 5,
				TicketAanmaakmanieren = ticketAanmaakmanierStandard
			};
			_context.Add(contractTypeStandardThreeYear);

			ContractType contractTypePremiumOneYear = new ContractType() {
				MinimaleDoorlooptijd = ContractDoorlooptijd.OneYear,
				Naam = "Standard Service - 3 Year",
				Prijs = new decimal(5000),
				Status = ContractTypeStatus.Active,
				TicketAanmaaktijd = TicketAanmaakTijd.AllTime,
				TicketAfhandeltijd = 3,
				TicketAanmaakmanieren = ticketAanmaakmanierPremium
			};
			_context.Add(contractTypePremiumOneYear);

			// Lotus: Klant
			Klant lotusKlant = new Klant() {
				Bedrijfsnaam = "Lotus",
				Busnr = "a",
				Datumklantgeworden = DateTime.Today.AddDays(-50),
				Gebruikersnaam = "LotusKoekjes",
				Huisnummer = 44,
				Land = "BE",
				Postcode = "9750",
				Status = GebruikersStatus.Active,
				Straat = "Veldstraat",
				Wachtwoord = "Wachtwoord",
				Woonplaats = "Oost-Vlaanderen"
			};
			_context.Add(lotusKlant);

			// Lotus: Contactpersonen
			Contactpersoon lotusContactEmiel = new Contactpersoon() {
				Email = "emiel.boon@lotus.com",
				Naam = "Boon",
				Voornaam = "Emiel"
			};
			lotusKlant.AddContactpersoon(lotusContactEmiel);

			Contactpersoon lotusContactEva = new Contactpersoon() {
				Email = "eva.helsen@lotus.com",
				Naam = "Helsen",
				Voornaam = "Eva"
			};
			lotusKlant.AddContactpersoon(lotusContactEva);

			// Lotus: Contracten
			Contract lotusContractPremiumOneYear = new Contract() {
				ContractType = contractTypePremiumOneYear,
				Status = ContractStatus.Active,
				Einddatum = DateTime.Today.AddDays(320),
				Startdatum = DateTime.Today.AddDays(-40),
			};
			lotusKlant.AddContract(lotusContractPremiumOneYear);

			// Lotus: Tickets
			// Ticket 1: lotusTicketFinishedRetiredPermision
			Ticket lotusTicketFinishedRetiredPermision = new Ticket() {
				Status = TicketStatus.Finished,
				BekekenDoorTechnieker = true,
				BekekenDoorKlant = false,
				DatumAanmaak = DateTime.Today.AddDays(-7),
				DatumAfgehandeld = DateTime.Today.AddDays(-5),
				Titel = "No permission to mark someone as retired",
				Omschrijving = "I do not have an option to mark people as retired within the ticketingsystem app. How do I do this?",
				Dienst = Dienst.IT,
				Urgency = TicketUrgency.NoProductionImpact
			};
			lotusContractPremiumOneYear.AddTicket(lotusTicketFinishedRetiredPermision);
			klaasTechniekerAdmin.AddTicket(lotusTicketFinishedRetiredPermision);

			Comment lotusTicketFinishedRetiredPermisionCommentTechnieker = new Comment() {
				Opmerking = "You have to mark them as non-active and only admins can do this action.",
				PersoonDieOpmerkingToevoegt = "KlaasVM",
				Tijdstip = DateTime.Now.AddDays(-6)
			};
			lotusTicketFinishedRetiredPermision.AddComment(lotusTicketFinishedRetiredPermisionCommentTechnieker);

			// Ticket 2: lotusTicketCreatedPrinterExcel
			Ticket lotusTicketCreatedPrinterExcel = new Ticket() {
				Status = TicketStatus.Created,
				BekekenDoorTechnieker = false,
				BekekenDoorKlant = false,
				DatumAanmaak = DateTime.Today.AddDays(-2),
				Titel = "Printer doenst work with Excel",
				Omschrijving = "When I try to print my Excel sheets, the printer just doesn't react. Is there anything i can do about this?",
				Dienst = Dienst.Finance,
				Urgency = TicketUrgency.NoProductionImpact
			};
			lotusContractPremiumOneYear.AddTicket(lotusTicketCreatedPrinterExcel);
			klaasTechniekerAdmin.AddTicket(lotusTicketCreatedPrinterExcel);

			// Ticket 3: lotusTicketInDevelopementWordError
			Ticket lotusTicketAwaitingCustInfoWordError = new Ticket() {
				Status = TicketStatus.AwaitingCustomerInformation,
				BekekenDoorTechnieker = true,
				BekekenDoorKlant = false,
				DatumAanmaak = DateTime.Today.AddDays(-1),
				Titel = "Error while trying to save a Word document",
				Omschrijving = "Every time i try to save a Word document it says 'A File Error Has Occurred' How can I fix this? I tried finding a solution on the site I added as an attachment but without success.",
				Dienst = Dienst.Finance,
				Urgency = TicketUrgency.NoProductionImpact
			};
			lotusContractPremiumOneYear.AddTicket(lotusTicketAwaitingCustInfoWordError);
			julietSupportManagerFinance.AddTicket(lotusTicketAwaitingCustInfoWordError);

			Comment lotusTicketAwaitingCustInfoWordErrorCommentTechnieker = new Comment() {
				Opmerking = "What is your Word version? Are you using Microsoft Office 365?",
				PersoonDieOpmerkingToevoegt = "Juliet Stokes",
				Tijdstip = DateTime.Now.AddDays(0)
			};
			lotusTicketAwaitingCustInfoWordError.AddComment(lotusTicketAwaitingCustInfoWordErrorCommentTechnieker);

			// Ticket 4: lotusTicketAwaitingCustInfoAttachements
			Ticket lotusTicketAwaitingCustInfoAttachements = new Ticket() {
				Status = TicketStatus.AwaitingCustomerInformation,
				BekekenDoorTechnieker = true,
				BekekenDoorKlant = false,
				DatumAanmaak = DateTime.Today.AddDays(-1),
				Titel = "Attachments are invisible on old tickets",
				Omschrijving = "I know that my previous tickets have had attachments, but they're all gone now, same with the comments! Open tickets do still show comments and attachments. Send help!",
				Dienst = Dienst.Finance,
				Urgency = TicketUrgency.NoProductionImpact
			};
			lotusContractPremiumOneYear.AddTicket(lotusTicketAwaitingCustInfoAttachements);
			klaasTechniekerAdmin.AddTicket(lotusTicketAwaitingCustInfoAttachements);

			Comment lotusTicketAwaitingCustInfoAttachementsCommentTechnieker = new Comment() {
				Opmerking = "This was a known bug, our programing department has now fixed this bug. If this problem continues be shure to contact me.",
				PersoonDieOpmerkingToevoegt = "KlaasVM",
				Tijdstip = DateTime.Now.AddDays(0)
			};
			lotusTicketAwaitingCustInfoAttachements.AddComment(lotusTicketAwaitingCustInfoAttachementsCommentTechnieker);

			// ticket 5: lotusTicketAwaitingCustInfoDataDeleted
			Ticket lotusTicketAwaitingCustInfoDataDeleted = new Ticket() {
				Status = TicketStatus.AwaitingCustomerInformation,
				BekekenDoorTechnieker = true,
				BekekenDoorKlant = false,
				DatumAanmaak = DateTime.Today.AddDays(0),
				Titel = "All data has disapeared",
				Omschrijving = "All our data(tickets, contracts, statistics, customers, employees) has disapeared. This morning it was stil there but now it has disapeared. Whe have to give a presentation to a customer in 10 minutes.",
				Dienst = Dienst.IT,
				Urgency = TicketUrgency.ProductionImpacted
			};
			lotusContractPremiumOneYear.AddTicket(lotusTicketAwaitingCustInfoDataDeleted);
			klaasTechniekerAdmin.AddTicket(lotusTicketAwaitingCustInfoDataDeleted);

			Comment lotusTicketAwaitingCustInfoDataDeletedCommentTechnieker = new Comment() {
				Opmerking = "We are sorry for the inconvienance, a junior programmer had acidentaly deleted all data in the database by running a old version of the software. When have coppied all data from a backup to the database.",
				PersoonDieOpmerkingToevoegt = "KlaasVM",
				Tijdstip = DateTime.Now.AddDays(0)
			};
			lotusTicketAwaitingCustInfoDataDeleted.AddComment(lotusTicketAwaitingCustInfoDataDeletedCommentTechnieker);

		}

	}
}
