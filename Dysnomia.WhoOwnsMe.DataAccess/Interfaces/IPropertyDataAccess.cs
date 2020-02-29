using System.Collections.Generic;
using System.Threading.Tasks;

using Dysnomia.WhoOwnsMe.Common.Models;

namespace Dysnomia.WhoOwnsMe.DataAccess.Interfaces {
	public interface IPropertyDataAccess {
		Task<IEnumerable<string>> Search(string name);
		Task<Property> GetPropertyByName(string name);
	}
}
