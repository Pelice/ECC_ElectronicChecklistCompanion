using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECC.Core;
using ECC.Data;
using ECC.Data.Models;
using NLog;

namespace Ecc.Core
{
	public class ChecklistSqlManager : IChecklistManager
	{
		private readonly SQLiteStorageManager _storage;

		public ChecklistSqlManager(SQLiteStorageManager sqLiteStorage)
		{
			_storage = sqLiteStorage;
		}

		public async Task<List<Airplane>> GetAvailableAirplaneChecklists()
		{
			return await _storage.GetAirplanesListAsync();
		}

		public async Task<Airplane?> GetChecklistForAirplane(int checklistId)
		{
			return await _storage.GetAirplaneCheclistDataAsync(checklistId);
		}
	}
}
