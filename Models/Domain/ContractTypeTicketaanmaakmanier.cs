using _2021_dotnet_g_04.Models.Domain.Enumerations;
using Newtonsoft.Json;

namespace _2021_dotnet_g_04.Models.Domain {
    public class ContractTypeTicketAanmaakmanier {
        public int ContractTypeId { get; set; }
        public TicketAanmaakManier TicketAanmaakmanier { get; set; }

        public ContractType ContractType { get; set; }

        public bool IsApplication() {
            return TicketAanmaakmanier == TicketAanmaakManier.Application;  
        }
    }
}
