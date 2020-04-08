using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Dysnomia.WhoOwnsMe.Business.Interfaces;
using Dysnomia.WhoOwnsMe.Common.Models;
using Dysnomia.WhoOwnsMe.DataAccess.Interfaces;

namespace Dysnomia.WhoOwnsMe.Business.Implementations {
	public class PropertyService : IPropertyService {
		private readonly IPropertyDataAccess propertyDataAccess;
		public PropertyService(IPropertyDataAccess propertyDataAccess) {
			this.propertyDataAccess = propertyDataAccess;
		}

		public async Task<IEnumerable<string>> Search(string name) {
			try {
				return await propertyDataAccess.Search(name);
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}

			return null;
		}

		public async Task<Property> GetPropertyByName(string name) {
			try {
				return await propertyDataAccess.GetPropertyByName(name);
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}

			return null;
		}

		public async Task AddViewToItem(string name) {
			try {
				await propertyDataAccess.AddViewToItem(name);
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}

		public async Task<IEnumerable<Property>> GetTopProperties() {
			try {
				return await propertyDataAccess.GetTopProperties();
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}

			return new List<Property>();
		}
	}
}
