using _2021_dotnet_g_04.Controllers;
using _2021_dotnet_g_04.Extensions;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using _2021_dotnet_g_04.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Controllers {
	public class StatistiekControllerTest {
		private readonly DummyDbContext _dummyContext;
		private readonly StatistiekController _statistiekController;
		private readonly Klant _klant;

		public StatistiekControllerTest() {
			_dummyContext = new DummyDbContext();
			_statistiekController = new StatistiekController();
			_klant = _dummyContext.Klant1;

			_klant.GetContractBy(2).AddTicket(new Ticket() {
				BekekenDoorTechnieker = true,
				Bijlages = new List<TicketBijlage>(),
				Comments = new List<Comment>(),
				Contract = _klant.GetContractBy(2),
				ContractNummer = 2,
				DatumAanmaak = DateTime.Today.AddDays(-10),
				DatumAfgehandeld = DateTime.Today.AddDays(-5),
				Dienst = Dienst.Admin,
				Id = 4,
				Omschrijving = "Tweede Test Ticket",
				Status = TicketStatus.Finished,
				Titel = "Test2",
				ToegewezenTechnieker = null,
				ToegewezenTechniekerId = 7,
				Urgency = TicketUrgency.NoProductionImpact
			});

			_klant.GetContractBy(2).AddTicket(new Ticket() {
				BekekenDoorTechnieker = true,
				Bijlages = new List<TicketBijlage>(),
				Comments = new List<Comment>(),
				Contract = _klant.GetContractBy(2),
				ContractNummer = 2,
				DatumAanmaak = DateTime.Today.AddDays(-7),
				DatumAfgehandeld = DateTime.Today.AddDays(-5),
				Dienst = Dienst.Admin,
				Id = 5,
				Omschrijving = "Derde Test Ticket",
				Status = TicketStatus.Finished,
				Titel = "Test2",
				ToegewezenTechnieker = null,
				ToegewezenTechniekerId = 7,
				Urgency = TicketUrgency.NoProductionImpact
			});

			_klant.GetContractBy(2).AddTicket(new Ticket() {
				BekekenDoorTechnieker = true,
				Bijlages = new List<TicketBijlage>(),
				Comments = new List<Comment>(),
				Contract = _klant.GetContractBy(2),
				ContractNummer = 2,
				DatumAanmaak = DateTime.Today.AddDays(-57),
				DatumAfgehandeld = DateTime.Today.AddDays(-5),
				Dienst = Dienst.Admin,
				Id = 5,
				Omschrijving = "Vierde Test Ticket",
				Status = TicketStatus.Finished,
				Titel = "Test2",
				ToegewezenTechnieker = null,
				ToegewezenTechniekerId = 7,
				Urgency = TicketUrgency.NoProductionImpact
			});

			Contract contract1 = new Contract {
				Startdatum = DateTime.Today.AddYears(-1).AddDays(5),
				ContractType = _dummyContext.ContractType1,
				Einddatum = DateTime.Today.AddDays(5),
				Status = ContractStatus.Active
			};
			Contract contract2 = new Contract {
				Startdatum = DateTime.Today.AddYears(-2).AddDays(25),
				ContractType = _dummyContext.ContractType2,
				Einddatum = DateTime.Today.AddDays(25),
				Status = ContractStatus.Active
			};

			_klant.Contracten.Add(contract1);
			_klant.Contracten.Add(contract2);
		}


		#region == Index ==
		[Fact]
		public void Index_ShowExpectedResults() {
			Dictionary<string, int> expectedDictionary0 = new Dictionary<string, int> { ["Finished Late"] = 1, ["Finished On Time"] = 3 };
			DateTime d1 = DateTime.Today.AddDays(-57).StartOfWeekMonday();
			DateTime d2 = DateTime.Today.AddDays(-10).StartOfWeekMonday();
			DateTime d3 = DateTime.Today.AddDays(-7).StartOfWeekMonday();
			DateTime d4 = DateTime.Today.StartOfWeekMonday();

			Dictionary<string, int> expectedDictionary1;
			if (d2.Date == d3.Date) {
				expectedDictionary1 = new Dictionary<string, int> { [d1.ToShortDateString()] = 1, [d2.ToShortDateString()] = 2, [d4.ToShortDateString()] = 3 };
			}
			else {
				expectedDictionary1 = new Dictionary<string, int> { [d1.ToShortDateString()] = 1, [d2.ToShortDateString()] = 1, [d3.ToShortDateString()] = 1, [d4.ToShortDateString()] = 3 };
			}

			var result = Assert.IsType<ViewResult>(_statistiekController.Index(_klant));
			List<ChartViewModel> model = Assert.IsType<List<ChartViewModel>>(result.Model);

			Assert.Equal(expectedDictionary0, model[0].Data);
			Assert.Equal(expectedDictionary1, model[1].Data);
		}

		#endregion
	}
}
