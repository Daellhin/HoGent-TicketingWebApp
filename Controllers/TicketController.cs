using _2021_dotnet_g_04.Filters;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using _2021_dotnet_g_04.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_dotnet_g_04.Controllers
{
	[Authorize]
	public class TicketController : Controller
	{

		private readonly IWerknemerRepository _werknemerRepository;
		private readonly ITicketRepository _ticketRepository;
		private readonly IContractTypeRepository _contractTypeRepository;

		public TicketController(IWerknemerRepository werknemerRepository, ITicketRepository ticketRepository, IContractTypeRepository contractTypeRepository)
		{
			_werknemerRepository = werknemerRepository;
			_ticketRepository = ticketRepository;
			_contractTypeRepository = contractTypeRepository;
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Index(Klant klant)
		{
			IEnumerable<Ticket> openTickets = klant.GetOpenTickets();
			ViewData["OpenTickets"] = true;
			ViewData["activeLink"] = "OpenTickets";
			List<TicketEditViewModel> ticketEditViewModels = openTickets.Select(e => new TicketEditViewModel(e)).ToList();
			return View(ticketEditViewModels);
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult ClosedTickets(Klant klant)
		{
			IEnumerable<Ticket> closedTickets = klant.GetClosedTickets();

			ViewData["OpenTickets"] = false;
			ViewData["activeLink"] = "ClosedTickets";
			return View(nameof(Index), closedTickets.Select(e => new TicketEditViewModel(e)).ToList());
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Edit(int id, Klant klant) {
			Ticket ticket = klant.GetTicketBy(id);
			if (ticket == null) {
				return NotFound();
			}

			ViewData["Contracts"] = GetContractsAsSelectList(klant);
			ViewData["activeLink"] = ticket.IsOpen() ? "OpenTickets" : "ClosedTickets";
			return View(new TicketEditViewModel(ticket));
		}

		[HttpPost]
		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Edit(int id, TicketEditViewModel model, Klant klant) {
			if (ModelState.IsValid) {
				try {
					klant.EditTicket(id, model.Urgency, model.NewComment, model.NewBijlages.Select(e => new TicketBijlage(e.Bijlage)).ToList());
					_ticketRepository.SaveChanges();
					TempData["messageTitle"] = $"You successfully updated the ticket";
					TempData["message"] = $"{klant.GetTicketBy(id).Titel}";
					Ticket ticket = klant.GetTicketBy(id);
				}
				catch (KeyNotFoundException) {
					return NotFound();
				}
				catch (ArgumentException e) {
					TempData["errorTitle"] = e;
				}
				catch (Exception e) {
					TempData["errorTitle"] = $"Sorry, something went wrong, ticket #{id} was not updated";
					TempData["error"] = $"{e.Message}";
				}
				return RedirectToAction(nameof(Index));
			}

			ViewData["Contracts"] = GetContractsAsSelectList(klant);
			ViewData["activeLink"] = "OpenTickets";
			return View(model);
		}

		[HttpPost]
		// source: https://github.com/stevcooo/AddItemsDynamically
		public ActionResult EditAddBijlage(TicketEditViewModel model) {
			List<BijlageViewModel> outputBijlages = new List<BijlageViewModel>();
			int index = 0;
			foreach (var item in model.NewBijlages) {
				outputBijlages.Add(new BijlageViewModel(index, item.Bijlage));
				index++;
			}
			outputBijlages.Add(new BijlageViewModel(index));
			model.NewBijlages = outputBijlages;
			return PartialView("EditBijlage", model);
		}

		[HttpPost]
		// source: https://github.com/stevcooo/AddItemsDynamically
		public ActionResult EditRemoveBijlage(TicketEditViewModel model, int id) {
			// geen idee waarom dit nodig is maar zonder werkt het niet en word enkel de laatste bijlage verwijderd
			// source: https://stackoverflow.com/questions/4438904/strange-behaviour-in-asp-net-mvc-removing-item-from-a-list-in-a-nested-structur
			ModelState.Clear();

			List<BijlageViewModel> outputBijlages = new List<BijlageViewModel>();
			int index = 0;
			foreach (var item in model.NewBijlages) {
				if (item.Id != id) {
					outputBijlages.Add(new BijlageViewModel(index, item.Bijlage));
					index++;
				}
			}
			model.NewBijlages = outputBijlages;
			return PartialView("EditBijlage", model);
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Create(Klant klant) {
			if (klant.Contracten.Where(e => e.Status == ContractStatus.Active).Count() <= 0) {
				TempData["errorTitle"] = $"You do not have any active contracts";
				TempData["error"] = $"Please sign a new contract to start submitting tickets.";
				return RedirectToAction(nameof(Index));
			}

			ViewData["Contracts"] = GetContractsAsSelectList(klant);
			ViewData["activeLink"] = "CreateTicket";
			return View(new TicketCreateViewModel());
		}

		[HttpPost]
		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Create(TicketCreateViewModel model, Klant klant)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Werknemer technieker = _werknemerRepository.GetRandomTechnician();
					klant.CreateTicket(model.Titel, model.Urgency, model.ContractNummer, model.Dienst, model.Omschrijving, model.NewBijlages, technieker);
					_ticketRepository.SaveChanges();
					TempData["messageTitle"] = $"You successfully created the ticket:";
					TempData["message"] = $"{model.Titel}";
					TempData["sorteer"] = $"id_desc";
				}
				catch (Exception e)
				{
					TempData["errorTitle"] = $"Sorry, something went wrong, the ticket was not created";
					TempData["error"] = $"{e.Message}";
				}
				return RedirectToAction(nameof(Index));
			}

			ViewData["Contracts"] = GetContractsAsSelectList(klant);
			ViewData["activeLink"] = "CreateTicket";
			return View(model);
		}

		[HttpPost]
		// source: https://github.com/stevcooo/AddItemsDynamically
		public ActionResult CreateAddBijlage(TicketCreateViewModel model) {
			List<BijlageViewModel> outputBijlages = new List<BijlageViewModel>();
			int index = 0;
			foreach (var item in model.NewBijlages) {
				outputBijlages.Add(new BijlageViewModel(index, item.Bijlage));
				index++;
			}
			outputBijlages.Add(new BijlageViewModel(index));
			model.NewBijlages = outputBijlages;
			return PartialView("CreateBijlage", model);
		}

		[HttpPost]
		// source: https://github.com/stevcooo/AddItemsDynamically
		public ActionResult CreateRemoveBijlage(TicketEditViewModel model, int id) {
			// geen idee waarom dit nodig is maar zonder werkt het niet en word enkel de laatste bijlage verwijderd
			ModelState.Clear();

			List<BijlageViewModel> outputBijlages = new List<BijlageViewModel>();
			int index = 0;
			foreach (var item in model.NewBijlages) {
				if (item.Id != id) {
					outputBijlages.Add(new BijlageViewModel(index, item.Bijlage));
					index++;
				}
			}
			model.NewBijlages = outputBijlages;
			return PartialView("CreateBijlage", model);
		}

		public IActionResult Cancel(int id)
		{
			try
			{
				Ticket ticket = _ticketRepository.GetBy(id);
				ticket.CancelTicket();
				_ticketRepository.SaveChanges();
				TempData["messageTitle"] = $"You successfully cancelled ticket:";
				TempData["message"] = $"{ticket.Titel}";
			}
			catch (ArgumentException e)
			{
				TempData["error"] = e;
			}
			catch (Exception e)
			{
				TempData["errorTitle"] = $"Sorry, something went wrong, ticket #{id} was not cancelled";
				TempData["error"] = $"{e.Message}";
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult SetViewedByCustomer(int id) {
			try {
				Ticket ticket = _ticketRepository.GetBy(id);
				ticket.BekekenDoorKlant = true;
				_ticketRepository.SaveChanges();
				return StatusCode(201);
			}
			catch {
				return NotFound();
			}
		}


		[ServiceFilter(typeof(KlantFilter))]
		private SelectList GetContractsAsSelectList(Klant klant)
		{
			var actieveContracten = klant.GetOpenContracts().Where(e => e.Status == ContractStatus.Active).ToList();
			actieveContracten.ForEach(e => _contractTypeRepository.SelectTicketAanmaakmanieren(e.ContractType));
			return new SelectList(actieveContracten.Where(e => e.ContractType.TicketAanmaakmanieren.Any(p => p.IsApplication())).ToList(), nameof(Contract.Nummer), nameof(Contract.DisplayName));
		}
	}
}
