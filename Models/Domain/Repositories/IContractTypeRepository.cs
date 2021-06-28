using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain.Repositories {
	public interface IContractTypeRepository {
		ContractType GetBy(int id);
		List<ContractType> GetAllActiveContractTypes();
		void SelectTicketAanmaakmanieren(ContractType contractType);
		void SaveChanges();
	}
}
