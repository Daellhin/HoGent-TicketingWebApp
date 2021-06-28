using _2021_dotnet_g_04.Extensions;
using _2021_dotnet_g_04.Filters;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_dotnet_g_04.Controllers {
	[Authorize]
	public class StatistiekController : Controller {

		public StatistiekController() {
		}

		// source: https://www.c-sharpcorner.com/article/creating-charts-with-asp-net-core/
		// docs: https://www.chartjs.org/docs/latest/
		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Index(Klant klant) {
			List<ChartViewModel> chartViewModels = new List<ChartViewModel>();

			List<Ticket> finishedTickets = klant.GetClosedTickets()
				.Where(e => e.Status == TicketStatus.Finished)
				.ToList();
			if(finishedTickets.Count() > 0) {
				chartViewModels.Add(CreateFinishedOnTimeStatistic(finishedTickets));
			}

			List<Ticket> tickets = klant.GetTickets()
				.ToList();
			if (tickets.Count() > 0) {
				chartViewModels.Add(CreateCreatedTicketsPerWeekStatistic(klant, tickets));
			}

			ViewData["activeLink"] = "Statistics";
			return View(chartViewModels);
		}

		private ChartViewModel CreateFinishedOnTimeStatistic(IEnumerable<Ticket> finishedTickets) {
			Dictionary<bool, int> ticketsOpTijd = finishedTickets.GroupBy(e => e.IsFinishedOnTime())
					.ToDictionary(grp => grp.Key, grp => grp.Count());

			Dictionary<string, int> data0 = ticketsOpTijd.ToDictionary(e => e.Key ? "Finished On Time" : "Finished Late", e => e.Value);
			return new ChartViewModel(0, "pie", "Distribution Tickets Finished On Time", "On Time", "Amount Of Tickets", data0);
		}

		private ChartViewModel CreateCreatedTicketsPerWeekStatistic(Klant klant, IEnumerable<Ticket> tickets) {
			// Barchart: Aantal tickets aangemaakt per week
			Dictionary<DateTime, int> ticketsPerWeek = tickets.GroupBy(e => e.DatumAanmaak.StartOfWeekMonday())
				.ToDictionary(grp => grp.Key, grp => grp.Count());

			// grafiek verder opvullen, met weken waar er geen ticketten zijn aangemaakt
			DateTime startOfGraph = klant.Datumklantgeworden.StartOfWeekMonday();
			DateTime endOfGraph = DateTime.Now.StartOfWeekMonday();
			for (DateTime date = startOfGraph; date <= endOfGraph; date = date.AddDays(7)) {
				if (!ticketsPerWeek.ContainsKey(date)) {
					ticketsPerWeek.Add(date, 0);
				}
			}

			Dictionary<string, int> data1 = ticketsPerWeek.OrderBy(e => e.Key.Date).ToDictionary(e => e.Key.ToShortDateString(), e => e.Value);
			return new ChartViewModel(1, "bar", "Tickets Created Per Week", "Week", "Amount Of Tickets", data1);
		}

	}
}
