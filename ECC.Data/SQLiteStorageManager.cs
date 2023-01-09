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
				_dbCnn = $"Data Source={dbFile};";
			}
			else
				throw new Exception($"Db file {dbFile} not found!");
		}

		public async Task<List<Airplane>> GetAirplanesListAsync()
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

				result = (await db.QueryAsync<Airplane>(sql)).ToList();
			}

			return result;
		}

		private async Task<Airplane?> GetAirplaneDataAsync(int airplaneId) {
			Airplane? result = null;

			string sql = @"
select 
	AirplaneId,
	AirplaneName,
	SimConnectName
from Airplanes
where
	AirplaneId = @id
";
			using (var db = new SqliteConnection(_dbCnn))
			{
				db.Open();

				result = await db.QueryFirstOrDefaultAsync<Airplane>(sql, new { id = airplaneId});
			}

			return result;
		}

		private async Task<Airplane?> GetAirplaneDataFromChecklistIdAsync(int checklistId)
		{
			Airplane? result = null;

			string sql = @"

select 
	a.AirplaneId,
	a.AirplaneName,
	a.SimConnectName
from Checklists c 
     join Airplanes a on c.airplaneId = a.airplaneId
where
	c.checklistId = @id
";
			using (var db = new SqliteConnection(_dbCnn))
			{
				db.Open();

				result = await db.QueryFirstOrDefaultAsync<Airplane>(sql, new { id = checklistId });
			}

			return result;
		}

		public async Task<List<Checklist>> GetAirplaneChecklistsListForAirplaneAsync(int airplaneId) {
			List<Checklist> result = new List<Checklist>();

			string sql = @"
select 
    AirplaneId,
    ChecklistId,
    ChecklistName
from checklists
where AirplaneId = @id
";
			using (var db = new SqliteConnection(_dbCnn))
			{
				db.Open();

				result = (await db.QueryAsync<Checklist>(sql, new { id = airplaneId})).ToList();
			}

			return result;
		}
		private async Task<List<ChecklistSection>> GetAirplaneChecklistSectionListForChecklistAsync(int checklistId) {
			List<ChecklistSection> result = new List<ChecklistSection>();

			string sql = @"
select 
    SectionId,
    ChecklistId,
    SectionName,
    SectionOrder
from ChecklistSections
where ChecklistId = @id
";
			using (var db = new SqliteConnection(_dbCnn))
			{
				db.Open();

				result = (await db.QueryAsync<ChecklistSection>(sql, new { id = checklistId})).ToList();
			}

			return result;
		}
		private async Task<List<ChecklistItem>> GetChecklistItemListForSectionAsync(int sectionId) {
			List<ChecklistItem> result = new List<ChecklistItem>();

			string sql = @"
select 
    ChecklistItemId,
    SectionId,
    ItemOrder,
    Description,
    Value,
    Notes,
    Image,
    TextColor,
    TextBackgroundColor,
    TextBold
from ChecklistItems
where sectionid = @id
order by ItemOrder
";
			using (var db = new SqliteConnection(_dbCnn))
			{
				db.Open();

				result = (await db.QueryAsync<ChecklistItem>(sql, new { id = sectionId })).ToList();
			}

			return result;
		}
		
		public async Task<Airplane?> GetAirplaneCheclistDataAsync(int checlistId) {
			Airplane? result = await GetAirplaneDataFromChecklistIdAsync(checlistId);

			if (result == null)
				return null;
			
			result.Checklist = await GetAirplaneChecklistsListForAirplaneAsync(result.AirplaneId);
			
			if (result.Checklist != null && result.Checklist.Any())
			{
				foreach(var c in result.Checklist)
				{
					c.Sections = await GetAirplaneChecklistSectionListForChecklistAsync(c.ChecklistId);
					if (c.Sections != null && c.Sections.Any())
					{
						foreach(var s in c.Sections)
						{
							s.ChecklistItems = await GetChecklistItemListForSectionAsync(s.SectionId);
						}
					}
				}
			}

			return result;
		}
	}
}
