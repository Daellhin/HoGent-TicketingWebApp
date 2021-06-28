using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_g_04.Extensions {
	public static class DateTimeExtension {
		public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek) {
			int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
			return dt.AddDays(-1 * diff).Date;
		}

		public static DateTime StartOfWeekMonday(this DateTime dt) {
			return StartOfWeek(dt, DayOfWeek.Monday);
		}
	}
}
