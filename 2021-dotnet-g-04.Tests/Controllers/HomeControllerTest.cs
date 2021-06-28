using _2021_dotnet_g_04.Controllers;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using _2021_dotnet_g_04.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly DummyDbContext _dummyContext;
        private readonly HomeController _homeController;
        private readonly Klant _klantMetAlles;
        private readonly Klant _klantMetNiets;
        private readonly ContractType _contractType1Jaar;
        private readonly ContractType _contractType2Jaar;

        public HomeControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _homeController = new HomeController();
            _klantMetAlles = _dummyContext.Klant1;
            _klantMetNiets = _dummyContext.KlantZonderActiefContract;
            _contractType1Jaar = _dummyContext.ContractType1;
            _contractType2Jaar = _dummyContext.ContractType2;

            _klantMetAlles.GetContractBy(2).AddTicket(new Ticket()
            {
                BekekenDoorTechnieker = true,
                Bijlages = new List<TicketBijlage>(),
                Comments = new List<Comment>(),
                Contract = _klantMetAlles.GetContractBy(2),
                ContractNummer = 2,
                DatumAanmaak = DateTime.Today.AddDays(-7),
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

            Contract contract1 = new Contract
            {
                Startdatum = DateTime.Today.AddYears(-1).AddDays(5),
                ContractType = _contractType1Jaar,
                Einddatum = DateTime.Today.AddDays(5),
                Status = ContractStatus.Active
            };
            Contract contract2 = new Contract
            {
                Startdatum = DateTime.Today.AddYears(-2).AddDays(25),
                ContractType = _contractType2Jaar,
                Einddatum = DateTime.Today.AddDays(25),
                Status = ContractStatus.Active
            };

            _klantMetAlles.Contracten.Add(contract1);
            _klantMetAlles.Contracten.Add(contract2);
        }

        #region == Index ==
        [Fact]
        public void Index_ShowsAllContractsExpiringIn30DaysOrLess()
        {
            var result = Assert.IsType<ViewResult>(_homeController.Index(_klantMetAlles));
            var model = Assert.IsType<HomeViewModel>(result.Model);
            Assert.Equal(2, model.ContractsExpire30.Count());
            Assert.True((bool) result.ViewData["ContractsExpire30"]);
        }

        [Fact]
        public void Index_ShowsAllTicketsWithStatusAwaitingCustomerInformation()
        {
            var result = Assert.IsType<ViewResult>(_homeController.Index(_klantMetAlles));
            var model = Assert.IsType<HomeViewModel>(result.Model);
            Assert.Single(model.TicketsWaitingCustomerInfo);
            Assert.True((bool) result.ViewData["TicketsWaitingCustomerInfo"]);
        }


        [Fact]
        public void Index_ShowsAllTicketsWithStatusFinishedAndNotViewedByCustomer()
        {
            var result = Assert.IsType<ViewResult>(_homeController.Index(_klantMetAlles));
            var model = Assert.IsType<HomeViewModel>(result.Model);
            Assert.Equal(2, model.TicketsRecentlyFinished.Count());
            Assert.True((bool) result.ViewData["TicketsRecentlyFinished"]);
        }

        [Fact]
        public void Index_ShowsMessage_NoTicketsOrContracts()
        {
            var result = Assert.IsType<ViewResult>(_homeController.Index(_klantMetNiets));
            var model = Assert.IsType<HomeViewModel>(result.Model);

            Assert.Empty(model.ContractsExpire30);
            Assert.Empty(model.TicketsWaitingCustomerInfo);
            Assert.Empty(model.TicketsRecentlyFinished);
            Assert.False((bool) result.ViewData["ContractsExpire30"]);
            Assert.False((bool) result.ViewData["TicketsWaitingCustomerInfo"]);
            Assert.False((bool) result.ViewData["TicketsRecentlyFinished"]);
        }
        #endregion
    }
}
