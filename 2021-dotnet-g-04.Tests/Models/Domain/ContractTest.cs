using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Tests.Data;
using System;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Models.Domain {
    public class ContractTest {
        private readonly DummyDbContext _context;
        private readonly Contract _contract;
        private readonly ContractType _inactiefContractType;
        private readonly ContractType _actiefContractType;

        public ContractTest() {
            _context = new DummyDbContext();
            _contract = _context.Contract1;
            _inactiefContractType = _context.ContractTypes[2];
            _actiefContractType = _context.ContractType1;
        }

       [Fact]
        public void NewContract_WrongDate_ThrowsException() {
            Assert.Throws<ArgumentException>(() => new Contract(DateTime.Now.AddDays(-1), _actiefContractType));
        }

        [Fact]
        public void NewContract_InactiveContractType_ThrowsException() {
            Assert.Throws<ArgumentException>(() => new Contract(DateTime.Now.AddDays(1), _inactiefContractType));
        }

        [Fact]
        public void NewContract_CorrecteGegevens_StartVandaag_Doorlooptijd1Jaar_MaaktActiefContractAan() {
            DateTime date = DateTime.Now;
            
            Contract contract = new Contract(date, _actiefContractType);
            
            Assert.Equal(date, contract.Startdatum);
            Assert.Equal(date.AddYears(1), contract.Einddatum);
            Assert.Equal(_actiefContractType, contract.ContractType);
            Assert.Equal(ContractStatus.Active, contract.Status);
        }

        [Fact]
        public void NewContract_CorrecteGegevens_StartLater_Doorlooptijd1Jaar_MaaktPendingContractAan() {
            DateTime date = DateTime.Now.AddDays(5);
            
            Contract contract = new Contract(date, _actiefContractType);
            
            Assert.Equal(date, contract.Startdatum);
            Assert.Equal(date.AddYears(1), contract.Einddatum);
            Assert.Equal(ContractStatus.Pending, contract.Status);
        }

        [Fact]
        public void AddTicket_VoegtTicketToeAanContract() {
            Ticket ticket = new Ticket();
            int aantalTicketsVoorAdd = _contract.Tickets.Count;
            
            _contract.AddTicket(ticket);

            Assert.Equal(aantalTicketsVoorAdd+1, _contract.Tickets.Count);
        }

        [Fact]
        public void CancelContract_OpenContract_SteltEindDatumInEnVerandertStatusNaarCancelled() {
            _contract.CancelContract();

            Assert.Equal(ContractStatus.Cancelled, _contract.Status);
            Assert.Equal(DateTime.Now.Day, _contract.Einddatum.Value.Day);
        }

        [Fact]
        public void CancelContract_GeslotenContract_ThrowsException() {
            _contract.Status = ContractStatus.Finished;

            Assert.Throws<ArgumentException>(() => _contract.CancelContract());
        }

        [Fact]
        public void IsOpen_ActiveContract_ReturnsTrue() {
            _contract.Status = ContractStatus.Active;
            Assert.True(_contract.IsOpen());
        }

        [Fact]
        public void IsOpen_PendingContract_ReturnsTrue() {
            _contract.Status = ContractStatus.Pending;
            _contract.Startdatum = DateTime.Now.AddDays(1);
            Assert.True(_contract.IsOpen());
            Assert.Equal(ContractStatus.Pending, _contract.Status);
        }

        [Fact]
        public void IsOpen_PendingContractMaarStartDatumVerlopen_SetsStatusToActiveAndReturnsTrue() {
            _contract.Status = ContractStatus.Pending;
            _contract.Startdatum = DateTime.Now.AddDays(-1);
            Assert.True(_contract.IsOpen());
            Assert.Equal(ContractStatus.Active, _contract.Status);
        }

        [Fact]
        public void IsOpen_ActiveContractMaarEindDatumVerlopen_SetsStatusToFinishedAndReturnsFalse() {
            _contract.Status = ContractStatus.Active;
            _contract.Einddatum = DateTime.Now.AddDays(-1);

            Assert.False(_contract.IsOpen());
            Assert.Equal(ContractStatus.Finished, _contract.Status);
        }

        [Fact]
        public void IsOpen_CancelledContract_ReturnsFalse() {
            _contract.Status = ContractStatus.Cancelled;
            Assert.False(_contract.IsOpen());
        }

        [Fact]
        public void IsOpen_FinishedContract_ReturnsFalse() {
            _contract.Status = ContractStatus.Finished;
            Assert.False(_contract.IsOpen());
        }
    }
}
