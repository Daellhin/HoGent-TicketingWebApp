using _2021_dotnet_g_04.Filters;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using _2021_dotnet_g_04.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021_dotnet_g_04.Controllers {
	[Authorize]
	public class ContractController : Controller {
		private readonly IContractTypeRepository _contractTypeRepository;

		public ContractController(IContractTypeRepository contractTypeRepository) {
			_contractTypeRepository = contractTypeRepository;
			
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Index( Klant klant) {
			IEnumerable<Contract> openContracts = klant.GetOpenContracts();
			ViewData["OpenContracts"] = true;
			ViewData["activeLink"] = "OpenContracts";
			return View(openContracts.Select(e => new ContractViewModel(e)).ToList());
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult ClosedContracts(Klant klant) {
			IEnumerable<Contract> closedContracts = klant.GetClosedContracts();
			ViewData["OpenContracts"] = false;
			ViewData["activeLink"] = "ClosedContracts";
			return View(nameof(Index), closedContracts.Select(e => new ContractViewModel(e)).ToList());
		}

		public IActionResult Create() {
			ViewBag.contractTypes = _contractTypeRepository.GetAllActiveContractTypes().Select(e => new ContractTypeViewModel(e)).ToList();
			ViewData["activeLink"] = "CreateContract";
			return View(new ContractCreateViewModel());
		}

		[HttpPost]
		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Create(ContractCreateViewModel model, Klant klant) {
			if (ModelState.IsValid) {
				try {
				var contracttype = _contractTypeRepository.GetBy(model.ContractTypeId);
				klant.CreateContract(model.Startdatum, _contractTypeRepository.GetBy(model.ContractTypeId));
				_contractTypeRepository.SaveChanges();
				TempData["messageTitle"] = $"You successfully signed the contract:";
				TempData["message"] = contracttype.Naam;
				return RedirectToAction(nameof(Index));
				}
				catch (Exception e) {
					ModelState.AddModelError("", e.Message);
				}
			}
			ViewBag.contractTypes = _contractTypeRepository.GetAllActiveContractTypes().Select(e => new ContractTypeViewModel(e)).ToList();
			ViewData["activeLink"] = "CreateContract";
			return View(model);
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Details(int id, Klant klant) {
			Contract contract = klant.GetContractBy(id);
			if (contract == null) {
				return NotFound();
			}
			_contractTypeRepository.SelectTicketAanmaakmanieren(contract.ContractType);

			ViewData["activeLink"] = contract.IsOpen() ? "OpenContracts" : "ClosedContracts";
			return View(new ContractViewModel(contract));
		}

		[ServiceFilter(typeof(KlantFilter))]
		public IActionResult Cancel(int id, Klant klant) {
			Contract contract = null;
			try {
				contract = klant.GetContractBy(id);
				contract.CancelContract();
				_contractTypeRepository.SaveChanges();
				TempData["messageTitle"] = $"You successfully cancelled the contract:";
				TempData["message"] = contract.DisplayName;
			}
			catch (ArgumentException e) {
				TempData["error"] = e;
			}
			catch (Exception) {
				TempData["errorTitle"] = $"Sorry, something went wrong, the contract was not cancelled";
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
