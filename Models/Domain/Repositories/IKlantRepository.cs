using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain.Repositories {
	public interface IKlantRepository {
		bool Exists(string gebruikersnaam, string wachtwoord);
		Klant GetBy(string gebruikersnaam, string wachtwoord);
		Klant GetKlantFromUsername(string gebruikersnaam);
		void SaveChanges();
	}
}
