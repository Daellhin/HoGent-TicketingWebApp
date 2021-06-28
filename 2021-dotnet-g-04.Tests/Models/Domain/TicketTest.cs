using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using _2021_dotnet_g_04.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Models.Domain
{
    public class TicketTest
    {
        private readonly DummyDbContext _context;
        private readonly Ticket ticketCanceled;
        private readonly Ticket ticketFinished;
        private readonly Ticket ticketopen;
        private readonly Contract contract;
        private readonly Werknemer werknemer;
        private readonly ContractType contractType;
        private List<TicketBijlage> ticketBijlages;

        public TicketTest()
        {
            _context = new DummyDbContext();
            ticketCanceled = _context.TicketCanceled;
            ticketFinished = _context.TicketFinished;
            ticketopen = _context.Ticket1;
            contract = _context.Contract1;
            werknemer = _context.Werknemer;
            contractType = _context.ContractType1;
            TicketBijlage ticketBijlage = new TicketBijlage("Bijlage");
            ticketBijlages = new List<TicketBijlage>();
            ticketBijlages.Add(ticketBijlage);
        }

        #region IsOpen
        [Fact]
        public void IsOpen_OpenTicket_retourneertTrue()
        {
            Assert.True(ticketopen.IsOpen());
        }

        [Fact]
        public void IsOpen_FinishedTicket_retourneertFalse()
        {
            Assert.False(ticketFinished.IsOpen());
        }

        [Fact]
        public void IsOpen_CancelledTicket_retourneertFalse()
        {
            Assert.False(ticketCanceled.IsOpen());
        }
        #endregion

        #region editTicket
        [Fact]
        public void editTicket_geldigeGegevens_voertVeranderingenDoor()
        {
            ticketopen.EditTicket(TicketUrgency.ProductionWillBeImpacted, "Nieuwe comment", ticketBijlages, werknemer.Gebruikersnaam);
            Assert.Equal(TicketUrgency.ProductionWillBeImpacted, ticketopen.Urgency);
            Assert.Equal("Nieuwe comment", ticketopen.Comments.LastOrDefault().Opmerking);
            Assert.Equal(werknemer.Gebruikersnaam, ticketopen.Comments.LastOrDefault().PersoonDieOpmerkingToevoegt);
            Assert.Equal("Bijlage", ticketopen.Bijlages.LastOrDefault().Bijlage);
        }

        [Fact]
        public void editTicket_geslotenTicket_werptException()
        {
            Assert.Throws<ArgumentException>(() => ticketCanceled.EditTicket(TicketUrgency.ProductionWillBeImpacted, "Nieuwe comment", ticketBijlages, werknemer.Gebruikersnaam));
        }
        #endregion

        #region cancelTicket
        [Fact]
        public void cancelTicket_openTicket_setStatusEnDatumAfgehandeld()
        {
            ticketopen.CancelTicket();
            Assert.Equal(TicketStatus.Cancelled, ticketopen.Status);
            Assert.Equal(DateTime.Today.Day, ticketopen.DatumAfgehandeld.Value.Day);
        }

        [Fact]
        public void CancelTicekt_CancelledTicket_werptException()
        {
            Assert.Throws<ArgumentException>(() => ticketCanceled.CancelTicket());
        }
        #endregion
    }
}
