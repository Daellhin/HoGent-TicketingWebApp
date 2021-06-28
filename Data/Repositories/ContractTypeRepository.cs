using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Data.Repositories {
	public class ContractTypeRepository : IContractTypeRepository {
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<ContractType> _contractTypes;

		public ContractTypeRepository(ApplicationDbContext context) {
			_dbContext = context;
			_contractTypes = _dbContext.ContractTypes;
		}

		public List<ContractType> GetAllActiveContractTypes() {
			List<ContractType> contractTypes = _contractTypes.Where(e => e.Status == ContractTypeStatus.Active).ToList();
			foreach (var contractType in contractTypes) {
				SelectTicketAanmaakmanieren(contractType);
			}
			return contractTypes;
		}

		public void SelectTicketAanmaakmanieren(ContractType contractType) {
			contractType.TicketAanmaakmanieren = _dbContext.ContractTypeTicketAanmaakManieren.Where(e => e.ContractTypeId == contractType.Id).ToList();
		}

		public ContractType GetBy(int id) {
			return _contractTypes.Find(id);
		}

		public void SaveChanges() {
			_dbContext.SaveChanges();
		}
	}
}
