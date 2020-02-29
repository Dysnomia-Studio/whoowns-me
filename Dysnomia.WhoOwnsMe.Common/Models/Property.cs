using System.Collections.Generic;
using System.Data;

using Dysnomia.Common.SQL;

namespace Dysnomia.WhoOwnsMe.Common.Models {
	public class Property {
		public string Name { get; set; }
		public string LongName { get; set; }
		public string Type { get; set; }
		public string[] Sources { get; set; }
		public IEnumerable<OtherThing> Owners { get; set; }
		public IEnumerable<OtherThing> Possessions { get; set; }

		public static Property MapFromReader(IDataReader reader) {
			if (!reader.Read()) {
				return null;
			}

			return new Property {
				Name = reader.GetString("name"),
				LongName = reader.GetString("longName"),
				Type = reader.GetString("type"),
				Sources = reader["sources"] as string[]
			};
		}
	}
}
