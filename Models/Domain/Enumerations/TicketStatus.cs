using System.ComponentModel.DataAnnotations;

namespace _2021_dotnet_g_04.Models.Domain.Enumerations
{
    public enum TicketStatus
    {
        Created,
        Pending,
        Finished,
        Cancelled,
        [Display(Name = "Awaiting customer information")]
        AwaitingCustomerInformation,
        [Display(Name = "Received customer information")]
        ReceivedCustomerInformation,
        [Display(Name = "In development")]
        InDevelopment,
    }
}
