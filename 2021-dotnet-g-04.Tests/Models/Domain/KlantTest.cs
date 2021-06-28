using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using _2021_dotnet_g_04.Tests.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Models.Domain {
    public class KlantTest {
        private readonly DummyDbContext _context;
        private readonly Klant klant;
        private readonly Ticket ticket;
        private readonly Contract contract;
        private readonly Werknemer werknemer;
        private readonly ContractType contractType;

        public KlantTest() {
            _context = new DummyDbContext();
            klant = _context.Klant1;
            ticket = _context.Ticket1;
            contract = _context.Contract1;
            werknemer = _context.Werknemer;
            contractType = _context.ContractType1;
        }

        [Fact]
        public void GetOpenTickets_KlantMetOpenTicketten_RetourneertAlleOpenTicketten() {
            Assert.IsAssignableFrom<IEnumerable>(klant.GetOpenTickets());
            Assert.Equal(2, klant.GetOpenTickets().Count());
        }

        [Fact]
        public void GetClosedTickets_KlantMetGeslotenTicketten_RetourneertAlleGeslotenTicketten() {
            Assert.IsAssignableFrom<IEnumerable>(klant.GetClosedTickets());
            Assert.Single(klant.GetClosedTickets());
        }

        [Fact]
        public void GetOpenContracts_KlantMetOpenContracten_RetourneertAlleOpenContracts() {
            Assert.Equal(2, klant.GetOpenContracts().Count());
        }

        [Fact]
        public void GetClosedContracts_KlantMetGeslotenContracten_RetourneertAlleGeslotenContracts() {
            Assert.Single(klant.GetClosedContracts());
        }

        [Fact]
        public void GetTicket_KlantMetEenGeldigTicketId_RetourneertHetJuisteTicket() {
            Assert.Equal(ticket, klant.GetTicketBy(1));
        }

        [Fact]
        public void GetTicket_KlantMetOngeldigTicketId_RetourneertNull() {
            Assert.Null(klant.GetTicketBy(5));
        }

        [Fact]
        public void GetContract_KlantMetEenGeldigContractId_RetourneertHetJuisteContract() {
            Assert.Equal(contract, klant.GetContractBy(1));
        }

        [Fact]
        public void GetContract_KlantMetOngeldigContractId_RetourneertNull() {
            Assert.Null(klant.GetContractBy(5));
        }

        [Fact]
        public void CreateTicket_GeldigeParameters_MaaktTicketAanEnVoegtToeAanContract() {
            klant.CreateTicket("TestTitel", TicketUrgency.NoProductionImpact, 1, Dienst.Finance, "TestOmschrijving", new List<BijlageViewModel>(), werknemer);
           
            Assert.Equal(3, klant.GetOpenTickets().Count());
        }

        [Fact]
        public void EditTicket_GeldigeParameters_PastTicketAan() {
            klant.EditTicket(1, TicketUrgency.ProductionWillBeImpacted, "nieuwe Comment", new List<TicketBijlage>());
            
            Assert.Equal(2, klant.GetOpenTickets().Count());
            Assert.Equal(TicketUrgency.ProductionWillBeImpacted, ticket.Urgency);
            Assert.Equal("nieuwe Comment", ticket.Comments.Last().Opmerking);
            Assert.Equal(new List<TicketBijlage>(), ticket.Bijlages);
        }

        [Fact]
        public void CreateContract_OverlappendeStartDatum_WerptArgumentException() {
            DateTime startDatum = DateTime.Now;

            Assert.Throws<ArgumentException>(() => klant.CreateContract(startDatum, contractType));
        }

        [Fact]
        public void CreateContract_GeldigeParameters_MaaktContractAanEnVoegtToeAanLijstContracten() {
            DateTime startDatum = DateTime.Now.AddYears(1);

            klant.CreateContract(startDatum, contractType);

            Assert.Equal(4, klant.Contracten.Count());
            Assert.Equal(contractType, klant.Contracten.Last().ContractType);
            Assert.Equal(startDatum, klant.Contracten.Last().Startdatum);
        }
    }
}
