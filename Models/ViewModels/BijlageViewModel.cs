using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Models.ViewModels {
	public class BijlageViewModel {
		[HiddenInput]
		public int Id { get; set; }
		public string Bijlage { get; set; }

		public BijlageViewModel() {
		}

		public BijlageViewModel(int id) {
			Id = id;
		}

		public BijlageViewModel(int id, string bijlage) {
			Id = id;
			Bijlage = bijlage;
		}

		public override string ToString() {
			return $"{Id} {Bijlage}";
		}
	}
}
