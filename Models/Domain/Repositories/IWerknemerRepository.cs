﻿using _2021_dotnet_g_04.Models.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.Domain.Repositories {
	public interface IWerknemerRepository {
		Werknemer GetRandomTechnician();
	}
}
