using _2021_dotnet_g_04.Controllers;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using _2021_dotnet_g_04.Models.ViewModels;
using _2021_dotnet_g_04.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Controllers
{
    public class TicketControllerTest
    {
        private readonly DummyDbContext _context;
        private readonly TicketController controller;
        private readonly TicketEditViewModel editModel;
        private readonly TicketCreateViewModel createModel;
        private readonly List<Comment> comments = new List<Comment>();
        private readonly List<TicketBijlage> bijlages = new List<TicketBijlage>();
        private readonly Mock<IWerknemerRepository> mockWerknemerRepository;
        private readonly Mock<ITicketRepository> mockTicketRepo;
        private readonly Mock<IContractTypeRepository> mockContractTypeRepo;

        public TicketControllerTest()
        {
            _context = new DummyDbContext();
            mockWerknemerRepository = new Mock<IWerknemerRepository>();
            mockTicketRepo = new Mock<ITicketRepository>();
            mockContractTypeRepo = new Mock<IContractTypeRepository>();
            controller = new TicketController(mockWerknemerRepository.Object, mockTicketRepo.Object, mockContractTypeRepo.Object);
            controller.TempData = new Mock<ITempDataDictionary>().Object;

            comments.Add(new Comment() { Id = 1 });
            editModel = new TicketEditViewModel(new Ticket()
            {
                BekekenDoorTechnieker = false,
                Comments = comments,
                ContractNummer = 1,
                DatumAanmaak = DateTime.Now,
                DatumAfgehandeld = DateTime.Today.AddDays(5),
                Dienst = Dienst.Admin,
                Id = 1,
                Omschrijving = "Edited Omschrijving",
                Status = TicketStatus.AwaitingCustomerInformation,
                Titel = "editTicket",
                ToegewezenTechniekerId = 2,
                Urgency = TicketUrgency.ProductionImpacted
            });

            createModel = new TicketCreateViewModel()
            {
                Omschrijving = "Dit is een proef ticket",
                Titel = "Test ticket",
                Urgency = TicketUrgency.NoProductionImpact,
                ContractNummer = 1,
                Dienst = Dienst.Admin
            };
        }

        [Fact]
        public void Index_GeeftViewResult_EnGeeftCorrecteViewDataMeeAanView()
        {
            Klant klant = _context.Klant1;

            var viewResult = Assert.IsType<ViewResult>(controller.Index(klant));
            Assert.Equal(true, viewResult.ViewData["OpenTickets"]);
            Assert.Equal("OpenTickets", viewResult.ViewData["activeLink"]);
        }

        [Fact]
        public void ClosedTickets_GeeftViewResult_enGeeftCorrecteViewDataMeeAanView()
        {
            Klant klant = _context.Klant1;

            var result = Assert.IsType<ViewResult>(controller.ClosedTickets(klant));
            Assert.Equal(false, result.ViewData["OpenTickets"]);
            Assert.Equal("ClosedTickets", result.ViewData["activeLink"]);
        }

        [Fact]
        public void EditGet_GeeftTicketViewModelDoorAanView()
        {
            mockTicketRepo.Setup(repo => repo.GetBy(1)).Returns(_context.Ticket1);
            Klant klant = _context.Klant1;
            var actionResult = Assert.IsType<ViewResult>(controller.Edit(1, klant));
            Assert.IsType<SelectList>(actionResult.ViewData["Contracts"]);
            Assert.IsType<TicketEditViewModel>(actionResult.Model);
            Assert.Equal("OpenTickets", actionResult.ViewData["activeLink"]);
        }

        [Fact]
        public void EditPost_geldigeModelState_VoertVeranderingenDoorEnPersisteert()
        {
            Klant klant = _context.Klant1;
            Ticket tickettest = klant.GetTicketBy(1);
            tickettest.Urgency = TicketUrgency.NoProductionImpact;
            var result = Assert.IsType<RedirectToActionResult>(controller.Edit(1, editModel, klant));

            Assert.Equal(TicketUrgency.ProductionImpacted, tickettest.Urgency);

            mockTicketRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditPost_OngeldigeModelState_GeeftDefaultView()
        {
            Klant klant = _context.Klant1;
            controller.ModelState.AddModelError("any key", "any key");
            ViewResult result = Assert.IsType<ViewResult>(controller.Edit(1, editModel, klant));

            Assert.IsType<SelectList>(result.ViewData["Contracts"]);
            Assert.Equal("OpenTickets", result.ViewData["activeLink"]);
            Assert.Equal(editModel, result.Model);
        }

        [Fact]
        public void CreateGet_GeeftViewDataDoorAanView_EnGeeftViewDoorMetCreateModel()
        {
            Klant klant = _context.Klant1;
            var actionResult = Assert.IsType<ViewResult>(controller.Create(klant));
            Assert.IsType<SelectList>(actionResult.ViewData["Contracts"]);
            Assert.Equal("CreateTicket", actionResult.ViewData["activeLink"]);
            Assert.IsType<TicketCreateViewModel>(actionResult.Model);
        }

        [Fact]
        public void CreateGet_KlantZonderActiefContract_ReturntRedirectToActionEnTempData()
        {
            Klant klant = _context.KlantZonderActiefContract;
            Assert.IsType<RedirectToActionResult>(controller.Create(klant));
            Assert.NotNull(controller.TempData);
        }

        [Fact]
        public void CreatePost_GeldigeGegevens_PersisteerdEnReturnedRedirectToAction()
        {
            Klant klant = _context.Klant1;
            mockWerknemerRepository.Setup(repo => repo.GetRandomTechnician()).Returns(_context.Werknemer);
            Assert.IsType<RedirectToActionResult>(controller.Create(createModel, klant));
            mockTicketRepo.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CancelGet_GeeftViewData_EnViewDoor()
        {
            mockTicketRepo.Setup(repo => repo.GetBy(1)).Returns(_context.Ticket1);
            controller.Cancel(_context.Ticket1.Id);
            Assert.Equal(TicketStatus.Cancelled, _context.Ticket1.Status);
            Assert.Equal(DateTime.Today.Day, _context.Ticket1.DatumAfgehandeld.Value.Day);
            mockTicketRepo.Verify(repo => repo.SaveChanges(), Times.Once);

        }
    }
}