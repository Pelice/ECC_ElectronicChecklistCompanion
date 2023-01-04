using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ECC.Data.Models;
using Microsoft.Data.Sqlite;

namespace ECC.Data
{
	public class SQLiteStorageManager
	{
		private string? _dbCnn = null;

		public SQLiteStorageManager(string folder, string dbname)
		{
			var dbFile = Path.Combine(folder, dbname);
			if (File.Exists(dbFile))
			{
				_dbCnn = dbFile;
			}
			else
				throw new Exception($"Db file {dbFile} not found!");
		}

		public List<Airplane> GetAirplanes()
		{
			List<Airplane> result = new List<Airplane>();

			string sql  = @"
select 
	AirplaneId,
	AirplaneName,
	SimConnectName
from Airplanes
";
			using (var db = new SqliteConnection(_dbCnn))
			{
				db.Open();

				result = db.Query<Airplane>(sql).ToList();
			}

			return result;
		}
	}
}
