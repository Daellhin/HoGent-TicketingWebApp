using _2021_dotnet_g_04.Models.Domain;
using System.Collections.Generic;

namespace _2021_dotnet_g_04.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Contract> ContractsExpire30 { get; set; }
        public List<Ticket> TicketsWaitingCustomerInfo { get; set; }
        public List<Ticket> TicketsRecentlyFinished { get; set; }
    }
}
