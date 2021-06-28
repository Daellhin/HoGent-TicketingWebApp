using _2021_dotnet_g_04.Models.Domain.Enumerations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace _2021_dotnet_g_04.Models.Domain {
    public class ContractType {
        public int Id { get; set; }
        public ContractDoorlooptijd MinimaleDoorlooptijd { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public ContractTypeStatus Status { get; set; }
        public TicketAanmaakTijd TicketAanmaaktijd { get; set; }
        public int TicketAfhandeltijd { get; set; } // in aantal dagen

       [JsonIgnore]
        public ICollection<Contract> Contracts { get; set; }

        // moet manueel uit de databank opgehaald worden met de contractTypeRepository, want Java
        public ICollection<ContractTypeTicketAanmaakmanier> TicketAanmaakmanieren { get; set; }

        public ContractType() {
            Contracts = new HashSet<Contract>();
        }
    }
}
