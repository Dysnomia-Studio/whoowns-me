using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Dysnomia.WhoOwnsMe.Business.Interfaces;
using Dysnomia.WhoOwnsMe.Common.Models;
using Dysnomia.WhoOwnsMe.DataAccess.Interfaces;

namespace Dysnomia.WhoOwnsMe.Business.Implementations {
	public class PropertyService : IPropertyService {
		public IPropertyDataAccess propertyDataAccess;
		public PropertyService(IPropertyDataAccess propertyDataAccess) {
			this.propertyDataAccess = propertyDataAccess;
		}

		public async Task<IEnumerable<string>> Search(string name) {
			try {
				return await propertyDataAccess.Search(name);
			} catch (Exception e) {
				// @TODO: log it
			}

			return null;
		}

		public async Task<Property> GetPropertyByName(string name) {
			try {
				return await propertyDataAccess.GetPropertyByName(name);
			} catch (Exception e) {
				var test = "";
				// @TODO: log it
			}

			return null;
		}
	}
}
