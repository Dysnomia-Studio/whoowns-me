using System;
using System.Collections.Generic;
using System.Data;

using Dysnomia.Common.SQL;

namespace Dysnomia.WhoOwnsMe.Common.Models {
	public class OtherThing {
		public string Name { get; set; }
		public double Percentage { get; set; }
		public DateTime BeginDate { get; set; }
		public DateTime? EndDate { get; set; }

		public static List<OtherThing> MapListFromReader(IDataReader reader) {
			var list = new List<OtherThing>();

			while (reader.Read()) {
				list.Add(
					new OtherThing {
						Name = reader.GetString("name"),
						Percentage = reader.GetDouble("percentage"),
						BeginDate = reader.GetDateTime("beginDate"),
						EndDate = reader.GetNullableDateTime("endDate"),
					}
				);
			}

			return list;
		}
	}
}