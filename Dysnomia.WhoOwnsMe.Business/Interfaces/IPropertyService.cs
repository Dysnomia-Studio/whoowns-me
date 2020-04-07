using System.Collections.Generic;
using System.Threading.Tasks;

using Dysnomia.WhoOwnsMe.Common.Models;

namespace Dysnomia.WhoOwnsMe.Business.Interfaces {
	public interface IPropertyService {
		Task<IEnumerable<string>> Search(string name);
		Task<Property> GetPropertyByName(string name);
		Task AddViewToItem(string name);
		Task<IEnumerable<Property>> GetTopProperties();
	}
}
