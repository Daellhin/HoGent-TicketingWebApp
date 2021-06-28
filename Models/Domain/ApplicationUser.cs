using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain {
	public class ApplicationUser : IdentityUser {
		public Klant Klant { get; set; }
	}
}
