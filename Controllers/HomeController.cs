using _2021_dotnet_g_04.Filters;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace _2021_dotnet_g_04.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(Klant klant)
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.ContractsExpire30 = klant.GetOpenContracts().Where(c => (c.Einddatum.Value < DateTime.Today.AddDays(30))).ToList();
            homeViewModel.TicketsWaitingCustomerInfo = klant.GetOpenTickets().ToList().Where(t => t.Status.ToString().Equals("AwaitingCustomerInformation") && t.BekekenDoorKlant == false).ToList();
            //homeViewModel.TicketsWaitingCustomerInfo = klant.GetOpenTickets().ToList().Where(t => t.Status.ToString().Equals(TicketStatus.AwaitingCustomerInformation) && t.BekekenDoorKlant == false).ToList();
            homeViewModel.TicketsRecentlyFinished = klant.GetClosedTickets().ToList().Where(t => t.Status.ToString().Equals("Finished") && t.BekekenDoorKlant == false).ToList();
            //homeViewModel.TicketsRecentlyFinished = klant.GetClosedTickets().ToList().Where(t => t.Status.ToString().Equals(TicketStatus.Finished) && t.BekekenDoorKlant == false).ToList();

            /*
            ViewData["ContractsExpire30"] = false;
            ViewData["TicketsWaitingCustomerInfo"] = false;
            ViewData["TicketsRecentlyFinished"] = false;

            if (homeViewModel.ContractsExpire30.Count > 0)
            {
                ViewData["ContractsExpire30"] = true;
            }

            if (homeViewModel.TicketsWaitingCustomerInfo.Count > 0)
            {
                ViewData["TicketsWaitingCustomerInfo"] = true;
            }

            if (homeViewModel.TicketsRecentlyFinished.Count > 0)
            {
                ViewData["TicketsRecentlyFinished"] = true;
            }
            */

            ViewData["activeLink"] = "Index";
            return View(homeViewModel);
        }

    }
}
