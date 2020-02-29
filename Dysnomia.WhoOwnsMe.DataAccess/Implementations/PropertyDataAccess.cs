using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Dysnomia.Common.SQL;
using Dysnomia.WhoOwnsMe.Common;
using Dysnomia.WhoOwnsMe.Common.Models;
using Dysnomia.WhoOwnsMe.DataAccess.Interfaces;

using Microsoft.Extensions.Options;

using Npgsql;


namespace Dysnomia.WhoOwnsMe.DataAccess.Implementations {
	public class PropertyDataAccess : IPropertyDataAccess {
		private readonly string connectionString;
		public PropertyDataAccess(IOptions<AppSettings> appSettings) {
			connectionString = appSettings.Value.ConnectionString;
		}

		public async Task<IEnumerable<string>> Search(string name) {
			using var connection = new NpgsqlConnection(connectionString);

			var reader = await connection.ExecuteQuery(
				"SELECT name FROM things WHERE name ILIKE @name",
				new Dictionary<string, object>() {
					{ "name", "%" + name + "%" }
				}
			);

			var retour = new List<string>();
			while (reader.Read()) {
				retour.Add(reader.GetString("name"));
			}

			return retour;
		}

		private async Task<IEnumerable<OtherThing>> GetOwners(Guid id) {
			using var connection = new NpgsqlConnection(connectionString);

			var reader = await connection.ExecuteQuery(
				"SELECT owners.*, things.name " +
				"FROM owners " +
				"INNER JOIN things ON parent = things.id " +
				"WHERE child = @id",
				new Dictionary<string, object>() {
					{ "id", id }
				}
			);

			return OtherThing.MapListFromReader(reader);
		}

		private async Task<IEnumerable<OtherThing>> GetPossessions(Guid id) {
			using var connection = new NpgsqlConnection(connectionString);

			var reader = await connection.ExecuteQuery(
				"SELECT owners.*, things.name " +
				"FROM owners " +
				"INNER JOIN things ON child = things.id " +
				"WHERE parent = @id",
				new Dictionary<string, object>() {
					{ "id", id }
				}
			);

			return OtherThing.MapListFromReader(reader);
		}

		public async Task<Property> GetPropertyByName(string name) {
			using var connection = new NpgsqlConnection(connectionString);

			var reader = await connection.ExecuteQuery(
				"SELECT * FROM things WHERE LOWER(name) = LOWER(@name)",
				new Dictionary<string, object>() {
					{ "name", name }
				}
			);

			var property = Property.MapFromReader(reader);

			if (property == null) {
				return null;
			}

			Guid id = reader.GetGuid("id");

			property.Owners = await GetOwners(id);
			property.Possessions = await GetPossessions(id);

			return property;
		}
	}
}