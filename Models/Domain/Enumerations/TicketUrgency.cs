using System.ComponentModel.DataAnnotations;

namespace _2021_dotnet_g_04.Models.Domain.Enumerations {
    public enum TicketUrgency {
		[Display(Name = "Production impacted")]
		ProductionImpacted,
		[Display(Name = "Production will be impacted")]
		ProductionWillBeImpacted,
		[Display(Name = "No production impact")]
		NoProductionImpact
	}
}
